using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpMap.Desktop
{
    public partial class FormAddLayer : Form
    {

        FileFilterList _FilterList = new FileFilterList();

        public FileFilterList FilterList
        {
            get
            {
                return _FilterList;
            }
        }

        public FileFilterIdentifier Identifier
        {
            get
            {
                return _FilterList[comboBoxTypeList.SelectedIndex];
            }
        }

        public string ConnectionString
        {
            get
            {
                return textBoxConnectionString.Text;
            }
            set
            {
                textBoxConnectionString.Text = value;
            }

        }

        public FormAddLayer()
        {
            InitializeComponent();
        }

        public enum ListType
        {
            Vector,
            Raster
        }

        public FormAddLayer(ListType listType)
        {
            InitializeComponent();

            switch (listType)
            {
                case ListType.Raster:
                    {
                        _FilterList.InitializeRaster();
                    }
                    break;
                case ListType.Vector:
                    {
                        _FilterList.InitializeVector();
                    }
                    break;
            }

            RefreshCombo();
        }

        public void RefreshCombo()
        {
            comboBoxTypeList.Items.Clear();

            foreach (FileFilterIdentifier identifier in FilterList)
                comboBoxTypeList.Items.Add(identifier.Description);

            if (comboBoxTypeList.Items.Count > 0) comboBoxTypeList.SelectedIndex = 0;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            FileFilterIdentifier identifier = FilterList[comboBoxTypeList.SelectedIndex];

            switch (identifier.FilterType)
            {

                case FileFilterType.File:
                    {
                        openFileDialog.FileName = "";
                        openFileDialog.Filter = identifier.Filter;

                        if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            textBoxConnectionString.Text = openFileDialog.FileName;
                        }
                    }
                    break;

                case FileFilterType.Folder:
                    {
                        folderBrowserDialog.ShowNewFolderButton = true;
                        if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            textBoxConnectionString.Text = folderBrowserDialog.SelectedPath;
                        }
                    }
                    break;
            }
        }

        private void FormAddLayer_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxConnectionString.Text == "")
            {
                MessageBox.Show("Connection empty");
                e.Cancel = true;
            }
        }

       


    }
}
