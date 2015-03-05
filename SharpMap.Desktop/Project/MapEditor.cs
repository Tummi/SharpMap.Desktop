using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SharpMap;
using SharpMap.Layers;
using SharpMap.Data.Providers;
using SharpMap.Forms;
using System.Collections.ObjectModel;

namespace SharpMap.Desktop
{
    public partial class MapEditor : UserControl
    {

        private static Cursor CursorArrow;
        private static Cursor CursorDragDrop;
        private static Cursor CursorForbidden;
        //private static FormAddLayer _formVector;  //= new FormAddLayer(FormAddLayer.ListType.Vector);
        //private static FormAddLayer _formRaster; //= new FormAddLayer(FormAddLayer.ListType.Raster);

        private Map _map = null;
       
        private MapBox _mapBox;
        public MapBox MapBox
        {
            get
            {
                return _mapBox;
            }
            set
            {
                _mapBox = value;
                if (_map != null & _mapBox != null) _mapBox.Map = _map;
            }
        }

        public string MapName
        {
            get;  set;
        }

        public void SetMap(string mapname, Map newmap)
        {
            MapName = mapname;
            _map = newmap;

            if (_map != null & _mapBox != null) _mapBox.Map = _map;

            CreateFromMap();

            Invalidate();
            Refresh();
        }

       public MapEditor()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        private void MapEditor_Load(object sender, EventArgs e)
        {
            CursorArrow = new Cursor(new MemoryStream(Resources.Arrow));
            CursorDragDrop = new Cursor(new MemoryStream(Resources.DragDrop));
            CursorForbidden = new Cursor(new MemoryStream(Resources.Forbidden));
        }

        public void AddFolder()
        {
            string foldername = FormText.Show("Add New Folder", "Folder Name:", "New Folder");

            if (foldername != null)
            {
                LayerGroup layergroup = AddFolder(foldername);
            }

        }

        public LayerGroup AddFolder(string FolderName)
        {
            LayerGroup layergroup = new LayerGroup(FolderName);

            AddLayer(layergroup);

            return layergroup;

        }

        public void AddVector()
        {
            FormAddLayer _formVector = new FormAddLayer(FormAddLayer.ListType.Vector);

            _formVector.ConnectionString = "";

            if (_formVector.ShowDialog() == DialogResult.OK)
            {
                AddVector(_formVector.Identifier, _formVector.ConnectionString);
            }
        }

        public void AddVector(FileFilterIdentifier filterIdentifier, string ConnectionString)
        {
            Random rnd = new Random();

            int r = rnd.Next(127);
            int g = rnd.Next(127);
            int b = rnd.Next(127);

            Color dark = Color.FromArgb(r, g, b);
            Color light = Color.FromArgb(r * 2, g * 2, b * 2);

            string title = Path.GetFileNameWithoutExtension(ConnectionString);

            int i = 0;

            while (_map.Layers.GetLayerByName(title) != null)
            {
                i++;
                title = Path.GetFileNameWithoutExtension(ConnectionString) + i.ToString();
            }


            if (filterIdentifier.Name.Contains("SharpMap"))
            {
                VectorLayer layer = new VectorLayer(title);

                if (Path.GetExtension(ConnectionString).ToUpper() == ".SHP")
                {

                    ShapeFile s = new ShapeFile(ConnectionString, true);

                    layer.DataSource = s;
                }

                //_map.Layers.Add(layer);

                layer.Style.PointColor = new SolidBrush(dark);
                layer.Style.Line = new Pen(dark);
                layer.Style.Fill = new SolidBrush(dark);
                layer.Style.Outline = new Pen(new SolidBrush(light));
                layer.Style.EnableOutline = true;


                AddLayer(layer);


            }


            if (filterIdentifier.Name.Contains("OGR"))
            {

                try
                {

                    Ogr ds = new Ogr(ConnectionString);

                    if (ds.NumberOfLayers > 1)
                    {

                        LayerGroup layergroup = new LayerGroup(title);

                        for (int ln = 0; ln < ds.NumberOfLayers; ln++)
                        {
                            Ogr dsl = new Ogr(ConnectionString, ln);

                            VectorLayer l = new VectorLayer(title + " - " + dsl.LayerName);
                            l.DataSource = dsl;

                            layergroup.Layers.Add(l);
                        }


                        AddLayer(layergroup);


                    }
                    else
                    {
                        VectorLayer layer = new VectorLayer(title);

                        layer.DataSource = ds;

                        layer.Style.PointColor = new SolidBrush(dark);
                        layer.Style.Line = new Pen(dark);
                        layer.Style.Fill = new SolidBrush(dark);
                        layer.Style.Outline = new Pen(new SolidBrush(light));
                        layer.Style.EnableOutline = true;

                        AddLayer(layer);

                    }


                }
                catch (TypeInitializationException ex)
                {
                    if (ex.Message == "The type initializer for 'OSGeo.OGR.Ogr' threw an exception.")
                    {
                        throw new Exception(
                            String.Format(
                                "The application threw a PINVOKE exception. You probably need to copy the unmanaged dll's to your bin directory. They are a part of fwtools {0}. You can download it from: http://home.gdal.org/fwtools/",
                                GdalRasterLayer.FWToolsVersion));
                    }
                    throw;
                }

            }



            _mapBox.Map.ZoomToExtents();
            _mapBox.Refresh();
        }


