namespace mTicket
{
    partial class Form1
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
            this.button_startListen = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Text_Port_Content = new System.Windows.Forms.TextBox();
            this.button_Database_Browse = new System.Windows.Forms.Button();
            this.label_IpAddress_Title = new System.Windows.Forms.Label();
            this.label_Database_Content = new System.Windows.Forms.Label();
            this.label_IpAddress_Content = new System.Windows.Forms.Label();
            this.label_Database_Title = new System.Windows.Forms.Label();
            this.label_Port_Title = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.openDbFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView_checkin = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.code = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_info = new System.Windows.Forms.ListView();
            this.Key = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_startListen
            // 
            this.button_startListen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_startListen.Enabled = false;
            this.button_startListen.Location = new System.Drawing.Point(14, 148);
            this.button_startListen.Name = "button_startListen";
            this.button_startListen.Size = new System.Drawing.Size(282, 51);
            this.button_startListen.TabIndex = 0;
            this.button_startListen.Text = "开始监听";
            this.button_startListen.UseVisualStyleBackColor = true;
            this.button_startListen.Click += new System.EventHandler(this.button_startListen_onClick);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(6, 6);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(722, 564);
            this.textBox1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(742, 602);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(734, 576);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.Text_Port_Content);
            this.panel1.Controls.Add(this.button_startListen);
            this.panel1.Controls.Add(this.button_Database_Browse);
            this.panel1.Controls.Add(this.label_IpAddress_Title);
            this.panel1.Controls.Add(this.label_Database_Content);
            this.panel1.Controls.Add(this.label_IpAddress_Content);
            this.panel1.Controls.Add(this.label_Database_Title);
            this.panel1.Controls.Add(this.label_Port_Title);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(308, 564);
            this.panel1.TabIndex = 9;
            // 
            // Text_Port_Content
            // 
            this.Text_Port_Content.Location = new System.Drawing.Point(83, 48);
            this.Text_Port_Content.Name = "Text_Port_Content";
            this.Text_Port_Content.Size = new System.Drawing.Size(100, 21);
            this.Text_Port_Content.TabIndex = 9;
            this.Text_Port_Content.Text = "8080";
            this.Text_Port_Content.TextChanged += new System.EventHandler(this.Text_Port_Content_TextChanged);
            // 
            // button_Database_Browse
            // 
            this.button_Database_Browse.Location = new System.Drawing.Point(233, 105);
            this.button_Database_Browse.Name = "button_Database_Browse";
            this.button_Database_Browse.Size = new System.Drawing.Size(63, 23);
            this.button_Database_Browse.TabIndex = 8;
            this.button_Database_Browse.Text = "浏览";
            this.button_Database_Browse.UseVisualStyleBackColor = true;
            this.button_Database_Browse.Click += new System.EventHandler(this.button_Database_Browse_Click);
            // 
            // label_IpAddress_Title
            // 
            this.label_IpAddress_Title.AutoSize = true;
            this.label_IpAddress_Title.Location = new System.Drawing.Point(12, 18);
            this.label_IpAddress_Title.Name = "label_IpAddress_Title";
            this.label_IpAddress_Title.Size = new System.Drawing.Size(65, 12);
            this.label_IpAddress_Title.TabIndex = 1;
            this.label_IpAddress_Title.Text = "本机地址：";
            // 
            // label_Database_Content
            // 
            this.label_Database_Content.AutoSize = true;
            this.label_Database_Content.Location = new System.Drawing.Point(27, 110);
            this.label_Database_Content.Name = "label_Database_Content";
            this.label_Database_Content.Size = new System.Drawing.Size(41, 12);
            this.label_Database_Content.TabIndex = 7;
            this.label_Database_Content.Text = "无数据";
            // 
            // label_IpAddress_Content
            // 
            this.label_IpAddress_Content.AutoSize = true;
            this.label_IpAddress_Content.Location = new System.Drawing.Point(83, 18);
            this.label_IpAddress_Content.Name = "label_IpAddress_Content";
            this.label_IpAddress_Content.Size = new System.Drawing.Size(53, 12);
            this.label_IpAddress_Content.TabIndex = 2;
            this.label_IpAddress_Content.Text = "正在查找";
            // 
            // label_Database_Title
            // 
            this.label_Database_Title.AutoSize = true;
            this.label_Database_Title.Location = new System.Drawing.Point(12, 86);
            this.label_Database_Title.Name = "label_Database_Title";
            this.label_Database_Title.Size = new System.Drawing.Size(65, 12);
            this.label_Database_Title.TabIndex = 6;
            this.label_Database_Title.Text = "数据文件：";
            // 
            // label_Port_Title
            // 
            this.label_Port_Title.AutoSize = true;
            this.label_Port_Title.Location = new System.Drawing.Point(12, 51);
            this.label_Port_Title.Name = "label_Port_Title";
            this.label_Port_Title.Size = new System.Drawing.Size(65, 12);
            this.label_Port_Title.TabIndex = 3;
            this.label_Port_Title.Text = "监听端口：";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(734, 576);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // openDbFileDialog
            // 
            this.openDbFileDialog.Filter = "sqlite3 files (*.db)|*.db";
            this.openDbFileDialog.RestoreDirectory = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(320, 6);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listView_checkin);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView_info);
            this.splitContainer1.Size = new System.Drawing.Size(408, 564);
            this.splitContainer1.SplitterDistance = 288;
            this.splitContainer1.TabIndex = 10;
            // 
            // listView_checkin
            // 
            this.listView_checkin.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView_checkin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_checkin.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.code,
            this.time});
            this.listView_checkin.FullRowSelect = true;
            this.listView_checkin.GridLines = true;
            this.listView_checkin.Location = new System.Drawing.Point(3, 3);
            this.listView_checkin.MultiSelect = false;
            this.listView_checkin.Name = "listView_checkin";
            this.listView_checkin.Size = new System.Drawing.Size(402, 282);
            this.listView_checkin.TabIndex = 1;
            this.listView_checkin.UseCompatibleStateImageBehavior = false;
            this.listView_checkin.View = System.Windows.Forms.View.Details;
            this.listView_checkin.SelectedIndexChanged += new System.EventHandler(this.listView_checkin_SelectedIndexChanged);
            // 
            // id
            // 
            this.id.Text = "id";
            this.id.Width = 111;
            // 
            // code
            // 
            this.code.DisplayIndex = 2;
            this.code.Text = "code";
            // 
            // time
            // 
            this.time.DisplayIndex = 1;
            this.time.Text = "time";
            this.time.Width = 98;
            // 
            // listView_info
            // 
            this.listView_info.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_info.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Key,
            this.value});
            this.listView_info.GridLines = true;
            this.listView_info.Location = new System.Drawing.Point(3, 3);
            this.listView_info.Name = "listView_info";
            this.listView_info.Size = new System.Drawing.Size(402, 266);
            this.listView_info.TabIndex = 1;
            this.listView_info.UseCompatibleStateImageBehavior = false;
            this.listView_info.View = System.Windows.Forms.View.Details;
            // 
            // Key
            // 
            this.Key.Text = "属性";
            this.Key.Width = 181;
            // 
            // value
            // 
            this.value.Text = "值";
            this.value.Width = 175;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 626);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.button_Database_Browse_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.button_Database_Browse_DragEnter);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_startListen;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label_IpAddress_Title;
        private System.Windows.Forms.Label label_IpAddress_Content;
        private System.Windows.Forms.Label label_Port_Title;
        private System.Windows.Forms.Button button_Database_Browse;
        private System.Windows.Forms.Label label_Database_Content;
        private System.Windows.Forms.Label label_Database_Title;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.OpenFileDialog openDbFileDialog;
        private System.Windows.Forms.TextBox Text_Port_Content;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView_checkin;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader code;
        private System.Windows.Forms.ColumnHeader time;
        private System.Windows.Forms.ListView listView_info;
        private System.Windows.Forms.ColumnHeader Key;
        private System.Windows.Forms.ColumnHeader value;
    }
}

