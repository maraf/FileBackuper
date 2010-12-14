namespace FileBackuper.GUI
{
    partial class ConfigurationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
            this.btnSaveAndClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.fsrConfigPath = new FileBackuper.Controls.FileSelector();
            this.lblConfigPath = new System.Windows.Forms.Label();
            this.lblNewConfigPath = new System.Windows.Forms.Label();
            this.btnNewConfigPath = new System.Windows.Forms.Button();
            this.ofdNewConfigPath = new System.Windows.Forms.OpenFileDialog();
            this.dsrLogDir = new FileBackuper.Controls.DirectorySelector();
            this.lblLogDir = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAndClose.Location = new System.Drawing.Point(228, 109);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(110, 23);
            this.btnSaveAndClose.TabIndex = 12;
            this.btnSaveAndClose.Text = "S&ave and Close";
            this.btnSaveAndClose.UseVisualStyleBackColor = true;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(371, 109);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(457, 109);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // fsrConfigPath
            // 
            this.fsrConfigPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fsrConfigPath.Location = new System.Drawing.Point(12, 29);
            this.fsrConfigPath.MaximumSize = new System.Drawing.Size(2000, 31);
            this.fsrConfigPath.MinimumSize = new System.Drawing.Size(200, 31);
            this.fsrConfigPath.Name = "fsrConfigPath";
            this.fsrConfigPath.Size = new System.Drawing.Size(520, 31);
            this.fsrConfigPath.TabIndex = 13;
            this.fsrConfigPath.Value = "";
            // 
            // lblConfigPath
            // 
            this.lblConfigPath.AutoSize = true;
            this.lblConfigPath.Location = new System.Drawing.Point(12, 13);
            this.lblConfigPath.Name = "lblConfigPath";
            this.lblConfigPath.Size = new System.Drawing.Size(96, 13);
            this.lblConfigPath.TabIndex = 14;
            this.lblConfigPath.Text = "Path to profiles file:";
            // 
            // lblNewConfigPath
            // 
            this.lblNewConfigPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNewConfigPath.AutoSize = true;
            this.lblNewConfigPath.Location = new System.Drawing.Point(332, 13);
            this.lblNewConfigPath.Name = "lblNewConfigPath";
            this.lblNewConfigPath.Size = new System.Drawing.Size(119, 13);
            this.lblNewConfigPath.TabIndex = 15;
            this.lblNewConfigPath.Text = "Create new profiles file: ";
            // 
            // btnNewConfigPath
            // 
            this.btnNewConfigPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewConfigPath.Location = new System.Drawing.Point(457, 8);
            this.btnNewConfigPath.Name = "btnNewConfigPath";
            this.btnNewConfigPath.Size = new System.Drawing.Size(75, 23);
            this.btnNewConfigPath.TabIndex = 16;
            this.btnNewConfigPath.Text = "Create";
            this.btnNewConfigPath.UseVisualStyleBackColor = true;
            this.btnNewConfigPath.Click += new System.EventHandler(this.btnNewConfigPath_Click);
            // 
            // ofdNewConfigPath
            // 
            this.ofdNewConfigPath.CheckFileExists = false;
            this.ofdNewConfigPath.DefaultExt = "xml";
            this.ofdNewConfigPath.DereferenceLinks = false;
            this.ofdNewConfigPath.FileName = "Config.xml";
            this.ofdNewConfigPath.SupportMultiDottedExtensions = true;
            this.ofdNewConfigPath.Title = "Select new file for profiles (*.xml)";
            this.ofdNewConfigPath.ValidateNames = false;
            // 
            // dsrLogDir
            // 
            this.dsrLogDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dsrLogDir.Location = new System.Drawing.Point(142, 66);
            this.dsrLogDir.MinimumSize = new System.Drawing.Size(200, 31);
            this.dsrLogDir.Name = "dsrLogDir";
            this.dsrLogDir.Size = new System.Drawing.Size(390, 31);
            this.dsrLogDir.TabIndex = 17;
            this.dsrLogDir.Value = "";
            // 
            // lblLogDir
            // 
            this.lblLogDir.AutoSize = true;
            this.lblLogDir.Location = new System.Drawing.Point(12, 75);
            this.lblLogDir.Name = "lblLogDir";
            this.lblLogDir.Size = new System.Drawing.Size(124, 13);
            this.lblLogDir.TabIndex = 18;
            this.lblLogDir.Text = "Path to directory for logs:";
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 144);
            this.Controls.Add(this.lblLogDir);
            this.Controls.Add(this.dsrLogDir);
            this.Controls.Add(this.btnNewConfigPath);
            this.Controls.Add(this.lblNewConfigPath);
            this.Controls.Add(this.lblConfigPath);
            this.Controls.Add(this.fsrConfigPath);
            this.Controls.Add(this.btnSaveAndClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(560, 182);
            this.Name = "ConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FileBackuper - Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveAndClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private Controls.FileSelector fsrConfigPath;
        private System.Windows.Forms.Label lblConfigPath;
        private System.Windows.Forms.Label lblNewConfigPath;
        private System.Windows.Forms.Button btnNewConfigPath;
        private System.Windows.Forms.OpenFileDialog ofdNewConfigPath;
        private Controls.DirectorySelector dsrLogDir;
        private System.Windows.Forms.Label lblLogDir;
    }
}