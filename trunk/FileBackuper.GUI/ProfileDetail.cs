using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FileBackuper.Model;

namespace FileBackuper.GUI
{
    /// <summary>
    /// Delegat pro obsluhu tlacitek
    /// </summary>
    /// <param name="sender">Komponenta, ktera ho vyvolala</param>
    /// <param name="e">Argumenty udalosti</param>
    public delegate void DetailButtonHandler(object sender, EventArgs e);

    /// <summary>
    /// Druh zobrazovane zpravy
    /// </summary>
    public enum MessageType { Error, Warning, Success };

    /// <summary>
    /// Formular pro zobrazeni a editaci profilu
    /// </summary>
    public partial class ProfileDetail : Form
    {
        public event DetailButtonHandler SaveButtonClicked;

        public event DetailButtonHandler SaveAndCloseButtonClicked;

        public event DetailButtonHandler CloseButtonClicked;

        /// <summary>
        /// Zobrazeny profil
        /// </summary>
        private Profile profile;
        public Profile Profile
        {
            get { return profile; }
            set { profile = value; SetUIComponentsValues(); }
        }

        private Dictionary<string, TimePeriod[]> patterns = new Dictionary<string, TimePeriod[]>();

        public ProfileDetail(Profile profile)
        {
            InitializeComponent();
            InitializePatters();

            Profile = profile;

            tbxName.Focus();
        }

        /// <summary>
        /// Vytvori slovnik vzoru nazvu souboru
        /// </summary>
        private void InitializePatters()
        {
            TimePeriod[] cols1 = { TimePeriod.NoPeriod };
            patterns.Add("ProfileName", cols1);
            TimePeriod[] cols2 = { TimePeriod.NoPeriod, TimePeriod.OneMonth };
            patterns.Add("ProfileName_yyyy-MM", cols2);
            TimePeriod[] cols3 = { TimePeriod.NoPeriod, TimePeriod.OneMonth, TimePeriod.OneDay, TimePeriod.TwoDays, TimePeriod.ThreeDays, TimePeriod.OneWeek, TimePeriod.TwoWeeks };
            patterns.Add("ProfileName_yyyy-MM-dd", cols3);
            TimePeriod[] cols4 = { TimePeriod.NoPeriod, TimePeriod.OneMonth, TimePeriod.OneDay, TimePeriod.TwoDays, TimePeriod.ThreeDays, TimePeriod.OneWeek, TimePeriod.TwoWeeks, TimePeriod.OneHour, TimePeriod.ThreeHours, TimePeriod.FiveHours, TimePeriod.TwelveHours };
            patterns.Add("ProfileName_yyyy-MM-dd_HH-mm-ss", cols4);
        }

        /// <summary>
        /// Nastavi hodnoty UI komponent z profilu
        /// </summary>
        private void SetUIComponentsValues()
        {
            tbxName.Text = Profile.Name;
            dsrOutputFolder.Value = Profile.OutputFolder;
            cbxPeriod.SelectedIndex = (int) Profile.Period;
            nudNumberOfVersions.Value = Profile.NumberOfVersions;
            cbxDisabled.Checked = Profile.Disabled;

            UpdateFileNamePatternsItems(Profile.Period);

            try
            {
                lvwUnits.BeginUpdate();

                foreach (ZipUnit unit in Profile.Units)
                {
                    string[] cols = { ModelUtil.UnitTypeToString(unit.UnitType), unit.Path };
                    ListViewItem lvi = new ListViewItem(cols);
                    lvwUnits.Items.Add(lvi);
                }
            }
            finally
            {
                lvwUnits.EndUpdate();
            }
        }

        /// <summary>
        /// Opak k <code>SetUIComponentsValues</code>
        /// </summary>
        private void GetUIComponentsValues()
        {
            Profile.Name = tbxName.Text;
            Profile.OutputFolder = dsrOutputFolder.Value;
            Profile.Period = (TimePeriod) cbxPeriod.SelectedIndex;
            Profile.NumberOfVersions = (int) nudNumberOfVersions.Value;
            Profile.FileNamePattern = (string) cbxFileNamePattern.SelectedItem;
            Profile.Disabled = cbxDisabled.Checked;

            Profile.Units.Clear();
            foreach (ListViewItem item in lvwUnits.Items)
            {
                Profile.Units.Add(new ZipUnit(ModelUtil.StringToUnitType(item.SubItems[0].Text).Equals(UnitType.File) ? UnitType.File : UnitType.Folder, item.SubItems[1].Text));
            }
        }

        /// <summary>
        /// Vybere mozne vzory nazvu podle aktualne vybrane periody zalohovani
        /// </summary>
        /// <param name="period"></param>
        private void UpdateFileNamePatternsItems(TimePeriod period)
        {
            cbxFileNamePattern.Items.Clear();
            foreach (KeyValuePair<string, TimePeriod[]> item in patterns)
            {
                if (item.Value.Contains<TimePeriod>(period))
                {
                    cbxFileNamePattern.Items.Add(item.Key);
                }
            }
            cbxFileNamePattern.SelectedIndex = 0;
        }

