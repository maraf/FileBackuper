using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FileBackuper.Controls
{
    public partial class DirectorySelector : UserControl
    {
        public Button Button { get { return btnOpenDirectory; } }
        public TextBox TextBox { get { return tbxOpenDirectory; } }
        public FolderBrowserDialog FolderBrowserDialog { get { return fbdOpenDirectory; } }

        public string Value
        {
            get { return TextBox.Text; }
            set
            {
                TextBox.Text = value;
                FolderBrowserDialog.SelectedPath = value;
            }
        }

        public DirectorySelector()
        {
            InitializeComponent();
        }

        private void btnOpenDirectory_Click(object sender, EventArgs e)
        {
            if (fbdOpenDirectory.ShowDialog() == DialogResult.OK)
            {
                TextBox.Text = fbdOpenDirectory.SelectedPath;
            }
        }
    }
}
