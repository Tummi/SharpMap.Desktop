using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpMap.Desktop
{
    public class ProjectToolbar: ToolStrip
    {
        private Project _project;
        public Project project
        {
            get { return _project; }
            set { 
                _project = value; 
                CreateFromProject(); 
            }
        }


        private MapEditor _mapEditor;
        public MapEditor MapEditor
        {
            get { return _mapEditor; }
            set { _mapEditor = value; }
        }



        ToolStripComboBox toolStripComboBoxMaps = new ToolStripComboBox();
        ToolStripSplitButton toolStripSplitButtonAdd = new ToolStripSplitButton();
        ToolStripButton toolStripButtonPage = new ToolStripButton();

        public ProjectToolbar() :
            base()
        {
            System.Drawing.Image add = Resources.Add;


            ToolStripItem[] ButtonAdItems = new ToolStripItem[]
            {
                new ToolStripMenuItem("Map...",null, addMapToolStripMenuItem_Click),
                new ToolStripMenuItem("Folder...",null, addFolderToolStripMenuItem_Click),
                new ToolStripSeparator(),
                new ToolStripMenuItem("Vector...",null, vectorLayerToolStripMenuItem_Click),
                new ToolStripMenuItem("Raster...",null, rasterLayerToolStripMenuItem_Click),
            };

            toolStripComboBoxMaps.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboBoxMaps.Click += toolStripComboBoxMaps_Click;
            toolStripComboBoxMaps.SelectedIndexChanged += toolStripComboBoxMaps_SelectedIndexChanged;
            
            toolStripSplitButtonAdd = new ToolStripSplitButton("Add", add, ButtonAdItems);

            Items.Add(toolStripComboBoxMaps);
            Items.Add(toolStripSplitButtonAdd);
        }


        private void CreateFromProject()
        {
            toolStripComboBoxMaps.Items.Clear();

            if (_project != null)
            {
                foreach (string mapName in _project.maps.Keys) toolStripComboBoxMaps.Items.Add(mapName);

                if (toolStripComboBoxMaps.Items.Count > 0)
                    toolStripComboBoxMaps.SelectedIndex = 0;

                SetCurrentMap();
            }
        }

        private void SetCurrentMap()
        {
            if (toolStripComboBoxMaps.Items.Count > 0)
            {
                string mapname = toolStripComboBoxMaps.SelectedItem.ToString();

                _mapEditor.SetMap(mapname, project.maps[mapname]);
            }

        }

        private void addMapToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int n = _project.maps.Count;

            string MapName = FormText.Show("Add new map", "Map name:", "Map #" + (n + 1).ToString());

            if (MapName != null)
            {
                try
                {
                    _project.AddMap(MapName);
                    toolStripComboBoxMaps.Items.Add(MapName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void vectorLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mapEditor.AddVector();
        }

        private void rasterLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mapEditor.AddRaster();
        }

        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mapEditor.AddFolder();
        }

        private void toolStripComboBoxMaps_Click(object sender, EventArgs e)
        {
            //SetCurrentMap();
        }

        void toolStripComboBoxMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCurrentMap();
        }

    }
}
