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
    public partial class FormText : Form
    {
        public FormText()
        {
            InitializeComponent();
        }

        public static string Show(string FormTitle, string LabelText, string DefaultValue)
        {
            FormText TextDlg = new FormText();

            TextDlg.SetFormTitle(FormTitle);
            TextDlg.SetLabelText(LabelText);
            TextDlg.SetDefaultValue(DefaultValue);

            if (TextDlg.ShowDialog() == DialogResult.OK)
            {
                return TextDlg.GetValue();
            }

            return null;
        }

        public void SetFormTitle(String FormTitle)
        {
            if (FormTitle != null) this.Text = FormTitle;
        }

        public void SetLabelText(String LabelText)
        {
            if (LabelText != null) label.Text = LabelText;
        }

        public void SetDefaultValue(String DefaultValue)
        {
            if (DefaultValue != null) textBox.Text = DefaultValue;
        }

        public string GetValue()
        {
            return textBox.Text;
        }

        private void FormGetText_Load(object sender, EventArgs e)
        {

        }
    }
}
