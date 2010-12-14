namespace FileBackuper.GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.stsStatus = new System.Windows.Forms.StatusStrip();
            this.sslMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tlsMainMenu = new System.Windows.Forms.ToolStrip();
            this.tsbSaveConfig = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.tsbWeb = new System.Windows.Forms.ToolStripButton();
            this.tsbConfig = new System.Windows.Forms.ToolStripButton();
            this.lvwProfiles = new System.Windows.Forms.ListView();
            this.chdName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdDestination = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdLastBackup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdNextBackup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdDisabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblCount = new System.Windows.Forms.Label();
            this.totToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnClearBackups = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tmrInterval = new System.Windows.Forms.Timer(this.components);
            this.pnlProfileControls = new System.Windows.Forms.Panel();
            this.tmrPostLoadAction = new System.Windows.Forms.Timer(this.components);
            this.stsStatus.SuspendLayout();
            this.tlsMainMenu.SuspendLayout();
            this.pnlProfileControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // stsStatus
            // 
            this.stsStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.stsStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sslMessage,
            this.tspProgress});
            this.stsStatus.Location = new System.Drawing.Point(0, 359);
            this.stsStatus.Name = "stsStatus";
            this.stsStatus.ShowItemToolTips = true;
            this.stsStatus.Size = new System.Drawing.Size(709, 22);
            this.stsStatus.TabIndex = 0;
            // 
            // sslMessage
            // 
            this.sslMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sslMessage.ForeColor = System.Drawing.Color.Red;
            this.sslMessage.Name = "sslMessage";
            this.sslMessage.Size = new System.Drawing.Size(114, 17);
            this.sslMessage.Text = "Some errors occur!";
            this.sslMessage.Visible = false;
            // 
            // tspProgress
            // 
            this.tspProgress.Name = "tspProgress";
            this.tspProgress.Size = new System.Drawing.Size(200, 16);
            this.tspProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.tspProgress.Visible = false;
            // 
            // tlsMainMenu
            // 
            this.tlsMainMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlsMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSaveConfig,
            this.tsbRefresh,
            this.tsbAbout,
            this.tsbWeb,
            this.tsbConfig});
            this.tlsMainMenu.Location = new System.Drawing.Point(0, 0);
            this.tlsMainMenu.Name = "tlsMainMenu";
            this.tlsMainMenu.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.tlsMainMenu.Size = new System.Drawing.Size(709, 25);
            this.tlsMainMenu.TabIndex = 1;
            this.tlsMainMenu.Text = "toolStrip1";
            // 
            // tsbSaveConfig
            // 
            this.tsbSaveConfig.Image = global::FileBackuper.GUI.Properties.Resources.save;
            this.tsbSaveConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveConfig.Name = "tsbSaveConfig";
            this.tsbSaveConfig.Size = new System.Drawing.Size(93, 22);
            this.tsbSaveConfig.Text = "Save profiles";
            this.tsbSaveConfig.Click += new System.EventHandler(this.tsbSaveConfig_Click);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = global::FileBackuper.GUI.Properties.Resources.repeat;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(108, 22);
            this.tsbRefresh.Text = "Refresh profiles";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tsbAbout
            // 
            this.tsbAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAbout.Image = global::FileBackuper.GUI.Properties.Resources.help;
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(23, 22);
            this.tsbAbout.Text = "About";
            this.tsbAbout.Click += new System.EventHandler(this.tsbAbout_Click);
            // 
            // tsbWeb
            // 
            this.tsbWeb.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbWeb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbWeb.Image = global::FileBackuper.GUI.Properties.Resources.web;
            this.tsbWeb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbWeb.Name = "tsbWeb";
            this.tsbWeb.Size = new System.Drawing.Size(23, 22);
            this.tsbWeb.Text = "Homepage, http://dev.neptuo.com";
            this.tsbWeb.Click += new System.EventHandler(this.tsbWeb_Click);
            // 
            // tsbConfig
            // 
            this.tsbConfig.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbConfig.Image = global::FileBackuper.GUI.Properties.Resources.config;
            this.tsbConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConfig.Name = "tsbConfig";
            this.tsbConfig.Size = new System.Drawing.Size(23, 22);
            this.tsbConfig.Text = "Configuration";
            this.tsbConfig.Click += new System.EventHandler(this.tsbConfig_Click);
            // 
            // lvwProfiles
            // 
            this.lvwProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwProfiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chdName,
            this.chdDestination,
            this.chdLastBackup,
            this.chdNextBackup,
            this.chdDisabled});
            this.lvwProfiles.FullRowSelect = true;
            this.lvwProfiles.GridLines = true;
            this.lvwProfiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwProfiles.Location = new System.Drawing.Point(12, 28);
            this.lvwProfiles.MultiSelect = false;
            this.lvwProfiles.Name = "lvwProfiles";
            this.lvwProfiles.Size = new System.Drawing.Size(685, 291);
            this.lvwProfiles.TabIndex = 2;
            this.lvwProfiles.UseCompatibleStateImageBehavior = false;
            this.lvwProfiles.View = System.Windows.Forms.View.Details;
            this.lvwProfiles.SelectedIndexChanged += new System.EventHandler(this.lvwProfiles_SelectedIndexChanged);
            this.lvwProfiles.DoubleClick += new System.EventHandler(this.lvwProfiles_DoubleClick);
            // 
            // chdName
            // 
            this.chdName.Text = "Name";
            this.chdName.Width = 180;
            // 
            // chdDestination
            // 
            this.chdDestination.Text = "Ouput directory";
            this.chdDestination.Width = 200;
            // 
            // chdLastBackup
            // 
            this.chdLastBackup.Text = "Last backup";
            this.chdLastBackup.Width = 120;
            // 
            // chdNextBackup
            // 
            this.chdNextBackup.Text = "Next backup";
            this.chdNextBackup.Width = 120;
            // 
            // chdDisabled
            // 
            this.chdDisabled.Text = "Disabled";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(-3, 12);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(49, 13);
            this.lblCount.TabIndex = 100;
            this.lblCount.Text = "0 profiles";
            // 
            // btnBackup
            // 
            this.btnBackup.Enabled = false;
            this.btnBackup.Image = global::FileBackuper.GUI.Properties.Resources.folder_go;
            this.btnBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackup.Location = new System.Drawing.Point(338, 3);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(75, 27);
            this.btnBackup.TabIndex = 8;
            this.btnBackup.Text = "&Backup!";
            this.btnBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.totToolTip.SetToolTip(this.btnBackup, "Creates unplanned backup right now.");
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Image = global::FileBackuper.GUI.Properties.Resources.folder_add;
            this.btnCreate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCreate.Location = new System.Drawing.Point(582, 3);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(100, 27);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "&Create Profile";
            this.btnCreate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.totToolTip.SetToolTip(this.btnCreate, "Creates new profile");
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnClearBackups
            // 
            this.btnClearBackups.Enabled = false;
            this.btnClearBackups.Image = global::FileBackuper.GUI.Properties.Resources.exclamation;
            this.btnClearBackups.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearBackups.Location = new System.Drawing.Point(208, 3);
            this.btnClearBackups.Name = "btnClearBackups";
            this.btnClearBackups.Size = new System.Drawing.Size(124, 27);
            this.btnClearBackups.TabIndex = 7;
            this.btnClearBackups.Text = "Clear &last backups!";
            this.btnClearBackups.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.totToolTip.SetToolTip(this.btnClearBackups, "Clears last backups but doesn\'t delete backup files");
            this.btnClearBackups.UseVisualStyleBackColor = true;
            this.btnClearBackups.Click += new System.EventHandler(this.btnClearBackups_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.Image = global::FileBackuper.GUI.Properties.Resources.folder_edit;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(74, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(53, 27);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.totToolTip.SetToolTip(this.btnEdit, "Edit currently selected profile");
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.lvwProfiles_DoubleClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = global::FileBackuper.GUI.Properties.Resources.folder_delete;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(133, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(69, 27);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.totToolTip.SetToolTip(this.btnDelete, "Delete currently selected profile");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // tmrInterval
            // 
            this.tmrInterval.Tick += new System.EventHandler(this.tmrInterval_Tick);
            // 
            // pnlProfileControls
            // 
            this.pnlProfileControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlProfileControls.Controls.Add(this.lblCount);
            this.pnlProfileControls.Controls.Add(this.btnBackup);
            this.pnlProfileControls.Controls.Add(this.btnCreate);
            this.pnlProfileControls.Controls.Add(this.btnClearBackups);
            this.pnlProfileControls.Controls.Add(this.btnEdit);
            this.pnlProfileControls.Controls.Add(this.btnDelete);
            this.pnlProfileControls.Location = new System.Drawing.Point(12, 322);
            this.pnlProfileControls.Name = "pnlProfileControls";
            this.pnlProfileControls.Size = new System.Drawing.Size(685, 34);
            this.pnlProfileControls.TabIndex = 9;
            // 
            // tmrPostLoadAction
            // 
            this.tmrPostLoadAction.Tick += new System.EventHandler(this.tmrPostLoadAction_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 381);
            this.Controls.Add(this.pnlProfileControls);
            this.Controls.Add(this.lvwProfiles);
            this.Controls.Add(this.tlsMainMenu);
            this.Controls.Add(this.stsStatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(725, 419);
            this.Name = "MainForm";
            this.Text = "FileBackuper";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.stsStatus.ResumeLayout(false);
            this.stsStatus.PerformLayout();
            this.tlsMainMenu.ResumeLayout(false);
            this.tlsMainMenu.PerformLayout();
            this.pnlProfileControls.ResumeLayout(false);
            this.pnlProfileControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip stsStatus;
        private System.Windows.Forms.ToolStrip tlsMainMenu;
        private System.Windows.Forms.ListView lvwProfiles;
        private System.Windows.Forms.ColumnHeader chdName;
        private System.Windows.Forms.ColumnHeader chdDestination;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.ColumnHeader chdDisabled;
        private System.Windows.Forms.ToolStripStatusLabel sslMessage;
        private System.Windows.Forms.ToolStripButton tsbSaveConfig;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ColumnHeader chdLastBackup;
        private System.Windows.Forms.ColumnHeader chdNextBackup;
        private System.Windows.Forms.Button btnClearBackups;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.ToolTip totToolTip;
        private System.Windows.Forms.ToolStripProgressBar tspProgress;
        private System.Windows.Forms.Timer tmrInterval;
        private System.Windows.Forms.Panel pnlProfileControls;
        private System.Windows.Forms.Timer tmrPostLoadAction;
        private System.Windows.Forms.ToolStripButton tsbAbout;
        private System.Windows.Forms.ToolStripButton tsbConfig;
        private System.Windows.Forms.ToolStripButton tsbWeb;
    }
}

