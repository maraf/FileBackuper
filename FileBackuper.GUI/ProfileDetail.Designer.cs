namespace FileBackuper.GUI
{
    partial class ProfileDetail
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
            this.lblName = new System.Windows.Forms.Label();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.lblFileNamePattern = new System.Windows.Forms.Label();
            this.cbxFileNamePattern = new System.Windows.Forms.ComboBox();
            this.gpbVersions = new System.Windows.Forms.GroupBox();
            this.cbxPeriod = new System.Windows.Forms.ComboBox();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.lblNumberOfVersionsNote = new System.Windows.Forms.Label();
            this.nudNumberOfVersions = new System.Windows.Forms.NumericUpDown();
            this.lblNumberOfVersions = new System.Windows.Forms.Label();
            this.gpbBasicInfo = new System.Windows.Forms.GroupBox();
            this.cbxDisabled = new System.Windows.Forms.CheckBox();
            this.lblOutputFolder = new System.Windows.Forms.Label();
            this.dsrOutputFolder = new FileBackuper.Controls.DirectorySelector();
            this.gpbUnits = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblUnitsNote = new System.Windows.Forms.Label();
            this.lvwUnits = new System.Windows.Forms.ListView();
            this.chdType = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.chdPath = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.cmsUnits = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.rbnFolder = new System.Windows.Forms.RadioButton();
            this.rbnFile = new System.Windows.Forms.RadioButton();
            this.dsrFolder = new FileBackuper.Controls.DirectorySelector();
            this.fsrFile = new FileBackuper.Controls.FileSelector();
            this.lblUnits = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSaveAndClose = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.gpbVersions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.nudNumberOfVersions)).BeginInit();
            this.gpbBasicInfo.SuspendLayout();
            this.gpbUnits.SuspendLayout();
            this.cmsUnits.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 26);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(68, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Profile name:";
            // 
            // tbxName
            // 
            this.tbxName.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxName.Location = new System.Drawing.Point(80, 23);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(453, 20);
            this.tbxName.TabIndex = 1;
            // 
            // lblFileNamePattern
            // 
            this.lblFileNamePattern.AutoSize = true;
            this.lblFileNamePattern.Location = new System.Drawing.Point(6, 49);
            this.lblFileNamePattern.Name = "lblFileNamePattern";
            this.lblFileNamePattern.Size = new System.Drawing.Size(88, 13);
            this.lblFileNamePattern.TabIndex = 2;
            this.lblFileNamePattern.Text = "Filename pattern:";
            // 
            // cbxFileNamePattern
            // 
            this.cbxFileNamePattern.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxFileNamePattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFileNamePattern.FormattingEnabled = true;
            this.cbxFileNamePattern.Items.AddRange(new object[] {
            "ProfileName_YYYY-MM-dd"});
            this.cbxFileNamePattern.Location = new System.Drawing.Point(100, 46);
            this.cbxFileNamePattern.Name = "cbxFileNamePattern";
            this.cbxFileNamePattern.Size = new System.Drawing.Size(284, 21);
            this.cbxFileNamePattern.TabIndex = 3;
            // 
            // gpbVersions
            // 
            this.gpbVersions.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbVersions.Controls.Add(this.cbxPeriod);
            this.gpbVersions.Controls.Add(this.lblPeriod);
            this.gpbVersions.Controls.Add(this.lblNumberOfVersionsNote);
            this.gpbVersions.Controls.Add(this.nudNumberOfVersions);
            this.gpbVersions.Controls.Add(this.lblNumberOfVersions);
            this.gpbVersions.Controls.Add(this.lblFileNamePattern);
            this.gpbVersions.Controls.Add(this.cbxFileNamePattern);
            this.gpbVersions.Location = new System.Drawing.Point(15, 135);
            this.gpbVersions.Name = "gpbVersions";
            this.gpbVersions.Size = new System.Drawing.Size(619, 77);
            this.gpbVersions.TabIndex = 4;
            this.gpbVersions.TabStop = false;
            this.gpbVersions.Text = "Versions";
            // 
            // cbxPeriod
            // 
            this.cbxPeriod.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPeriod.FormattingEnabled = true;
            this.cbxPeriod.Items.AddRange(new object[] {
            "No period",
            "One hours",
            "Three hours",
            "Five hours",
            "Twelve hours",
            "One day",
            "Two days",
            "Three days",
            "One week",
            "Two weeks",
            "One month"});
            this.cbxPeriod.Location = new System.Drawing.Point(167, 17);
            this.cbxPeriod.Name = "cbxPeriod";
            this.cbxPeriod.Size = new System.Drawing.Size(217, 21);
            this.cbxPeriod.TabIndex = 9;
            this.cbxPeriod.SelectedIndexChanged += new System.EventHandler(this.cbxPeriod_SelectedIndexChanged);
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Location = new System.Drawing.Point(6, 20);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(157, 13);
            this.lblPeriod.TabIndex = 8;
            this.lblPeriod.Text = "Backup period ( means every ) :";
            // 
            // lblNumberOfVersionsNote
            // 
            this.lblNumberOfVersionsNote.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumberOfVersionsNote.AutoSize = true;
            this.lblNumberOfVersionsNote.Location = new System.Drawing.Point(402, 41);
            this.lblNumberOfVersionsNote.Name = "lblNumberOfVersionsNote";
            this.lblNumberOfVersionsNote.Size = new System.Drawing.Size(176, 13);
            this.lblNumberOfVersionsNote.TabIndex = 7;
            this.lblNumberOfVersionsNote.Text = "( Set to 0 for only the latest version )";
            // 
            // nudNumberOfVersions
            // 
            this.nudNumberOfVersions.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudNumberOfVersions.Location = new System.Drawing.Point(528, 18);
            this.nudNumberOfVersions.Name = "nudNumberOfVersions";
            this.nudNumberOfVersions.Size = new System.Drawing.Size(85, 20);
            this.nudNumberOfVersions.TabIndex = 6;
            // 
            // lblNumberOfVersions
            // 
            this.lblNumberOfVersions.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumberOfVersions.AutoSize = true;
            this.lblNumberOfVersions.Location = new System.Drawing.Point(402, 20);
            this.lblNumberOfVersions.Name = "lblNumberOfVersions";
            this.lblNumberOfVersions.Size = new System.Drawing.Size(120, 13);
            this.lblNumberOfVersions.TabIndex = 4;
            this.lblNumberOfVersions.Text = "Number of last versions:";
            // 
            // gpbBasicInfo
            // 
            this.gpbBasicInfo.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbBasicInfo.Controls.Add(this.cbxDisabled);
            this.gpbBasicInfo.Controls.Add(this.lblOutputFolder);
            this.gpbBasicInfo.Controls.Add(this.dsrOutputFolder);
            this.gpbBasicInfo.Controls.Add(this.lblName);
            this.gpbBasicInfo.Controls.Add(this.tbxName);
            this.gpbBasicInfo.Location = new System.Drawing.Point(15, 12);
            this.gpbBasicInfo.Name = "gpbBasicInfo";
            this.gpbBasicInfo.Size = new System.Drawing.Size(619, 117);
            this.gpbBasicInfo.TabIndex = 5;
            this.gpbBasicInfo.TabStop = false;
            this.gpbBasicInfo.Text = "Basic settings";
            // 
            // cbxDisabled
            // 
            this.cbxDisabled.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDisabled.AutoSize = true;
            this.cbxDisabled.Location = new System.Drawing.Point(546, 25);
            this.cbxDisabled.Name = "cbxDisabled";
            this.cbxDisabled.Size = new System.Drawing.Size(67, 17);
            this.cbxDisabled.TabIndex = 4;
            this.cbxDisabled.Text = "Disabled";
            this.cbxDisabled.UseVisualStyleBackColor = true;
            // 
            // lblOutputFolder
            // 
            this.lblOutputFolder.AutoSize = true;
            this.lblOutputFolder.Location = new System.Drawing.Point(6, 55);
            this.lblOutputFolder.Name = "lblOutputFolder";
            this.lblOutputFolder.Size = new System.Drawing.Size(99, 13);
            this.lblOutputFolder.TabIndex = 3;
            this.lblOutputFolder.Text = "Select ouput folder:";
            // 
            // dsrOutputFolder
            // 
            this.dsrOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dsrOutputFolder.Location = new System.Drawing.Point(9, 71);
            this.dsrOutputFolder.MinimumSize = new System.Drawing.Size(200, 31);
            this.dsrOutputFolder.Name = "dsrOutputFolder";
            this.dsrOutputFolder.Size = new System.Drawing.Size(604, 31);
            this.dsrOutputFolder.TabIndex = 2;
            this.dsrOutputFolder.Value = "";
            // 
            // gpbUnits
            // 
            this.gpbUnits.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbUnits.Controls.Add(this.btnAdd);
            this.gpbUnits.Controls.Add(this.lblUnitsNote);
            this.gpbUnits.Controls.Add(this.lvwUnits);
            this.gpbUnits.Controls.Add(this.rbnFolder);
            this.gpbUnits.Controls.Add(this.rbnFile);
            this.gpbUnits.Controls.Add(this.dsrFolder);
            this.gpbUnits.Controls.Add(this.fsrFile);
            this.gpbUnits.Controls.Add(this.lblUnits);
            this.gpbUnits.Location = new System.Drawing.Point(15, 218);
            this.gpbUnits.Name = "gpbUnits";
            this.gpbUnits.Size = new System.Drawing.Size(619, 360);
            this.gpbUnits.TabIndex = 6;
            this.gpbUnits.TabStop = false;
            this.gpbUnits.Text = "Files and Folders";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(538, 23);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 55);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "A&dd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblUnitsNote
            // 
            this.lblUnitsNote.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUnitsNote.AutoSize = true;
            this.lblUnitsNote.Location = new System.Drawing.Point(6, 339);
            this.lblUnitsNote.Name = "lblUnitsNote";
            this.lblUnitsNote.Size = new System.Drawing.Size(320, 13);
            this.lblUnitsNote.TabIndex = 6;
            this.lblUnitsNote.Text = "( To remove item, right click on a sngle row and select \"Remove\" )";
            // 
            // lvwUnits
            // 
            this.lvwUnits.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwUnits.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chdType,
            this.chdPath});
            this.lvwUnits.ContextMenuStrip = this.cmsUnits;
            this.lvwUnits.FullRowSelect = true;
            this.lvwUnits.GridLines = true;
            this.lvwUnits.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwUnits.Location = new System.Drawing.Point(9, 89);
            this.lvwUnits.Name = "lvwUnits";
            this.lvwUnits.Size = new System.Drawing.Size(604, 247);
            this.lvwUnits.TabIndex = 5;
            this.lvwUnits.UseCompatibleStateImageBehavior = false;
            this.lvwUnits.View = System.Windows.Forms.View.Details;
            // 
            // chdType
            // 
            this.chdType.Text = "Type";
            // 
            // chdPath
            // 
            this.chdPath.Text = "Path";
            this.chdPath.Width = 400;
            // 
            // cmsUnits
            // 
            this.cmsUnits.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmDelete});
            this.cmsUnits.Name = "cmsUnits";
            this.cmsUnits.Size = new System.Drawing.Size(108, 26);
            this.cmsUnits.Opening += new System.ComponentModel.CancelEventHandler(this.cmsUnits_Opening);
            // 
            // tsmDelete
            // 
            this.tsmDelete.Name = "tsmDelete";
            this.tsmDelete.Size = new System.Drawing.Size(152, 22);
            this.tsmDelete.Text = "&Delete";
            this.tsmDelete.Click += new System.EventHandler(this.tsmDelete_Click);
            // 
            // rbnFolder
            // 
            this.rbnFolder.AutoSize = true;
            this.rbnFolder.Location = new System.Drawing.Point(142, 61);
            this.rbnFolder.Name = "rbnFolder";
            this.rbnFolder.Size = new System.Drawing.Size(85, 17);
            this.rbnFolder.TabIndex = 4;
            this.rbnFolder.Text = "Whole folder";
            this.rbnFolder.UseVisualStyleBackColor = true;
            // 
            // rbnFile
            // 
            this.rbnFile.AutoSize = true;
            this.rbnFile.Checked = true;
            this.rbnFile.Location = new System.Drawing.Point(142, 29);
            this.rbnFile.Name = "rbnFile";
            this.rbnFile.Size = new System.Drawing.Size(70, 17);
            this.rbnFile.TabIndex = 3;
            this.rbnFile.TabStop = true;
            this.rbnFile.Text = "Single file";
            this.rbnFile.UseVisualStyleBackColor = true;
            this.rbnFile.CheckedChanged += new System.EventHandler(this.rbnFile_CheckedChanged);
            // 
            // dsrFolder
            // 
            this.dsrFolder.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dsrFolder.Enabled = false;
            this.dsrFolder.Location = new System.Drawing.Point(245, 52);
            this.dsrFolder.MinimumSize = new System.Drawing.Size(200, 31);
            this.dsrFolder.Name = "dsrFolder";
            this.dsrFolder.Size = new System.Drawing.Size(288, 31);
            this.dsrFolder.TabIndex = 2;
            this.dsrFolder.Value = "";
            // 
            // fsrFile
            // 
            this.fsrFile.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fsrFile.Location = new System.Drawing.Point(245, 19);
            this.fsrFile.MaximumSize = new System.Drawing.Size(2000, 31);
            this.fsrFile.MinimumSize = new System.Drawing.Size(200, 31);
            this.fsrFile.Name = "fsrFile";
            this.fsrFile.Size = new System.Drawing.Size(288, 31);
            this.fsrFile.TabIndex = 1;
            this.fsrFile.Value = "";
            // 
            // lblUnits
            // 
            this.lblUnits.AutoSize = true;
            this.lblUnits.Location = new System.Drawing.Point(6, 31);
            this.lblUnits.Name = "lblUnits";
            this.lblUnits.Size = new System.Drawing.Size(116, 13);
            this.lblUnits.TabIndex = 0;
            this.lblUnits.Text = "Select files and folders:";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(559, 589);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(473, 589);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.ClientSizeChanged += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAndClose.Location = new System.Drawing.Point(330, 589);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(110, 23);
            this.btnSaveAndClose.TabIndex = 9;
            this.btnSaveAndClose.Text = "S&ave and Close";
            this.btnSaveAndClose.UseVisualStyleBackColor = true;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(21, 594);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(128, 13);
            this.lblMessage.TabIndex = 10;
            this.lblMessage.Text = "Some errors occured!";
            this.lblMessage.Visible = false;
            // 
            // ProfileDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 624);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnSaveAndClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gpbUnits);
            this.Controls.Add(this.gpbBasicInfo);
            this.Controls.Add(this.gpbVersions);
            this.MinimumSize = new System.Drawing.Size(662, 662);
            this.Name = "ProfileDetail";
            this.Text = "ProfileDetail";
            this.gpbVersions.ResumeLayout(false);
            this.gpbVersions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.nudNumberOfVersions)).EndInit();
            this.gpbBasicInfo.ResumeLayout(false);
            this.gpbBasicInfo.PerformLayout();
            this.gpbUnits.ResumeLayout(false);
            this.gpbUnits.PerformLayout();
            this.cmsUnits.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.Label lblFileNamePattern;
        private System.Windows.Forms.ComboBox cbxFileNamePattern;
        private System.Windows.Forms.GroupBox gpbVersions;
        private System.Windows.Forms.GroupBox gpbBasicInfo;
        private Controls.DirectorySelector dsrOutputFolder;
        private System.Windows.Forms.Label lblOutputFolder;
        private System.Windows.Forms.NumericUpDown nudNumberOfVersions;
        private System.Windows.Forms.Label lblNumberOfVersions;
        private System.Windows.Forms.Label lblNumberOfVersionsNote;
        private System.Windows.Forms.GroupBox gpbUnits;
        private System.Windows.Forms.RadioButton rbnFolder;
        private System.Windows.Forms.RadioButton rbnFile;
        private Controls.DirectorySelector dsrFolder;
        private Controls.FileSelector fsrFile;
        private System.Windows.Forms.Label lblUnits;
        private System.Windows.Forms.ListView lvwUnits;
        private System.Windows.Forms.Label lblUnitsNote;
        private System.Windows.Forms.ColumnHeader chdType;
        private System.Windows.Forms.ColumnHeader chdPath;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSaveAndClose;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ComboBox cbxPeriod;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ContextMenuStrip cmsUnits;
        private System.Windows.Forms.ToolStripMenuItem tsmDelete;
        private System.Windows.Forms.CheckBox cbxDisabled;
    }
}