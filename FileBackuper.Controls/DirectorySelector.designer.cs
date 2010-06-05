namespace FileBackuper.Controls
{
    partial class DirectorySelector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpenDirectory = new System.Windows.Forms.Button();
            this.tbxOpenDirectory = new System.Windows.Forms.TextBox();
            this.fbdOpenDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btnOpenDirectory
            // 
            this.btnOpenDirectory.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenDirectory.Location = new System.Drawing.Point(282, 3);
            this.btnOpenDirectory.Name = "btnOpenDirectory";
            this.btnOpenDirectory.Size = new System.Drawing.Size(75, 23);
            this.btnOpenDirectory.TabIndex = 0;
            this.btnOpenDirectory.Text = "Open";
            this.btnOpenDirectory.UseVisualStyleBackColor = true;
            this.btnOpenDirectory.Click += new System.EventHandler(this.btnOpenDirectory_Click);
            // 
            // tbxOpenDirectory
            // 
            this.tbxOpenDirectory.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxOpenDirectory.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tbxOpenDirectory.Location = new System.Drawing.Point(0, 5);
            this.tbxOpenDirectory.Name = "tbxOpenDirectory";
            this.tbxOpenDirectory.ReadOnly = true;
            this.tbxOpenDirectory.Size = new System.Drawing.Size(276, 20);
            this.tbxOpenDirectory.TabIndex = 1;
            // 
            // DirectorySelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbxOpenDirectory);
            this.Controls.Add(this.btnOpenDirectory);
            this.MinimumSize = new System.Drawing.Size(200, 31);
            this.Name = "DirectorySelector";
            this.Size = new System.Drawing.Size(357, 31);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenDirectory;
        private System.Windows.Forms.TextBox tbxOpenDirectory;
        private System.Windows.Forms.FolderBrowserDialog fbdOpenDirectory;
    }
}
