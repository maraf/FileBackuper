using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using FileBackuper.Logic;
using FileBackuper.Model;
using System.IO;

namespace FileBackuper.GUI
{
    /// <summary>
    /// Kdy je potreba pocitat nove datum zalohy?
    /// - Po editaci profilu (StartAt + Period)
    /// - Po provedeni zpozdene zalohy (StartAt + Period)
    /// - Po vycisteni predchozich zaloh u NoPerid (StartAt + Period)
    /// </summary>
    [ConfigPath(@"C:\Temp\FileBackuper\Config.xml")]
    public partial class MainForm : Form
    {
        private ProfileDetail frmProfileDetail;
        private ConfigurationForm frmConfiguration;

        private Configuration configuration;

        private ProfileManager profileManager;

        private ScheduledTaskManager taskManager;

        private int SelectedProfileIndex = -1;

        private bool isSavedLastChanges = true;

        public MainForm()
        {
            InitializeComponent();
            this.Text += " - " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            configuration = ConfigurationManager.LoadFromResource();
            
            //LoadProfilesFromConfiguration(configuration);

            try
            {
                profileManager = new ProfileManager(configuration.ConfigPath);
                //profileManager = new ProfileManager(ModelUtil.PathToConfigurationFile(this.GetType()));
                profileManager.Load();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Sorry, but file configuration doesn't exist! Go to configration and create new one or select corect path!", "FileBackuper");
            }

            taskManager = new ScheduledTaskManager();

            tmrPostLoadAction.Enabled = true;
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

            Application.DoEvents();
        }

        /// <summary>
        /// Skryje hlasku
        /// </summary>
        public void HideMessage()
        {
            sslMessage.Visible = false;
        }

        /// <summary>
        /// Zobrazi "nahravani" + zpravu
        /// </summary>
        /// <param name="message">Obsah zpravy, viz <code>ShowMessage</code></param>
        /// <param name="type">Typ zpravy (barva), viz <code>ShowMessage</code></param>
        /// <param name="disable">Pokud je true, zablokuje zbytek okna, dokud neni zavolano <code>HideLoading</code></param>
        public void ShowLoading(string message, MessageType type, bool disable)
        {
            tspProgress.Visible = true;
            tmrInterval.Enabled = true;

            if (disable)
            {
                tlsMainMenu.Enabled = false;
                lvwProfiles.Enabled = false;
                pnlProfileControls.Enabled = false;
            }

            ShowMessage(message, type);
        }

        /// <summary>
        /// Skryje "nahravani"
        /// </summary>
        public void HideLoading()
        {
            tspProgress.Visible = false;
            tmrInterval.Enabled = false;

            tlsMainMenu.Enabled = true;
            lvwProfiles.Enabled = true;
            pnlProfileControls.Enabled = true;

            HideMessage();
        }

