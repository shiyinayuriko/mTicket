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
            this.text_log = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_main = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView_checkin = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.from = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.syncTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_info = new System.Windows.Forms.ListView();
            this.Key = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel_start = new System.Windows.Forms.Panel();
            this.Text_Port_Content = new System.Windows.Forms.TextBox();
            this.button_Database_Browse = new System.Windows.Forms.Button();
            this.label_IpAddress_Title = new System.Windows.Forms.Label();
            this.label_Database_Content = new System.Windows.Forms.Label();
            this.label_IpAddress_Content = new System.Windows.Forms.Label();
            this.label_Database_Title = new System.Windows.Forms.Label();
            this.label_Port_Title = new System.Windows.Forms.Label();
            this.tabPage_log = new System.Windows.Forms.TabPage();
            this.openDbFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl.SuspendLayout();
            this.tabPage_main.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel_start.SuspendLayout();
            this.tabPage_log.SuspendLayout();
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
            // text_log
            // 
            this.text_log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_log.Location = new System.Drawing.Point(6, 6);
            this.text_log.Multiline = true;
            this.text_log.Name = "text_log";
            this.text_log.ReadOnly = true;
            this.text_log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.text_log.Size = new System.Drawing.Size(680, 558);
            this.text_log.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage_main);
            this.tabControl.Controls.Add(this.tabPage_log);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(773, 597);
            this.tabControl.TabIndex = 2;
            // 
            // tabPage_main
            // 
            this.tabPage_main.Controls.Add(this.splitContainer1);
            this.tabPage_main.Controls.Add(this.panel_start);
            this.tabPage_main.Location = new System.Drawing.Point(4, 22);
            this.tabPage_main.Name = "tabPage_main";
            this.tabPage_main.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_main.Size = new System.Drawing.Size(765, 571);
            this.tabPage_main.TabIndex = 0;
            this.tabPage_main.Text = "启动";
            this.tabPage_main.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.splitContainer1.Size = new System.Drawing.Size(439, 559);
            this.splitContainer1.SplitterDistance = 285;
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
            this.time,
            this.from,
            this.syncTime});
            this.listView_checkin.FullRowSelect = true;
            this.listView_checkin.GridLines = true;
            this.listView_checkin.Location = new System.Drawing.Point(3, 3);
            this.listView_checkin.MultiSelect = false;
            this.listView_checkin.Name = "listView_checkin";
            this.listView_checkin.Size = new System.Drawing.Size(433, 279);
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
            // time
            // 
            this.time.Text = "time";
            this.time.Width = 74;
            // 
            // from
            // 
            this.from.Text = "from";
            this.from.Width = 79;
            // 
            // syncTime
            // 
            this.syncTime.Text = "sync time";
            this.syncTime.Width = 84;
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
            this.listView_info.Size = new System.Drawing.Size(433, 264);
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
            // panel_start
            // 
            this.panel_start.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel_start.Controls.Add(this.Text_Port_Content);
            this.panel_start.Controls.Add(this.button_startListen);
            this.panel_start.Controls.Add(this.button_Database_Browse);
            this.panel_start.Controls.Add(this.label_IpAddress_Title);
            this.panel_start.Controls.Add(this.label_Database_Content);
            this.panel_start.Controls.Add(this.label_IpAddress_Content);
            this.panel_start.Controls.Add(this.label_Database_Title);
            this.panel_start.Controls.Add(this.label_Port_Title);
            this.panel_start.Location = new System.Drawing.Point(6, 6);
            this.panel_start.Name = "panel_start";
            this.panel_start.Size = new System.Drawing.Size(308, 559);
            this.panel_start.TabIndex = 9;
            // 
            // Text_Port_Content
            // 
            this.Text_Port_Content.Location = new System.Drawing.Point(83, 48);
            this.Text_Port_Content.Name = "Text_Port_Content";
            this.Text_Port_Content.Size = new System.Drawing.Size(100, 21);
            this.Text_Port_Content.TabIndex = 9;
            this.Text_Port_Content.Text = "8000";
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
            // tabPage_log
            // 
            this.tabPage_log.Controls.Add(this.text_log);
            this.tabPage_log.Location = new System.Drawing.Point(4, 22);
            this.tabPage_log.Name = "tabPage_log";
            this.tabPage_log.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_log.Size = new System.Drawing.Size(692, 570);
            this.tabPage_log.TabIndex = 1;
            this.tabPage_log.Text = "log";
            this.tabPage_log.UseVisualStyleBackColor = true;
            // 
            // openDbFileDialog
            // 
            this.openDbFileDialog.Filter = "sqlite3 files (*.db)|*.db";
            this.openDbFileDialog.RestoreDirectory = true;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 621);
            this.Controls.Add(this.tabControl);
            this.Name = "Form1";
            this.Text = "mTicket";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.button_Database_Browse_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.button_Database_Browse_DragEnter);
            this.tabControl.ResumeLayout(false);
            this.tabPage_main.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel_start.ResumeLayout(false);
            this.panel_start.PerformLayout();
            this.tabPage_log.ResumeLayout(false);
            this.tabPage_log.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_startListen;
        public System.Windows.Forms.TextBox text_log;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_main;
        private System.Windows.Forms.TabPage tabPage_log;
        private System.Windows.Forms.Label label_IpAddress_Title;
        private System.Windows.Forms.Label label_IpAddress_Content;
        private System.Windows.Forms.Label label_Port_Title;
        private System.Windows.Forms.Button button_Database_Browse;
        private System.Windows.Forms.Label label_Database_Content;
        private System.Windows.Forms.Label label_Database_Title;
        private System.Windows.Forms.Panel panel_start;
        private System.Windows.Forms.OpenFileDialog openDbFileDialog;
        private System.Windows.Forms.TextBox Text_Port_Content;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView_checkin;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader syncTime;
        private System.Windows.Forms.ColumnHeader time;
        private System.Windows.Forms.ListView listView_info;
        private System.Windows.Forms.ColumnHeader Key;
        private System.Windows.Forms.ColumnHeader value;
        private System.Windows.Forms.ColumnHeader from;
    }
}

