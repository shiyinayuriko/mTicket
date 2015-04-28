namespace mTicket
{
    partial class Importer
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.infoLabel = new System.Windows.Forms.Label();
            this.buttonExcute = new System.Windows.Forms.Button();
            this.saveDb = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(13, 13);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(23, 12);
            this.infoLabel.TabIndex = 0;
            this.infoLabel.Text = "0/0\r\n";
            // 
            // buttonExcute
            // 
            this.buttonExcute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExcute.Enabled = false;
            this.buttonExcute.Location = new System.Drawing.Point(12, 98);
            this.buttonExcute.Name = "buttonExcute";
            this.buttonExcute.Size = new System.Drawing.Size(298, 28);
            this.buttonExcute.TabIndex = 1;
            this.buttonExcute.Text = "生成";
            this.buttonExcute.UseVisualStyleBackColor = true;
            this.buttonExcute.Click += new System.EventHandler(this.buttonExcute_Click);
            // 
            // saveDb
            // 
            this.saveDb.DefaultExt = "db";
            this.saveDb.Filter = "All files (*.*)|*.*|sqlite3 files (*.db)|*.db";
            this.saveDb.FilterIndex = 2;
            // 
            // Importer
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 138);
            this.Controls.Add(this.buttonExcute);
            this.Controls.Add(this.infoLabel);
            this.Name = "Importer";
            this.Text = "Importer";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Importer_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Importer_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Button buttonExcute;
        private System.Windows.Forms.SaveFileDialog saveDb;


    }
}

