using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using FileBackuper.Model;

namespace FileBackuper.GUI
{
    public partial class MainForm : Form
    {
        private ProfileDetail frmProfileDetail;

        public MainForm()
        {
            InitializeComponent();
            this.Text += " - " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public void OpenProfileDetail(Profile profile)
        {
            if (frmProfileDetail == null)
            {
                frmProfileDetail = new ProfileDetail(profile);
            }
            if (frmProfileDetail.WindowState == FormWindowState.Minimized)
            {
                frmProfileDetail.WindowState = FormWindowState.Normal;
            }
            if (!frmProfileDetail.Visible)
            {
                frmProfileDetail.Show();
            }
            frmProfileDetail.BringToFront();
            frmProfileDetail.FormClosed += delegate { frmProfileDetail = null; };
            frmProfileDetail.SaveButtonClicked += delegate
            {
                
            };
            frmProfileDetail.SaveAndCloseButtonClicked += delegate
            {
                frmProfileDetail.Close();
            };
            frmProfileDetail.CloseButtonClicked += delegate
            {
                frmProfileDetail.Close();
            };
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Profile p = new Profile();
            p.Name = "New Profile";
            p.Period = TimePeriod.OneMonth;
            OpenProfileDetail(p);
        }
    }
}