        public void AddRaster()
        {
            FormAddLayer _formRaster = new FormAddLayer(FormAddLayer.ListType.Raster);

            _formRaster.ConnectionString = "";
            if (_formRaster.ShowDialog() == DialogResult.OK)
            {
                AddRaster(_formRaster.Identifier, _formRaster.ConnectionString);
            }
        }

        public void AddRaster(FileFilterIdentifier filterIdentifier, string ConnectionString)
        {

            if (filterIdentifier.Name.Contains("GDAL"))
            {

                string title = Path.GetFileNameWithoutExtension(ConnectionString);

                int i = 0;

                while (_map.Layers.GetLayerByName(title) != null)
                {
                    i++;
                    title = Path.GetFileNameWithoutExtension(ConnectionString) + i.ToString();
                }
                
                //string name = filterIdentifier.Name.Remove(0, 5);//remove "GDAL." berfore name

                GdalRasterLayer layer = new SharpMap.Layers.GdalRasterLayer(title, ConnectionString);

                _map.Layers.Add(layer);

                AddLayer(layer);

                _mapBox.Map.ZoomToExtents();
                _mapBox.Refresh();
            }
        }

  

        private void AddLayer(Layer layer)
        {
            MapEditorItem CurFodler = GetSelectedFolder();
            var obj = GetObject(CurFodler);

            if (obj is Map)
            {
                (obj as Map).Layers.Add(layer);
            }

            if (obj is LayerGroup)
            {
                (obj as LayerGroup).Layers.Add(layer);
            }

            MapEditorItem newItem = CurFodler.AddSubItem(layer);

            newItem.LayerChanged += new MapEditorItem.OnLayerChanged(item_LayerChanged);

            //if (layer is LayerGroup)
            //{
            //    foreach (MapEditorItem child in newItem.ChildItems)
            //    {
            //        child.LayerChanged += new MapEditorItem.OnLayerChanged(item_LayerChanged);
            //    }
            //}
        }


        public MapEditorItem GetSelectedFolder()
        {
            List<string> selectedItems = GetSelectedItems();

            MapEditorItem itm = null;

            if (selectedItems.Count > 0)
            {
                MapEditorItem itmToTest = GetLegendItem(selectedItems[0]);

                //Is the default value
                if (itmToTest.Object is Map)
                {
                    itm = itmToTest;
                }

                if (itmToTest.Object is Layer)
                {
                    itm = itmToTest.ParentItem;
                }

                if (itmToTest.Object is LayerGroup)
                {
                    itm = itmToTest;
                }
            }
            else
            {
                itm = GetLegendItem(MapName);
            }

            if (itm == null) throw (new Exception("Selecterd item not found!"));

            return itm;
        }

