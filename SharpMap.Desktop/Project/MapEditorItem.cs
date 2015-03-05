using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpMap.Layers;
using SharpMap.Data.Providers;
using SharpMap.Styles;
using SharpMap.Rendering;
using GeoAPI.Geometries;

namespace SharpMap.Desktop
{

    public enum EditorItemType
    {
        None = 0,
        Point = 1,
        Line = 2,
        Polygon = 3,
        Raster = 4
    }

    public partial class MapEditorItem : UserControl
    {
        public List<EditorItemType> EditorItemTypes
        {
            get;
            set;
        }
     
        public bool ExpandedCollpaseVisible
        {
            get
            {
                return panelExpander.Visible;
            }
            set
            {
                panelExpander.Visible = value;
            }

        }

        private bool _expanded = true;
        public bool Expanded
        {
            get
            {
                return _expanded;
            }
            set
            {
                _expanded = value;

                if (_expanded)
                {
                    pictureExpander.Image = Resources.minus;
                    panelSub.Visible = true;  
                }
                else
                {
                    pictureExpander.Image = Resources.plus;
                    panelSub.Visible = false;
                }

                ComputeHeight();
            }


        }

        public bool CollapseExpandVisible
        {
            get
            {
                return panelExpander.Visible;
            }
            set
            {
                panelExpander.Visible = value;
            }
        }

        public bool CheckVisible
        {
            get
            {
                return panelCheck.Visible;
            }
            set
            {
                panelCheck.Visible = value;
            }
        }

        public bool IconVisible
        {
            get
            {
                return pictureBoxIcon.Visible;
            }
            set
            {
                pictureBoxIcon.Visible = value;
            }
        }

        //public bool SampleVisible
        //{
        //    get
        //    {
        //        return panelSample.Visible;
        //    }
        //    set
        //    {
        //        panelSample.Visible = value;
        //    }

        //}

        public bool SubVisible
        {
            get
            {
                return panelSub.Visible;
            }
            set
            {
                panelSub.Visible = value;
            }

        }

        public Image Icon
        {
            get
            {
                return pictureBoxIcon.Image;
            }
            set
            {
                pictureBoxIcon.Image = value;
            }
        }

        private bool _Selected = false;
        public bool Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                _Selected = value;

