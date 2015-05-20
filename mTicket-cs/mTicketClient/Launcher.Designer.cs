namespace mTicketClient
{
    partial class Launcher
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
            this.text_Port_Content = new System.Windows.Forms.TextBox();
            this.label_IpAddress_Title = new System.Windows.Forms.Label();
            this.label_Port_Title = new System.Windows.Forms.Label();
            this.textBox_IpAddress_Content = new System.Windows.Forms.TextBox();
            this.button_search = new System.Windows.Forms.Button();
            this.button_connect = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // text_Port_Content
            // 
            this.text_Port_Content.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_Port_Content.Font = new System.Drawing.Font("宋体", 12F);
            this.text_Port_Content.Location = new System.Drawing.Point(114, 34);
            this.text_Port_Content.Name = "text_Port_Content";
            this.text_Port_Content.Size = new System.Drawing.Size(150, 26);
            this.text_Port_Content.TabIndex = 13;
            this.text_Port_Content.Text = "8000";
            this.text_Port_Content.TextChanged += new System.EventHandler(this.Text_Port_Content_TextChanged);
            // 
            // label_IpAddress_Title
            // 
            this.label_IpAddress_Title.AutoSize = true;
            this.label_IpAddress_Title.Font = new System.Drawing.Font("宋体", 12F);
            this.label_IpAddress_Title.Location = new System.Drawing.Point(4, 6);
            this.label_IpAddress_Title.Name = "label_IpAddress_Title";
            this.label_IpAddress_Title.Size = new System.Drawing.Size(96, 16);
            this.label_IpAddress_Title.TabIndex = 10;
            this.label_IpAddress_Title.Text = "服务器地址:";
            // 
            // label_Port_Title
            // 
            this.label_Port_Title.AutoSize = true;
            this.label_Port_Title.Font = new System.Drawing.Font("宋体", 12F);
            this.label_Port_Title.Location = new System.Drawing.Point(4, 37);
            this.label_Port_Title.Name = "label_Port_Title";
            this.label_Port_Title.Size = new System.Drawing.Size(96, 16);
            this.label_Port_Title.TabIndex = 12;
            this.label_Port_Title.Text = "服务器端口:";
            // 
            // textBox_IpAddress_Content
            // 
            this.textBox_IpAddress_Content.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_IpAddress_Content.Font = new System.Drawing.Font("宋体", 12F);
            this.textBox_IpAddress_Content.Location = new System.Drawing.Point(114, 3);
            this.textBox_IpAddress_Content.Name = "textBox_IpAddress_Content";
            this.textBox_IpAddress_Content.Size = new System.Drawing.Size(150, 26);
            this.textBox_IpAddress_Content.TabIndex = 14;
            this.textBox_IpAddress_Content.Text = "127.0.0.1";
            // 
            // button_search
            // 
            this.button_search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_search.Location = new System.Drawing.Point(3, 64);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(261, 50);
            this.button_search.TabIndex = 15;
            this.button_search.Text = "搜索服务器";
            this.button_search.UseVisualStyleBackColor = true;
            this.button_search.Click += new System.EventHandler(this.button_search_Click);
            // 
            // button_connect
            // 
            this.button_connect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_connect.Location = new System.Drawing.Point(3, 120);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(261, 50);
            this.button_connect.TabIndex = 16;
            this.button_connect.Text = "链接服务器";
            this.button_connect.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label_Port_Title);
            this.panel1.Controls.Add(this.button_connect);
            this.panel1.Controls.Add(this.label_IpAddress_Title);
            this.panel1.Controls.Add(this.button_search);
            this.panel1.Controls.Add(this.text_Port_Content);
            this.panel1.Controls.Add(this.textBox_IpAddress_Content);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 173);
            this.panel1.TabIndex = 17;
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 197);
            this.Controls.Add(this.panel1);
            this.Name = "Launcher";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox text_Port_Content;
        private System.Windows.Forms.Label label_IpAddress_Title;
        private System.Windows.Forms.Label label_Port_Title;
        private System.Windows.Forms.TextBox textBox_IpAddress_Content;
        private System.Windows.Forms.Button button_search;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.Panel panel1;
    }
}

