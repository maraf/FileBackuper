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

        private ProfileManager manager = new ProfileManager(@"C:\Temp\FileBackuper\Config.xml");

        private int SelectedProfileIndex = -1;

        public MainForm()
        {
            InitializeComponent();
            this.Text += " - " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            manager.Load();

            lblCount.Text = String.Format("{0} profile(s)", manager.Profiles.Count);

            try
            {
                lvwProfiles.BeginUpdate();
                foreach (Profile profile in manager.Profiles)
                {
                    lvwProfiles.BeginUpdate();
                    string[] cols = { profile.Name, profile.OutputFolder, profile.Disabled ? "Disabled" : "Enabled" };
                    ListViewItem lvi = new ListViewItem(cols);
                    lvwProfiles.Items.Add(lvi);
                }
            }
            finally
            {
                lvwProfiles.EndUpdate();
            }
        }

        /// <summary>
        /// Zobrazi hlasku v pravem dolnim rohu
        /// </summary>
        /// <param name="message">Obsah hlasky</param>
        /// <param name="type">Typ hlasky (barva)</param>
        public void ShowMessage(string message, MessageType type)
        {
            sslMessage.Text = message;
            Color[] colors = { Color.Red, Color.Orange, Color.Green };
            sslMessage.ForeColor = colors[(int) type];
            sslMessage.Visible = true;
        }

        /// <summary>
        /// Zobrazi detail profilu
        /// </summary>
        /// <param name="profile">Zobrazeny profil</param>
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
                string message;
                if (manager.Validate(frmProfileDetail.Profile, out message))
                {
                    CreateOrUpdateProfile(frmProfileDetail.Profile);
                }
                else
                {
                    frmProfileDetail.ShowMessage(message, MessageType.Error);
                }
            };
            frmProfileDetail.SaveAndCloseButtonClicked += delegate
            {
                string message;
                if (manager.Validate(frmProfileDetail.Profile, out message))
                {
                    CreateOrUpdateProfile(frmProfileDetail.Profile);
                    frmProfileDetail.Close();
                    SelectedProfileIndex = -1;
                }
                else
                {
                    frmProfileDetail.ShowMessage(message, MessageType.Error);
                }
            };
            frmProfileDetail.CloseButtonClicked += delegate
            {
                frmProfileDetail.Close();
                SelectedProfileIndex = -1;
            };
        }

        /// <summary>
        /// Vytvori nebo upravi profile podle vybraneho indexu (<code>SelectedProfileIndex</code>)
        /// </summary>
        /// <param name="profile">Novy/Upravovany profile</param>
        private void CreateOrUpdateProfile(Profile profile)
        {
            if (SelectedProfileIndex == -1)
            {
                try
                {
                    lvwProfiles.BeginUpdate();
                    string[] cols = { profile.Name, profile.OutputFolder, profile.Disabled ? "Disabled" : "Enabled" };
                    ListViewItem lvi = new ListViewItem(cols);
                    lvwProfiles.Items.Add(lvi);
                    manager.Profiles.Add(profile);
                }
                finally
                {
                    lvwProfiles.EndUpdate();
                }
            }
            else
            {
                try
                {
                    lvwProfiles.BeginUpdate();
                    lvwProfiles.Items[SelectedProfileIndex].SubItems[0].Text = profile.Name;
                    lvwProfiles.Items[SelectedProfileIndex].SubItems[1].Text = profile.OutputFolder;
                    lvwProfiles.Items[SelectedProfileIndex].SubItems[2].Text = profile.Disabled ? "Disabled" : "Enabled";
                    manager.Profiles[SelectedProfileIndex] = profile;
                }
                finally
                {
                    lvwProfiles.EndUpdate();
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            SelectedProfileIndex = -1;
            Profile p = new Profile();
            p.Name = "New Profile";
            p.Period = TimePeriod.OneMonth;
            OpenProfileDetail(p);
        }

        private void lvwProfiles_DoubleClick(object sender, EventArgs e)
        {
            if (lvwProfiles.SelectedIndices.Count == 1)
            {
                SelectedProfileIndex = lvwProfiles.SelectedIndices[0];
                OpenProfileDetail(manager.Profiles[SelectedProfileIndex]);
            }
            else
            {
                ShowMessage("First, select profile!", MessageType.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvwProfiles.SelectedIndices.Count == 1)
            {
                if (MessageBox.Show(String.Format("Do you really want to delete profile \"{0}\"?", manager.Profiles[lvwProfiles.SelectedIndices[0]].Name), "Delete profile", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    try
                    {
                        lvwProfiles.BeginUpdate();
                        manager.Profiles.Remove(manager.Profiles[lvwProfiles.SelectedIndices[0]]);
                        lvwProfiles.SelectedItems[0].Remove();
                    }
                    finally
                    {
                        lvwProfiles.EndUpdate();
                    }
                }
            }
            else
            {
                ShowMessage("First, select profile!", MessageType.Error);
            }
        }

        private void tsbSaveConfig_Click(object sender, EventArgs e)
        {
            manager.Save();
        }
    }
}
