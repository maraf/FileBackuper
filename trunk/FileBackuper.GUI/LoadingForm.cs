using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileBackuper.GUI
{
    public partial class LoadingForm : Form
    {
        /// <summary>
        /// Inicializuje okno
        /// </summary>
        public LoadingForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Inizializuje okno a nastaví text zprávy
        /// </summary>
        /// <param name="message">text zprávy</param>
        public LoadingForm(string message)
            : this()
        {
            lblMessage.Text = message;
        }

        private void tmrInterval_Tick(object sender, EventArgs e)
        {
            pgbProgress.Value = ((pgbProgress.Value == pgbProgress.Maximum) ? pgbProgress.Minimum : (pgbProgress.Value + pgbProgress.Step));
        }
    }
}
