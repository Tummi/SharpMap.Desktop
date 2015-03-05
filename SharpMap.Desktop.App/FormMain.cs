using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SharpMap;
using SharpMap.Desktop;
using SharpMap.Forms;

namespace SharpMap.Desktop.App
{
    public partial class FormMain : Form
    {
        Project theProject = new Project();
        ProjectToolbar projectToolbar = new ProjectToolbar();

        Panel mapEditorPanel = new Panel();
        MapEditor mapEditor = new MapEditor();

        MapBox mapBox = new MapBox();

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            theProject.AddMap("map");
            
            projectToolbar.Dock = DockStyle.Top;
            mapEditor.Dock = DockStyle.Fill;

            projectToolbar.RenderMode = ToolStripRenderMode.System;

            projectToolbar.MapEditor = mapEditor;
            projectToolbar.project = theProject;


            mapEditor.MapBox = mapBox;

                mapEditorPanel.Dock = DockStyle.Fill;
            mapEditorPanel.BorderStyle = BorderStyle.Fixed3D;
            
            mapBox.Dock = DockStyle.Fill;
            mapBox.BackColor = Color.White;

            mapEditorPanel.Controls.Add(mapBox);

            splitContainer.Panel1.Controls.Add(mapEditor);
            splitContainer.Panel1.Controls.Add(projectToolbar);

            splitContainer.Panel2.Controls.Add(mapEditorPanel);


            //projectEditor.Dock = DockStyle.Fill;
            //projectEditor.MapBox = mapBox;
            //projectEditor.project = theProject;

            

            //splitContainer.Panel1.Controls.Add(projectEditor);
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theProject = new Project();
            theProject.Load();
            //projectEditor.project = theProject;
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theProject.Save();
        }

        private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theProject.Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