                if (_Selected)
                {
                    label.BackColor = SystemColors.Highlight;
                    label.ForeColor = SystemColors.HighlightText;
                }
                else
                {
                    label.BackColor = Color.Transparent;
                    label.ForeColor = SystemColors.ControlText;
                }

            }
        }

        private Style _style = null;
        public Style Style
        {
            get
            {
                return _style;
            }
            set
            {
                _style = value;
                Invalidate();
                Refresh();
            }
        }

        public string PathName
        {
            get
            {
                if(ParentItem != null)
                {
                    return ParentItem.PathName + "/" + Text;
                }

                return Text;
            }
           
        }

        private MapEditorItem _parentitem = null;
        public MapEditorItem ParentItem
        {
            get
            {
                return _parentitem;
            }
            protected set
            {
                _parentitem = value;
            }
        }

        public Control.ControlCollection ChildItems
        {
            get
            {
                return panelSub.Controls;
            }
        }

        public MapEditorItem()
        {
            InitializeComponent();
            Initialize();

            ComputeHeight();
        }

        public MapEditorItem(MapEditorItem parent, Map map,  string mapname)
        {
            InitializeComponent();
            Initialize();

            _parentitem = parent;

            label.Text = mapname;

            CollapseExpandVisible = false;
            CheckVisible = false;
            //SampleVisible = false;

            Icon = Resources.map16;
            IconVisible = true;

            foreach (ILayer l in map.Layers)
            {
                AddSubitem(new MapEditorItem(this, l));
            }

            _object = map;

            ComputeHeight();
        }

        public MapEditorItem(MapEditorItem parent, ILayer layer)
        {
            InitializeComponent();
            Initialize();

            _object = layer;

            _parentitem = parent;

            label.Text = layer.LayerName;

            checkBox.Checked = layer.Enabled;

            if (layer is LayerGroup)
            {
                CreateFrom(layer as LayerGroup);
            } else

            if (layer is VectorLayer)
            {
                CreateFrom(layer as VectorLayer);
            } else

            if (layer is GdalRasterLayer)
            {
                CreateFrom(layer as GdalRasterLayer);
            }
      
            ComputeHeight();

        }


        private void CreateFrom(LayerGroup layerGroup)
        {
            Icon = Resources.folder;
            IconVisible = true;
            //SampleVisible = false;

            foreach (ILayer l in layerGroup.Layers)
            {
                AddSubitem(new MapEditorItem(this, l));
            }

        }

        private void CreateFrom(VectorLayer layer)
        {
            if (layer.DataSource is ShapeFile)
            {
                switch ((layer.DataSource as ShapeFile).ShapeType)
                {
                    case (ShapeType.Point):
                    case (ShapeType.PointM):
                    case (ShapeType.PointZ):
                    case (ShapeType.Multipoint): // Multipoint Not MultiPoint ?
                    case (ShapeType.MultiPointM):
                    case (ShapeType.MultiPointZ):
                        {
                            EditorItemTypes.Add(EditorItemType.Point);
                        }
                        break;
                    case (ShapeType.PolyLine):
                    case (ShapeType.PolyLineM):
                    case (ShapeType.PolyLineZ):
                        {
                            EditorItemTypes.Add(EditorItemType.Line);
                        }
                        break;
                    case (ShapeType.Polygon):
                    case (ShapeType.PolygonM):
                    case (ShapeType.PolygonZ):
                        {
                            EditorItemTypes.Add(EditorItemType.Polygon);
                        }
                        break;
                }
            }

            if (layer.DataSource is Ogr)
            {
                Ogr ogr = layer.DataSource as Ogr;

                int lays = ogr.NumberOfLayers;

                if(ogr.OgrGeometryTypeString.Contains("wkbPoint"))
                {
                    EditorItemTypes.Add(EditorItemType.Point);
                }
                
                if(ogr.OgrGeometryTypeString.Contains("wkbLineString"))
                {
                    EditorItemTypes.Add(EditorItemType.Line);
                }
                        
                if(ogr.OgrGeometryTypeString.Contains("wkbPolygon"))
                {
                    EditorItemTypes.Add(EditorItemType.Polygon);
                }
             
            }

            if (layer.Themes == null)
            {


                pictureBoxIcon.Width = 35;
                pictureBoxIcon.Image = GetSampleImage(pictureBoxIcon.Size, layer.Style);
                //MapEditorSubItem SubItem = new MapEditorSubItem(this, EditorItemTypes, layer.Style, "");

                //SubItem.SubItemChanged += new MapEditorSubItem.OnSubItemChanged(SubItem_StyleChanged);

                //AddControl(SubItem);
            }

        }

        private void CreateFrom(GdalRasterLayer layer)
        {
            EditorItemTypes.Add(EditorItemType.Raster);

            Icon = Resources.raster;
            CollapseExpandVisible = false;
            //SampleVisible = false;

            panelSub.Visible = false;

        }


        public MapEditorItem(MapEditorItem parent, List<EditorItemType> editorItemTypes, Style style, string text)
        {
            InitializeComponent();

            _parentitem = parent;
            _style = style;

            EditorItemTypes = editorItemTypes;
            Text = text;


            Initialize();

        }


        private Image GetSampleImage(Size size, VectorStyle style)
        {
            Bitmap bmp = new Bitmap(size.Width,size.Height);

            Graphics g = Graphics.FromImage(bmp);

                
                foreach (EditorItemType t in EditorItemTypes)
                {

                    switch (t)
                    {
                        case EditorItemType.Point:
                            {

                                Point Center = new Point(size.Width / 2,
                                                         size.Height / 2);

                                if (style.PointSymbolizer != null)
                                {
                                    //VectorRenderer.DrawPoint(style.PointSymbolizer, g, Center, map);

                                }
                                else
                                {
                                    if (style.Symbol != null || style.PointColor == null)
                                    {
                                        //VectorRenderer.DrawPoint(g, (IPoint)feature, style.Symbol, style.SymbolScale, style.SymbolOffset,
                                        //                         style.SymbolRotation, map);
                                    }
                                    else
                                    {
                                        
                                        //VectorRenderer.DrawPoint(g, Center, style.PointColor, style.PointSize, style.SymbolOffset);
                                    }

                                }
                            }
                            break;
                        case EditorItemType.Line:
                            {
                                g.DrawLine(style.Line, 0, size.Height / 2, size.Width, size.Height / 2);
                            }
                            break;
                        case EditorItemType.Polygon:
                            {
                                Rectangle rect = new Rectangle(new System.Drawing.Point(0, 0), size);

                                g.FillRectangle(style.Fill, rect);
                                if (style.EnableOutline)
                                {
                                    rect.Inflate(-(int)(style.Outline.Width), -(int)(style.Outline.Width));

                                    g.DrawRectangle(style.Outline, rect);
                                }
                            }
                            break;
                    }


                }

                g.Dispose();

                return bmp;
            

        }


        void SubItem_StyleChanged()
        {
            if (LayerChanged != null)
            {
                LayerChanged(this);
            }
        }

        private void Initialize()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            EditorItemTypes = new List<EditorItemType>();
        }
        
        private object _object = null;
        public object Object
        {
            get
            {
                return _object;
            }
        }

        public Layer asLayer
        {
            get
            {
                if (_object is Layer) 
                    return _object as Layer;

                throw new Exception("Object is not a Layer");
            }
        }

        public LayerGroup asLayergroup
        {
            get
            {
                if (_object is LayerGroup)
                    return _object as LayerGroup;

                throw new Exception("Object is not a LayerGroup");
            }
        }

        public Map asMap
        {
            get
            {
                if (_object is Map)
                    return _object as Map;

                throw new Exception("Object is not a Map");
            }
        }

        public new string Text
        {
            get
            {
                return label.Text;
            }
            set
            {
                label.Text = value;
            }
        }

        public void Highlight(bool highlight)
        {
            if (highlight)
            {
                if (panelLabel.BackColor != SystemColors.ButtonHighlight)
                {
                    panelLabel.BackColor = SystemColors.ButtonHighlight;
                }

                if (panelLabel.BorderStyle != BorderStyle.FixedSingle)
                {
                    panelLabel.BorderStyle = BorderStyle.FixedSingle;
                }
            }
            else
            {
                if (panelLabel.BackColor != Color.Transparent)
                {
                    panelLabel.BackColor = Color.Transparent;
                }

                if (panelLabel.BorderStyle != BorderStyle.None)
                {
                    panelLabel.BorderStyle = BorderStyle.None;
                }
            }
        }

        public delegate void OnLayerChanged(MapEditorItem item);
        public event OnLayerChanged LayerChanged;

        public List<MapEditorItem> GetAllItems()
        {
            List<MapEditorItem> lst = new List<MapEditorItem>();

            GetAllItems(ref lst);

            return lst;
        }

        private void GetAllItems(ref List<MapEditorItem> list)
        {

            list.Add(this);

            foreach (Control c in panelSub.Controls)
            {
                if (c is MapEditorItem)
                {
                    (c as MapEditorItem).GetAllItems(ref list);
                }
            }

        }

        public List<string> GetSelectedItems()
        {
            List<string> SelItems = new List<string>();
            
            if(Selected) 
            {
                SelItems.Add(PathName);
            }
            else
            {
                foreach(Control c in panelSub.Controls)
                {
                    if (c is MapEditorItem)
                    {
                        ((MapEditorItem)c).GetSelectedItems(PathName, ref SelItems);
                    }
                }
            }

            return SelItems;
        }

        private void GetSelectedItems(string path, ref List<string> SelItems)
        {
            if (Selected)
            {
                SelItems.Add(path + "/" + Text);
            }
            else
            {
               foreach(Control c in panelSub.Controls)
                {
                    if (c is MapEditorItem)
                    {
                        ((MapEditorItem)c).GetSelectedItems(PathName, ref SelItems);
                    }
                }
            }
        }

        public MapEditorItem GetItem(string path)
        {
                if (path == Text)
                {
                    return this;
                }
                
                if(path.Substring(0,Text.Length) == Text)
                {
                    int numchar = path.Length-Text.Length+1;
                    
                    string subPath = path.Substring(path.Length-numchar, numchar);

                    foreach (MapEditorItem itm in panelSub.Controls)
                    {
                       return itm.GetItem(subPath);
                    }

                }
                

            return null;
        }

        public MapEditorItem AddSubItem(ILayer layer)
        {
            MapEditorItem item = new MapEditorItem(this, layer);

            AddSubitem(item);

            return item;
        }

        //public MapEditorItem AddItem(Map map, string mapname)
        //{
        //    MapEditorItem item = new MapEditorItem(this, map , mapname);
        //    AddSubitem(item);

        //    return item;
        //}

        public void AddSubitem(MapEditorItem item)
        {
            item.Dock = DockStyle.Top;
            item.ParentItem = this;

            item.ItemMouseDown += new OnItemMouseDown(control_MouseDown);
            item.ItemMouseUp += new OnItemMouseUp(control_MouseUp);
            item.ItemMouseClick += new OnItemMouseClick(control_MouseClick);
            item.ItemMouseMove += new OnItemMouseMove(control_MouseMove);

            panelSub.Controls.Add(item);

            ComputeHeight();

        }
               
        //public void AddControl(Control item)
        //{
            
        //    if (item.Parent != null) item.Parent = null;
            

        //    item.Dock = DockStyle.Top;

        //    if (item is MapEditorItem)
        //    {
        //        MapEditorItem itm = item as MapEditorItem;

        //        itm.ParentItem = this;

        //        itm.ItemMouseDown += new OnItemMouseDown(control_MouseDown);
        //        itm.ItemMouseUp += new OnItemMouseUp(control_MouseUp);
        //        itm.ItemMouseClick += new OnItemMouseClick(control_MouseClick);
        //        itm.ItemMouseMove += new OnItemMouseMove(control_MouseMove);
        //        //itm.ItemMouseEnter += new OnItemMouseEnter(control_MouseEnter);
        //        //itm.ItemMouseLeave += new OnItemMouseLeave(control_MouseLeave);


        //    }

        //    if (item is MapEditorSubItem)
        //    {
        //        MapEditorSubItem StyleEditor = item as MapEditorSubItem;

        //        StyleEditor.LegendStyleItemMouseDown += new MapEditorSubItem.OnLegendStyleItemMouseDown(control_MouseDown);
        //        StyleEditor.LegendStyleItemMouseUp += new MapEditorSubItem.OnLegendStyleItemMouseUp(control_MouseUp);
        //        StyleEditor.LegendStyleItemMouseClick += new MapEditorSubItem.OnLegendStyleItemMouseClick(control_MouseClick);
        //        StyleEditor.LegendStyleItemMouseMove += new MapEditorSubItem.OnLegendStyleItemMouseMove(control_MouseMove);
        //        //StyleEditor.LegendStyleItemMouseEnter += new MapEditorSubItem.OnLegendStyleItemMouseEnter(control_MouseEnter);
        //        //StyleEditor.LegendStyleItemMouseLeave += new MapEditorSubItem.OnLegendStyleItemMouseLeave(control_MouseLeave);
        //    }

        //    panelSub.Controls.Add(item);

        //    ComputeHeight();

        //}
     
        public void ComputeHeight()
        {
            int height = pictureExpander.Height + Padding.Top + Padding.Bottom;

            if (_expanded)
            {
                if (panelSub.Controls.Count > 0)
                {
                    foreach (Control itm in panelSub.Controls)
                    {
                        height += itm.Height;
                    }
                }
            }

            Height = height;

            if (ParentItem != null) ParentItem.ComputeHeight();
        }

        public Rectangle GetDropRectangle()
        {
            if(_object is Map | _object is LayerGroup | _object is LayerCollection)
            {
                return new Rectangle(panelLabel.Left, panelLabel.Top, panelLabel.Width, panelLabel.Height);
            }

            //if (_object is Layer)
            //{
            //    return new Rectangle(ClientRectangle.Left, ClientRectangle.Top + ClientRectangle.Height - 3, ClientRectangle.Width, 3);
            //}

            //return new Rectangle(ClientRectangle.Left, ClientRectangle.Top + ClientRectangle.Height - 3, ClientRectangle.Width, 3);
            return ClientRectangle;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            Expanded = !Expanded;
        }

        public delegate void OnItemMouseClick(MapEditorItem sender, MouseEventArgs e);
        public event OnItemMouseClick ItemMouseClick;
        private void control_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender is MapEditorItem)
            {
                if (ItemMouseClick != null)
                {
                    ItemMouseClick(sender as MapEditorItem, e);
                }
            }
            else
            {
                Point ps = ((Control)sender).PointToScreen(new Point(e.X, e.Y));

                MouseEventArgs ea = new MouseEventArgs(e.Button, e.Clicks, ps.X, ps.Y, e.Delta);

                if (ItemMouseClick != null)
                {
                    ItemMouseClick(this, ea);
                }
            }
        }

        public delegate void OnItemMouseDown(MapEditorItem sender, MouseEventArgs e);
        public event OnItemMouseDown ItemMouseDown;
        private void control_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is MapEditorItem)
            {
                if (ItemMouseDown != null)
                {
                    ItemMouseDown(sender as MapEditorItem, e);
                }
            }
            else
            {
                Point ps = ((Control)sender).PointToScreen(new Point(e.X, e.Y));

                MouseEventArgs ea = new MouseEventArgs(e.Button, e.Clicks, ps.X, ps.Y, e.Delta);

                if (ItemMouseDown != null)
                {
                    ItemMouseDown(this, ea);
                }
            }
        }

        public delegate void OnItemMouseUp(MapEditorItem sender, MouseEventArgs e);
        public event OnItemMouseUp ItemMouseUp;
        private void control_MouseUp(object sender, MouseEventArgs e)
        {

            if (sender is MapEditorItem)
            {
                if (ItemMouseUp != null)
                {
                    ItemMouseUp(sender as MapEditorItem, e);
                }
            }
            else
            {
                Point ps = ((Control)sender).PointToScreen(new Point(e.X, e.Y));

                MouseEventArgs ea = new MouseEventArgs(e.Button, e.Clicks, ps.X, ps.Y, e.Delta);

                if (ItemMouseUp != null)
                {
                    ItemMouseUp(this, ea);
                }
            }
        }


        public delegate void OnItemMouseMove(MapEditorItem sender, MouseEventArgs e);
        public event OnItemMouseMove ItemMouseMove;
        private void control_MouseMove(object sender, MouseEventArgs e)
        {

            if (sender is MapEditorItem)
            {
                if (ItemMouseMove != null)
                {
                    ItemMouseMove(sender as MapEditorItem, e);
                }
            }
            else
            {
                Point ps = ((Control)sender).PointToScreen(new Point(e.X, e.Y));

                MouseEventArgs ea = new MouseEventArgs(e.Button, e.Clicks, ps.X, ps.Y, e.Delta);

                if (ItemMouseMove != null)
                {
                    ItemMouseMove(this, ea);
                }
            }
        }

       
        private void checkBox_Click(object sender, EventArgs e)
        {
            (_object as Layer).Enabled = checkBox.Checked;

            if (LayerChanged != null)
            {
                LayerChanged(this);
            }
        }


        #region unused now
        //public delegate void OnItemMouseEnter(MapEditorItem sender, EventArgs e);
        //public event OnItemMouseEnter ItemMouseEnter;
        //private void control_MouseEnter(object sender, EventArgs e)
        //{
        //    if (ItemMouseEnter != null)
        //    {
        //        if (sender is MapEditorItem)
        //        {
        //            ItemMouseEnter((MapEditorItem)sender, e);
        //        }
        //        else
        //        {
        //            ItemMouseEnter(this, e);
        //        }
        //    }
        //}

        //public delegate void OnItemMouseLeave(MapEditorItem sender, EventArgs e);
        //public event OnItemMouseLeave ItemMouseLeave;
        //private void control_MouseLeave(object sender, EventArgs e)
        //{
        //    if (ItemMouseLeave != null)
        //    {
        //        if (sender is MapEditorItem)
        //        {
        //            ItemMouseLeave((MapEditorItem)sender, e);
        //        }
        //        else
        //        {
        //            ItemMouseLeave(this, e);
        //        }
        //    }

        //}                

        #endregion
    }
}