        public object GetObject(MapEditorItem itm)
        {
            return GetObject(itm.PathName);
        }

        public object GetObject(string path)
        {
            if (path == MapName) return _map;

            List<string> pats = new List<string>();
            
            pats.AddRange(path.Split('/'));

            if (pats[0] == MapName) pats.RemoveAt(0);

            return FindPath(pats, _map.Layers);
            
        }

        private object FindPath(List<string> pats, LayerCollection layers)
        {

            if (pats.Count == 1) return layers[pats[0]];
      
            LayerGroup lg = (LayerGroup)layers[pats[0]];

            pats.RemoveAt(0);

            return FindPath(pats, lg.Layers);

        }

        private object FindPath(List<string> pats, Collection<Layer> layers)
        {

            if (pats.Count == 1) return layers[0];

            LayerGroup lg = (LayerGroup)layers[0];

            pats.RemoveAt(0);

            return FindPath(pats, lg.Layers);

        }

        //public void AddLegendItem(ILayer layer)
        //{

        //    MapEditorItem itm = GetSelectedFolder();

        //    //AddItem(itm, layer);

        //    MapEditorItem newItem = itm.AddItem(layer);

        //    newItem.LayerChanged += new MapEditorItem.OnLayerChanged(item_LayerChanged);


        //    if (layer is LayerGroup)
        //    {
        //        foreach(MapEditorItem child in newItem.ChildItems)
        //        {
        //            child.LayerChanged += new MapEditorItem.OnLayerChanged(item_LayerChanged);
        //        }
        //    }
            

        //}

        void item_LayerChanged(MapEditorItem item)
        {
            _mapBox.Refresh();
        }

        public void CreateFromMap()
        {
                SuspendLayout();

                panel.Visible = false;
                panel.Controls.Clear();

                MapEditorItem LegendItem = new MapEditorItem(null,_map, MapName);

                AddLegendItem(LegendItem);

                panel.Visible = true;
                ResumeLayout();

                Invalidate();
                Refresh();
        }

        public void AddLegendItem(MapEditorItem item)
        {
            item.Dock = DockStyle.Top;

            item.ItemMouseDown += new MapEditorItem.OnItemMouseDown(item_ItemMouseDown);
            item.ItemMouseUp += new MapEditorItem.OnItemMouseUp(item_ItemMouseUp);
            item.ItemMouseMove += new MapEditorItem.OnItemMouseMove(item_MouseMove);

            panel.Controls.Add(item);

        }


        private bool isDragging = false;
        private MapEditorItem Highligted = null;
        private void item_ItemMouseUp(MapEditorItem sender, MouseEventArgs e)
        {
            //if (sender.Object is Map) return;

            Point p = PointToClient(new Point(e.X, e.Y));
            MapEditorItem target = GetItem(p);

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (target != null)
                {
                    if (target.Object is Map)
                    {
                        //FormMapProperites formMapProperties = new FormMapProperites();

                        //formMapProperties.ShowDialog();
                    }
                }
            }