        /// <summary>
        /// Zobrazi hlasku v pravem dolnim rohu
        /// </summary>
        /// <param name="message">Obsah hlasky</param>
        /// <param name="type">Typ hlasky (barva)</param>
        public void ShowMessage(string message, MessageType type)
        {
            lblMessage.Text = message.Length > 60 ? message.Substring(0, 40) + " ..." : message;
            Color[] colors = { Color.Red, Color.Orange, Color.Green};
            lblMessage.ForeColor = colors[(int) type];
            lblMessage.Visible = true;
        }

        /// <summary>
        /// Skryje hlasku
        /// </summary>
        public void HideMessage()
        {
            lblMessage.Visible = false;
        }

        /// <summary>
        /// Zjisti zda zadana cesta jiz je v seznamu
        /// </summary>
        /// <param name="path">Hledana cesta</param>
        /// <returns>true pokud je jiz obsazena, false jinak</returns>
        public bool IsInUnits(string path)
        {
            foreach (ListViewItem item in lvwUnits.Items)
            {
                if (item.SubItems[1].Text.Equals(path))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Zjistuje zda zadana cesta neni vystupni slozkou
        /// </summary>
        /// <param name="path">Testovana cesta</param>
        /// <returns>true pokud je, false jinak</returns>
        public bool IsOutputFolder(string path)
        {
            return dsrOutputFolder.Value.Equals(path);
        }

        /// <summary>
        /// Zjistuje zda posledni slozka ze zadane cesty jiz je mezi slozkami k zalohovani
        /// </summary>
        /// <param name="path">Testovana cesta</param>
        /// <returns>true pokud posledni slozka ze zadane cesty jiz je mezi slozkami, false jinak</returns>
        public bool IsLastFolderInUnits(string path)
        {
            string[] folders = path.Split('\\');
            string folder = folders[folders.Length - 1];

            foreach (ListViewItem lvi in lvwUnits.Items)
            {
                if (lvi.SubItems[0].Text.Equals(ModelUtil.UnitTypeToString(UnitType.Folder)))
                {
                    folders = lvi.SubItems[1].Text.Split('\\');
                    if (folder.Equals(folders[folders.Length - 1]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

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

        private void cbxPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFileNamePatternsItems(ModelUtil.StringToTimePeriod((string) cbxPeriod.SelectedItem));
        }

        private void rbnFile_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnFile.Checked)
            {
                fsrFile.Enabled = true;
                dsrFolder.Enabled = false;
            }
            else
            {
                fsrFile.Enabled = false;
                dsrFolder.Enabled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!fsrFile.Value.Equals("") || !dsrFolder.Value.Equals(""))
            {
                string[] cols = { "", "" };
                if (rbnFile.Checked)
                {
                    cols[0] = ModelUtil.UnitTypeToString(UnitType.File);
                    cols[1] = fsrFile.Value;
                    fsrFile.Value = "";
                }
                else
                {
                    cols[0] = ModelUtil.UnitTypeToString(UnitType.Folder);
                    cols[1] = dsrFolder.Value;
                    dsrFolder.Value = "";
                }

                try
                {
                    lvwUnits.BeginUpdate();
                    ListViewItem lvi = new ListViewItem(cols);
                    if (!IsInUnits(cols[1]))
                    {
                        if ((!rbnFile.Checked && !IsLastFolderInUnits(cols[1])) || rbnFile.Checked)
                        {
                            if (!IsOutputFolder(cols[1]))
                            {
                                lvwUnits.Items.Add(lvi);
                                ShowMessage(String.Format("Added {0} {1}.", cols[0], cols[1]), MessageType.Success);
                            }
                            else
                            {
                                ShowMessage("Profile can't contain its output folder!", MessageType.Error);
                            }
                        }
                        else
                        {
                            ShowMessage("Profile already containts folder with this name!", MessageType.Error);
                        }
                    }
                    else
                    {
                        ShowMessage("Profile already contains this item!", MessageType.Error);
                    }
                }
                finally
                {
                    lvwUnits.EndUpdate();
                }
            }
            else
            {
                ShowMessage("You select file or folder!", MessageType.Error);
            }
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lvwUnits.BeginUpdate();
                ShowMessage(String.Format("Deleted {0} {1}.", lvwUnits.SelectedItems[0].SubItems[0].Text, lvwUnits.SelectedItems[0].SubItems[1].Text), MessageType.Success);
                lvwUnits.SelectedItems[0].Remove();
            }
            finally
            {
                lvwUnits.EndUpdate();
            }
        }

        private void cmsUnits_Opening(object sender, CancelEventArgs e)
        {
            if (lvwUnits.SelectedItems.Count == 0)
            {
                e.Cancel = true;
            }
        }
    }
}
