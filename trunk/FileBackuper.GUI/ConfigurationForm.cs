using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FileBackuper.Model;

namespace FileBackuper.GUI
{
    public partial class ConfigurationForm : Form
    {
        /// <summary>
        /// Editovana konfigurace
        /// </summary>
        public Configuration Configuration { get; set; }

        public ConfigurationForm(Configuration config)
        {
            InitializeComponent();

            Configuration = config;
            SetUIComponentsValues();
        }

        /// <summary>
        /// Udalost vyvolana kliknutim na tlacitko "Save"
        /// Upravuje hodnoty v property <code>Configuration</code>
        /// </summary>
        public event DetailButtonHandler SaveButtonClicked;

        /// <summary>
        /// Udalost vyvolana kliknutim na tlacitko "Save and Close"
        /// Upravuje hodnoty v property <code>Configuration</code>
        /// </summary>
        public event DetailButtonHandler SaveAndCloseButtonClicked;

        /// <summary>
        /// Udalost vyvolana klinutim na tlacitko "Close"
        /// NEupravuje hodnoty v property <code>Configration</code>!
        /// </summary>
        public event DetailButtonHandler CloseButtonClicked;

        /// <summary>
        /// Nastavi hodnoty UI komponent z profilu
        /// </summary>
        private void SetUIComponentsValues()
        {
            fsrConfigPath.Value = Configuration.ConfigPath;
            dsrLogDir.Value = Configuration.LogDirPath;
        }

        /// <summary>
        /// Opak k <code>SetUIComponentsValues</code>
        /// </summary>
        private void GetUIComponentsValues()
        {
            Configuration.ConfigPath = fsrConfigPath.Value;
            Configuration.LogDirPath = dsrLogDir.Value;
        }

        /// <summary>
        /// Zprostredkovava volani handleru
        /// </summary>
        /// <param name="dlg">Delegate</param>
        /// <param name="pList">Parametry</param>
        protected void Fire(Delegate dlg, params object[] pList)
        {
            if (dlg != null)
            {
                this.BeginInvoke(dlg, pList);
            }
        }

        protected virtual void btnClose_Click(object sender, EventArgs e)
        {
            Fire(CloseButtonClicked, this, e);
        }

        protected virtual void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            GetUIComponentsValues();
            Fire(SaveAndCloseButtonClicked, this, e);
        }

        protected virtual void btnSave_Click(object sender, EventArgs e)
        {
            GetUIComponentsValues();
            Fire(SaveButtonClicked, this, e);
        }

        private void btnNewConfigPath_Click(object sender, EventArgs e)
        {
            if (ofdNewConfigPath.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = ofdNewConfigPath.FileName;
                if (!File.Exists(file))
                {
                    using (StreamWriter sw = new StreamWriter(File.OpenWrite(file)))
                    {
                        sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                        sw.WriteLine("<profiles version=\"1\">");
                        sw.WriteLine("</profiles>");
                    }
                    Configuration.ConfigPath = file;
                    fsrConfigPath.Value = file;
                }
                else
                {
                    MessageBox.Show("You must select not existing file!", "Profiles file");
                }
            }
        }
    }
}
