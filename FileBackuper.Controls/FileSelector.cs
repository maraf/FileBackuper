using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FileBackuper.Controls
{
    /// <summary>
    /// Komponenta pro vyber souboru
    /// </summary>
    public partial class FileSelector : UserControl
    {
        public Button Button { get { return btnOpenFile; } }
        public TextBox TextBox { get { return tbxOpenFile; } }
        public OpenFileDialog OpenFileDialog { get { return ofdOpenFile; } }

        public String Value
        {
            get { return TextBox.Text; }
            set
            {
                TextBox.Text = value;
                OpenFileDialog.FileName = value;
            }
        }

        public FileSelector()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (ofdOpenFile.ShowDialog() == DialogResult.OK)
            {
                tbxOpenFile.Text = ofdOpenFile.FileName;
            }
        }
    }
}