            // Means Drag & drop is active
            if (isDragging)
            {
                if (target != null & target != sender)
                {
                    target.Highlight(false);

                    if (target.Object is LayerGroup)
                    {

                        object o = sender.ParentItem.Object;

                        if (o is Map)
                        {
                            (o as Map).Layers.Remove(sender.asLayer);
                        }
                        else if (o is LayerGroup)
                        {
                            (o as LayerGroup).Layers.Remove(sender.asLayer);
                        }

                        target.asLayergroup.Layers.Add(sender.asLayer);


                        // target.AddControl(sender);

                    }
                    else
                    if (target.Object is Layer)
                    {
                        int i = target.ParentItem.ChildItems.IndexOf(target);

                        if (sender.ParentItem.Object is Map)
                        {
                            (sender.ParentItem.Object as Map).Layers.Remove(sender.asLayer);
                        }
                        else if (sender.ParentItem.Object is LayerGroup)
                        {
                            (sender.ParentItem.Object as LayerGroup).Layers.Remove(sender.asLayer);
                        }

                        if (target.ParentItem.Object is Map)
                        {
                            (target.ParentItem.Object as Map).Layers.Insert(i, sender.asLayer);
                        }
                        else if (target.ParentItem.Object is LayerGroup)
                        {
                            (target.ParentItem.Object as LayerGroup).Layers.Insert(i, sender.asLayer);
                        }


                        if (target.ParentItem != sender.ParentItem)
                        {
                            //target.ParentItem.AddControl(sender);
                        }

                        target.ParentItem.ChildItems.SetChildIndex(sender, i);                        
                    }

                    if (target.Object is Map)
                    {
                        if (sender.ParentItem.Object is Map)
                        {
                            (sender.ParentItem.Object as Map).Layers.Remove(sender.asLayer);
                        }
                        else if (sender.ParentItem.Object is LayerGroup)
                        {
                            (sender.ParentItem.Object as LayerGroup).Layers.Remove(sender.asLayer);
                        }

                        (target.Object as Map).Layers.Insert(_map.Layers.Count, sender.asLayer);

                        //target.AddControl(sender);
                    }

                }
                //else
                //{
                //    MessageBox.Show("Same object");
                //}

                isDragging = false;

                Invalidate();
                Refresh();

                _mapBox.Refresh();
            }

        }

        private void item_ItemMouseDown(MapEditorItem sender, MouseEventArgs e)
        {
            if (!sender.Selected)
            {
                List<MapEditorItem> List = GetAllItems();
                foreach (MapEditorItem itm in List)
                {
                    itm.Selected = false;
                }

                sender.Selected = true;

            }
        }

        private void item_MouseMove(MapEditorItem sender, MouseEventArgs e)
        {
            Point p = this.PointToClient(new Point(e.X, e.Y));

            MapEditorItem target = GetItem(p);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (sender.Object is Map) return;

                if (target == null | target == sender)
                {
                    Cursor = CursorForbidden;
                }
                else
                {

                    Cursor = CursorDragDrop;
                    isDragging = true;
                    
                    if (Highligted != null)
                    {
                        if (Highligted != target)
                        {
                            Highligted.Highlight(false);
                        }
                    }

                    target.Highlight(true);
                    Highligted = target;
                
                }

            }
            else
            {
                Cursor = Cursors.Default;
            }

            Form t = Application.OpenForms[0];

            t.Text = "Screen: " + e.X.ToString() + " " + e.Y.ToString() + "   Client" + p.X.ToString() + " " + p.Y.ToString();


            if (target != null)
            {
                t.Text += " " + (target.PathName);
            }

            t.Text += "   Sender: " + (sender as MapEditorItem).PathName;
       
        }

     
        private MapEditorItem GetItem(int x, int y)
        {
            return GetItem(new Point(x, y));
        }

        private MapEditorItem GetItem(Point p)
        {
            List<MapEditorItem> List = GetAllItems();

            List.Reverse();

            foreach(MapEditorItem itm in List)
            {
                    Rectangle rc = itm.RectangleToScreen(itm.ClientRectangle);

                    rc = this.RectangleToClient(rc);

                    if (rc.Contains(p)) return itm;
            }


            return null;
        }

        private MapEditorItem GetLegendItem(String path)
        {

            List<MapEditorItem> List = GetAllItems();

            foreach (MapEditorItem itm in List)
            {
                if (itm.PathName == path) return itm;
            }

           return null;
        }

        public List<string> GetSelectedItems()
        {
            List<string> SelItems = new List<string>();

            foreach (MapEditorItem itm in panel.Controls)
            {
                SelItems.AddRange(itm.GetSelectedItems());
            }

            return SelItems;
        }

        public List<MapEditorItem> GetAllItems()
        {

            List<MapEditorItem> List = new List<MapEditorItem>();

            foreach (Control c in panel.Controls)
            {
                if (c is MapEditorItem)
                {
                    List.AddRange((c as MapEditorItem).GetAllItems());
                }
            }

            return List;
        }

    }
}