        private void LoadProfilesFromConfiguration(Configuration config)
        {
            profileManager = new ProfileManager(configuration.ConfigPath);
            //profileManager = new ProfileManager(ModelUtil.PathToConfigurationFile(this.GetType()));
            profileManager.Load();

            CheckThatLastBackupWasCompleted();
            LoadProfilesToLv();
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
                if (profileManager.Validate(frmProfileDetail.Profile, out message))
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
                if (profileManager.Validate(frmProfileDetail.Profile, out message))
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
        /// Zobrazi editaci konfigurace
        /// </summary>
        /// <param name="profile">Konfiguracni objekt</param>
        public void OpenConfiguration(Configuration config)
        {
            if (frmConfiguration == null)
            {
                frmConfiguration = new ConfigurationForm(config);
            }
            if (frmConfiguration.WindowState == FormWindowState.Minimized)
            {
                frmConfiguration.WindowState = FormWindowState.Normal;
            }
            if (!frmConfiguration.Visible)
            {
                frmConfiguration.Show();
            }
            frmConfiguration.BringToFront();
            frmConfiguration.FormClosed += delegate { frmConfiguration = null; };
            frmConfiguration.SaveButtonClicked += delegate
            {
                // Znovu nacist profily s pouzitim nove konfigurace
                configuration = frmConfiguration.Configuration;
                LoadProfilesFromConfiguration(configuration);
                ConfigurationManager.SaveToResource(configuration);
            };
            frmConfiguration.SaveAndCloseButtonClicked += delegate
            {
                // Znovu nacist profily s pouzitim nove konfigurace
                configuration = frmConfiguration.Configuration;
                LoadProfilesFromConfiguration(configuration);
                ConfigurationManager.SaveToResource(configuration);
                frmConfiguration.Close();
            };
            frmConfiguration.CloseButtonClicked += delegate
            {
                frmConfiguration.Close();
            };
        }

        /// <summary>
        /// Otevre AboutForm
        /// </summary>
        public void OpenAbout()
        {
            AboutForm about = new AboutForm();
            about.WindowState = FormWindowState.Normal;
            about.ShowDialog();
            about.FormClosed += delegate { about = null; };
        }

        /// <summary>
        /// Vytvori nebo upravi profile podle vybraneho indexu (<code>SelectedProfileIndex</code>)
        /// </summary>
        /// <param name="profile">Novy/Upravovany profile</param>
        private void CreateOrUpdateProfile(Profile profile)
        {
            // Vypocitat datum a cas nove zalohy pro povolene profily
            if (profile.Disabled)
            {
                profile.NextBackup = DateTime.MinValue;
            }
            else
            {
                profile.NextBackup = DateTime.Now.Add(ModelUtil.TimeSpanUsingStartAt(profile));
                taskManager.CreateOrUpdateTask(profile, profile.NextBackup);
            }


            if (SelectedProfileIndex == -1)
            {
                try
                {
                    lvwProfiles.BeginUpdate();
                    ListViewItem lvi = new ListViewItem(GetColsToLv(profile));
                    lvwProfiles.Items.Add(lvi);
                    profileManager.Profiles.Add(profile);
                }
                finally
                {
                    lvwProfiles.EndUpdate();
                    UpdateProfilesCount();
                }
            }
            else
            {
                try
                {
                    lvwProfiles.BeginUpdate();
                    ListViewItem lvi = new ListViewItem(GetColsToLv(profile));
                    lvwProfiles.Items[SelectedProfileIndex] = lvi;
                    profileManager.Profiles[SelectedProfileIndex] = profile;
                }
                finally
                {
                    lvwProfiles.EndUpdate();
                    UpdateProfilesCount();
                }
            }
            isSavedLastChanges = false;
        }

        /// <summary>
        /// Zobrazi seznam profilu v <code>lvwProfiles</code>
        /// </summary>
        private void LoadProfilesToLv()
        {
            int selected = lvwProfiles.SelectedIndices.Count == 1 ? lvwProfiles.SelectedIndices[0] : -1;

            try
            {
                lvwProfiles.BeginUpdate();
                lvwProfiles.Items.Clear();
                foreach (Profile profile in profileManager.Profiles)
                {
                    ListViewItem lvi = new ListViewItem(GetColsToLv(profile));
                    lvwProfiles.Items.Add(lvi);
                }
            }
            finally
            {
                lvwProfiles.EndUpdate();
                UpdateProfilesCount();
                if (lvwProfiles.Items.Count > selected && selected != -1)
                {
                    lvwProfiles.Items[selected].Selected = true;
                }
            }
        }

        /// <summary>
        /// Vrati pole sloupcu do <code>lvwProfiles</code>
        /// </summary>
        /// <param name="profile">Profil pro vyber hodnot</param>
        /// <returns>Pole sloupcu</returns>
        private String[] GetColsToLv(Profile profile)
        {
            //if (next < DateTime.Now || (profile.LastBackup.Hour != profile.StartAt.Hour || profile.LastBackup.Minute != profile.StartAt.Minute))
            //{
            //    next = DateTime.Now.Add(ModelUtil.TimeSpanUsingStartAt(profile));
            //}
            //string[] cols = { profile.Name, profile.OutputFolder, profile.LastBackup.Equals(DateTime.MinValue) ? "None" : String.Format("{0:yyyy-MM-dd HH:mm:ss}", profile.LastBackup), next.Equals(DateTime.MinValue) ? "None" : String.Format("{0:yyyy-MM-dd HH:mm:ss}", next), profile.Disabled ? "Disabled" : "Enabled" };
            string[] cols = { profile.Name, profile.OutputFolder, profile.LastBackup.Equals(DateTime.MinValue) ? "None" : String.Format("{0:yyyy-MM-dd HH:mm:ss}", profile.LastBackup), profile.NextBackup.Equals(DateTime.MinValue) || profile.Disabled ? "None" : String.Format("{0:yyyy-MM-dd HH:mm:ss}", profile.NextBackup), profile.Disabled ? "Disabled" : "Enabled" };
            return cols;
        }

        /// <summary>
        /// Ulozi konfiguraci a vytvori naplanovane ulohy pro zalohovani
        /// </summary>
        private void SaveChangesAndCreateTasks()
        {
            // Zde rovnou vytvaret ulohy, uzivateli neobtezovat tlacitkem "Create scheduled tasks".
            foreach (Profile p in profileManager.Profiles)
            {
                // Pro zakazane profily nevytvaret ulohy
                if (!p.Disabled)
                {
                    // Vypocitat za jak dlouho se ma poprve spustit z StartAt
                    taskManager.CreateOrUpdateTaskUsingStartAt(p);
                }
                else
                {
                    // Pokud jiz me vytvorenou ulohu profil, ktery je neni disabled, smaze tuto ulohu
                    taskManager.DeteleTask(p);
                }
            }

            SaveChanges();
        }

        /// <summary>
        /// Ulozit konfiguraci profilů
        /// </summary>
        private void SaveChanges()
        {
            try
            {
                // Ulozit konfiguraci
                profileManager.Save();
                isSavedLastChanges = true;
            }
            catch (DirtyWriteAttemptException e)
            {
                profileManager.Merge(MergeType.Merge);
                profileManager.Save();
                isSavedLastChanges = true;
            }
        }

        /// <summary>
        /// Kontroluje zda probehla posledni zaloha dle naplanovani.
        /// </summary>
        private void CheckThatLastBackupWasCompleted()
        {
            List<Profile> missed = new List<Profile>();
            string names = "";
            foreach (Profile p in profileManager.Profiles)
            {
                DateTime next = p.NextBackup;
                if (next <= DateTime.Now && !p.Disabled && !p.Period.Equals(TimePeriod.NoPeriod))
                {
                    missed.Add(p);
                    names += "\n\t" + p.Name;
                }
            }

            if (missed.Count != 0)
            {
                // Zobrazit chybovou hlasku!
                if (MessageBox.Show(String.Format("Last backup for profiles: \n{0}\n\nhadn't been processed, do you want to backup now?\nThis can be caused the computer was shutdown at backup time.", names), "Missed backups", MessageBoxButtons.YesNo, MessageBoxIcon.Error).Equals(DialogResult.Yes))
                {
                    ShowMessage("Backuping ...", MessageType.Success);

                    Scheduler sch = new Scheduler();
                    foreach (Profile p in missed)
                    {
                        sch.OnSchedule(p.Name, true);
                        //taskManager.CreateOrUpdateTaskUsingStartAt(p);
                    }

                    HideMessage();
                }
                else
                {
                    foreach (Profile p in missed)
                    {
                        taskManager.CreateOrUpdateTaskUsingStartAt(p);
                    }
                }
                profileManager.Load();
            }
        }

        /// <summary>
        /// Aktualizuje <code>lblCount</code> na počet aktuálně zobrazených profilů
        /// </summary>
        public void UpdateProfilesCount()
        {
            lblCount.Text = String.Format("{0} profile(s)", profileManager.Profiles.Count);
        }

        #region UDALOSTI

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
                OpenProfileDetail(profileManager.Profiles[SelectedProfileIndex]);
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
                if (MessageBox.Show(String.Format("Do you really want to delete profile \"{0}\"?", profileManager.Profiles[lvwProfiles.SelectedIndices[0]].Name), "Delete profile", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    try
                    {
                        lvwProfiles.BeginUpdate();
                        taskManager.DeteleTask(profileManager.Profiles[lvwProfiles.SelectedIndices[0]]);
                        profileManager.Profiles.Remove(profileManager.Profiles[lvwProfiles.SelectedIndices[0]]);
                        lvwProfiles.SelectedItems[0].Remove();
                    }
                    finally
                    {
                        lvwProfiles.EndUpdate();
                        UpdateProfilesCount();
                    }
                }
                isSavedLastChanges = false;
            }
            else
            {
                ShowMessage("First, select profile!", MessageType.Error);
            }
        }

