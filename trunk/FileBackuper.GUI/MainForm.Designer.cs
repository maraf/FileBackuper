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
            this.stsStatus = new System.Windows.Forms.StatusStrip();
            this.tlsMainMenu = new System.Windows.Forms.ToolStrip();
            this.lvwProfiles = new System.Windows.Forms.ListView();
            this.chdName = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.chdDestination = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // stsStatus
            // 
            this.stsStatus.Location = new System.Drawing.Point(0, 331);
            this.stsStatus.Name = "stsStatus";
            this.stsStatus.Size = new System.Drawing.Size(712, 22);
            this.stsStatus.TabIndex = 0;
            this.stsStatus.Text = "statusStrip1";
            // 
            // tlsMainMenu
            // 
            this.tlsMainMenu.Location = new System.Drawing.Point(0, 0);
            this.tlsMainMenu.Name = "tlsMainMenu";
            this.tlsMainMenu.Size = new System.Drawing.Size(712, 25);
            this.tlsMainMenu.TabIndex = 1;
            this.tlsMainMenu.Text = "toolStrip1";
            // 
            // lvwProfiles
            // 
            this.lvwProfiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chdName,
            this.chdDestination});
            this.lvwProfiles.FullRowSelect = true;
            this.lvwProfiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwProfiles.Location = new System.Drawing.Point(12, 28);
            this.lvwProfiles.MultiSelect = false;
            this.lvwProfiles.Name = "lvwProfiles";
            this.lvwProfiles.Size = new System.Drawing.Size(688, 271);
            this.lvwProfiles.TabIndex = 2;
            this.lvwProfiles.UseCompatibleStateImageBehavior = false;
            this.lvwProfiles.View = System.Windows.Forms.View.Details;
            // 
            // chdName
            // 
            this.chdName.Text = "Name";
            this.chdName.Width = 200;
            // 
            // chdDestination
            // 
            this.chdDestination.Text = "Ouput directory";
            this.chdDestination.Width = 400;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(593, 305);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(107, 23);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "Create Profile";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(12, 305);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(93, 305);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(204, 310);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(49, 13);
            this.lblCount.TabIndex = 6;
            this.lblCount.Text = "0 profiles";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 353);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.lvwProfiles);
            this.Controls.Add(this.tlsMainMenu);
            this.Controls.Add(this.stsStatus);
            this.Name = "MainForm";
            this.Text = "FileBackuper";
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
    }
}

