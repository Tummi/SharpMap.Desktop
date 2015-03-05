using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpMap.Forms;

namespace SharpMap.Desktop
{
    public partial class ProjectEditor : UserControl
    {
        public ProjectEditor()
        {
            InitializeComponent();
        }

        private Project _project;
        public Project project
        {
            get { return _project; }
            set { _project = value; CreateFromProject(); }
        }

        public MapBox MapBox
        {
            get
            {
                return mapEditor.MapBox;
            }
            set
            {
                mapEditor.MapBox = value;
            }
        }

        private void CreateFromProject()
        {
            toolStripComboBoxMaps.Items.Clear();
            foreach(string mapName in _project.maps.Keys) toolStripComboBoxMaps.Items.Add(mapName);
            
            if (toolStripComboBoxMaps.Items.Count > 0) 
                toolStripComboBoxMaps.SelectedIndex = 0;

            SetCurrentMap();
        }

        private void SetCurrentMap()
        {
            if (toolStripComboBoxMaps.Items.Count > 0)
            {
                string mapname = toolStripComboBoxMaps.SelectedItem.ToString();

                mapEditor.SetMap(mapname, project.maps[mapname]);
            }

        }

        private void addMapToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int n = _project.maps.Count;

            string MapName = FormText.Show("Add new map", "Map name:", "Map #"+(n+1).ToString());

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
            mapEditor.AddVector();
        }

        private void rasterLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapEditor.AddRaster();
        }

        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapEditor.AddFolder();
        }

        private void toolStripComboBoxMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            // int a = 1;
        }

        private void toolStripComboBoxMaps_Click(object sender, EventArgs e)
        {
            SetCurrentMap();
        }

    }
}
