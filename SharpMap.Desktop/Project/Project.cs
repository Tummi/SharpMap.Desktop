using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml;


using SharpMap;

using System.IO;
using System.Xml.Serialization;
using SharpMap.Serialization.Model;
using System.Net;
using System.Drawing;
using System.Windows.Forms;

namespace SharpMap.Desktop
{
    [Serializable]
    public class Project 
    {

        private Dictionary<string, Map> _maps = new Dictionary<string, Map>();

        public string FileName { get; private set; }


        public Project()
        {
            FileName = "";
        }

        public Dictionary<string, Map> maps
        {
            get
            {
                return _maps;
            }
        }

        public Map AddMap(string mapname)
        {
            if (_maps.Keys.Contains(mapname))
            {
                throw (new Exception("Mapname alredy in project"));
            }

            Map m = new Map();

            _maps.Add(mapname, m);

            return m;

        }

        public Map GetMap(string mapname)
        {
            return _maps[mapname];
        }

        public void Save()
        {
            if (FileName == "")
                SaveAs();
            else
                SaveSoap(FileName);

        }

        public void SaveAs()
        {
            SaveFileDialog dlg = new SaveFileDialog();

            if (FileName != "")
                dlg.FileName = FileName;

            dlg.Filter = "SharpMap xml project (*.sm.xml)|*.sm.xml|All files (*.*)|*.*";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SaveSoap(dlg.FileName);
            }
        }

        public void Load()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "SharpMap xml project (*.sm.xml)|*.sm.xml|All files (*.*)|*.*";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                LoadSoap(dlg.FileName);
            }
        }

        public void SaveSoap(string filename)
        {

            FileStream fs = new FileStream(filename, FileMode.Create);

            try
            {
                SoapFormatter formatter = new SoapFormatter();

                int mapsNum = _maps.Count;
                formatter.Serialize(fs, mapsNum);

                for (int i = 0; i < mapsNum; i++)
                {
                    formatter.Serialize(fs, _maps.ElementAt(i).Key);

                    MapDefinition md = GetMapDefinitionFromMap(_maps.ElementAt(i).Value);
                    formatter.Serialize(fs, md);

                }
            }
            catch (SerializationException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                fs.Close();
                FileName = filename;
            }
        }

        public void LoadSoap(string filename)
        {

            FileStream fs = new FileStream(filename, FileMode.Open);
            SoapFormatter formatter = new SoapFormatter();

            try
            {
                int mapsNum = (int)formatter.Deserialize(fs);

                for (int i = 0; i < mapsNum; i++)
                {
                    string mapName = (string)formatter.Deserialize(fs);

                    MapDefinition md = (MapDefinition)formatter.Deserialize(fs);

                    Map map = GetMapFromDefinition(md);

                    maps.Add(mapName, map);

                }

            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
                FileName = filename;
            }
       
        }

        private static MapDefinition GetMapDefinitionFromMap(Map m)
        {
            MapDefinition md = new MapDefinition();
            md.Extent = new Extent()
            {
                Xmin = m.Envelope.MinX,
                Xmax = m.Envelope.MaxX,
                Ymin = m.Envelope.MinY,
                Ymax = m.Envelope.MaxY
            };

            md.BackGroundColor = ColorTranslator.ToHtml(m.BackColor);
            md.SRID = m.SRID;

            List<MapLayer> layers = new List<MapLayer>();
            foreach (var layer in m.Layers)
            {
                MapLayer ml = null;
                if (layer is SharpMap.Layers.VectorLayer)
                {

                }
                else if (layer is SharpMap.Layers.WmsLayer)
                {
                    WmsLayer sl = new WmsLayer();
                    sl.OnlineURL = (layer as SharpMap.Layers.WmsLayer).CapabilitiesUrl;
                    sl.WmsLayers = string.Join(",", (layer as SharpMap.Layers.WmsLayer).LayerList.ToArray());
                    if ((layer as SharpMap.Layers.WmsLayer).Credentials is NetworkCredential)
                    {
                        sl.WmsUser = ((layer as SharpMap.Layers.WmsLayer).Credentials as NetworkCredential).UserName;
                        sl.WmsPassword = ((layer as SharpMap.Layers.WmsLayer).Credentials as NetworkCredential).Password;
                    }
                    ml = sl;
                }

                ml.MinVisible = layer.MinVisible;
                ml.MaxVisible = layer.MaxVisible;
                ml.Name = layer.LayerName;

                if (ml != null)
                    layers.Add(ml);
            }

            md.Layers = layers.ToArray();


            return md;

        }

        private static Map GetMapFromDefinition(MapDefinition md)
        {

            Map m = new Map();

            if (md.Extent != null)
                m.ZoomToBox(new GeoAPI.Geometries.Envelope(md.Extent.Xmin, md.Extent.Xmax, md.Extent.Ymin, md.Extent.Ymax));

            if (!string.IsNullOrEmpty(md.BackGroundColor))
            {
                m.BackColor = ColorTranslator.FromHtml(md.BackGroundColor);
            }

            m.SRID = md.SRID;

            foreach (var l in md.Layers)
            {
                SharpMap.Layers.ILayer lay = null;
                //WMSLayer?
                if (l is WmsLayer)
                {
                    ICredentials cred = null;
                    if (!string.IsNullOrEmpty((l as WmsLayer).WmsUser))
                        cred = new NetworkCredential((l as WmsLayer).WmsUser, (l as WmsLayer).WmsPassword);

                    SharpMap.Layers.WmsLayer wmsl = new Layers.WmsLayer(l.Name, (l as WmsLayer).OnlineURL, TimeSpan.MaxValue, WebRequest.DefaultWebProxy, cred);
                    if ((l as WmsLayer).WmsLayers != null)
                    {
                        string[] layers = (l as WmsLayer).WmsLayers.Split(',');
                        foreach (var wl in layers)
                        {
                            wmsl.AddLayer(wl);
                        }
                    }
                    else
                    {
                        wmsl.AddChildLayers(wmsl.RootLayer,true);
                    }
                    lay = wmsl;
                }
                //And some simple tiled layers
                else if (l is OsmLayer)
                {
                    lay = new Layers.TileLayer(new BruTile.Web.OsmTileSource(), l.Name);
                }
                else if (l is GoogleLayer)
                {
                    lay = new Layers.TileLayer(new BruTile.Web.GoogleTileSource(BruTile.Web.GoogleMapType.GoogleMap), l.Name);
                }
                else if (l is GoogleSatLayer)
                {
                    lay = new Layers.TileLayer(new BruTile.Web.GoogleTileSource(BruTile.Web.GoogleMapType.GoogleSatellite), l.Name);
                }
                else if (l is GoogleTerrainLayer)
                {
                    lay = new Layers.TileLayer(new BruTile.Web.GoogleTileSource(BruTile.Web.GoogleMapType.GoogleTerrain), l.Name);
                }

                if (lay != null)
                {
                    lay.MinVisible = l.MinVisible;
                    lay.MaxVisible = l.MaxVisible;
                    m.Layers.Add(lay);
                }
                
            }


            return m;
        }


      


       
    }
}