        private void btnClearBackups_Click(object sender, EventArgs e)
        {
            if (lvwProfiles.SelectedIndices.Count == 1)
            {
                if (MessageBox.Show(String.Format("Do you really want to clear last backups of profile \"{0}\"?", profileManager.Profiles[lvwProfiles.SelectedIndices[0]].Name), "Clear last backups", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    // smazat posledni zalohy
                    profileManager.Profiles[lvwProfiles.SelectedIndices[0]].LastBackup = DateTime.MinValue;
                    profileManager.Profiles[lvwProfiles.SelectedIndices[0]].VersionsNames.Clear();
                    // upravit lvwProfiles, hlavne zobrazeni poslednich zalohy a pristi zalohy -> pristi zaloha neni potreba aktualizovat!
                    try
                    {
                        lvwProfiles.BeginUpdate();
                        lvwProfiles.Items[lvwProfiles.SelectedIndices[0]] = new ListViewItem(GetColsToLv(profileManager.Profiles[lvwProfiles.SelectedIndices[0]]));
                    }
                    finally
                    {
                        lvwProfiles.EndUpdate();
                        UpdateProfilesCount();
                    }
                    isSavedLastChanges = false;
                }
            }
        }

        private void tsbSaveConfig_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            profileManager.Load();

            LoadProfilesToLv();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSavedLastChanges)
            {
                DialogResult dr = MessageBox.Show("Do you want to save last changes?", "Save last changes", MessageBoxButtons.YesNoCancel);
                if (DialogResult.Yes.Equals(dr))
                {
                    // Ulozit soubor a vytvorit naplanovane ulohy
                    SaveChangesAndCreateTasks();
                }
                else if (DialogResult.Cancel.Equals(dr))
                {
                    // Neukoncit aplikaci
                    e.Cancel = true;
                }
            }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            lvwProfiles.Select();
        }

        private void lvwProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwProfiles.SelectedIndices.Count == 1)
            {
                btnClearBackups.Enabled = !lvwProfiles.SelectedItems[0].SubItems[2].Text.Equals("None");
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnBackup.Enabled = true;
            }
            else
            {
                btnClearBackups.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnBackup.Enabled = false;
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            ShowMessage("Backuping ...", MessageType.Success);

            btnBackup.Enabled = false;
            Zipper zip = new Zipper();
            string name = zip.Zip(profileManager.Profiles[lvwProfiles.SelectedIndices[0]]);
            btnBackup.Enabled = true;

            ShowMessage(String.Format("Immediate backup created for profile {0}, filename is {1}.", profileManager.Profiles[lvwProfiles.SelectedIndices[0]].Name, name), MessageType.Success);
        }

        private void tmrInterval_Tick(object sender, EventArgs e)
        {
            tspProgress.Value = ((tspProgress.Value == tspProgress.Maximum) ? tspProgress.Minimum : (tspProgress.Value + tspProgress.Step));
            Application.DoEvents();
        }

        private void tmrPostLoadAction_Tick(object sender, EventArgs e)
        {
            tmrPostLoadAction.Enabled = false;

            CheckThatLastBackupWasCompleted();

            LoadProfilesToLv();
        }

        private void tsbConfig_Click(object sender, EventArgs e)
        {
            OpenConfiguration(configuration);
        }

        private void tsbAbout_Click(object sender, EventArgs e)
        {
            OpenAbout();
        }

        private void tsbWeb_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://dev.neptuo.com");
        }

        #endregion
    }
}
