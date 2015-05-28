namespace mTicket
{
    partial class Main
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
            this.tabPage_importer = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listView_empty = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_import = new System.Windows.Forms.Button();
            this.button_export_database = new System.Windows.Forms.Button();
            this.label_data_number_content = new System.Windows.Forms.Label();
            this.label_data_number_title = new System.Windows.Forms.Label();
            this.tabPage_log = new System.Windows.Forms.TabPage();
            this.openDbFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openImportFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveDb = new System.Windows.Forms.SaveFileDialog();
            this.tabControl.SuspendLayout();
            this.tabPage_main.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel_start.SuspendLayout();
            this.tabPage_importer.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.text_log.Size = new System.Drawing.Size(717, 634);
            this.text_log.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage_main);
            this.tabControl.Controls.Add(this.tabPage_importer);
            this.tabControl.Controls.Add(this.tabPage_log);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(737, 672);
            this.tabControl.TabIndex = 2;
            // 
            // tabPage_main
            // 
            this.tabPage_main.AllowDrop = true;
            this.tabPage_main.Controls.Add(this.splitContainer1);
            this.tabPage_main.Controls.Add(this.panel_start);
            this.tabPage_main.Location = new System.Drawing.Point(4, 22);
            this.tabPage_main.Name = "tabPage_main";
            this.tabPage_main.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_main.Size = new System.Drawing.Size(729, 646);
            this.tabPage_main.TabIndex = 0;
            this.tabPage_main.Text = "启动";
            this.tabPage_main.UseVisualStyleBackColor = true;
            this.tabPage_main.DragDrop += new System.Windows.Forms.DragEventHandler(this.Tab_main_DragDrop);
            this.tabPage_main.DragEnter += new System.Windows.Forms.DragEventHandler(this.Tab_DragEnter);
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
            this.splitContainer1.Size = new System.Drawing.Size(403, 634);
            this.splitContainer1.SplitterDistance = 321;
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
            this.listView_checkin.Size = new System.Drawing.Size(397, 315);
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
            this.listView_info.Size = new System.Drawing.Size(397, 303);
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
            this.panel_start.Size = new System.Drawing.Size(308, 634);
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
            // tabPage_importer
            // 
            this.tabPage_importer.AllowDrop = true;
            this.tabPage_importer.Controls.Add(this.panel2);
            this.tabPage_importer.Controls.Add(this.panel1);
            this.tabPage_importer.Location = new System.Drawing.Point(4, 22);
            this.tabPage_importer.Name = "tabPage_importer";
            this.tabPage_importer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_importer.Size = new System.Drawing.Size(729, 646);
            this.tabPage_importer.TabIndex = 2;
            this.tabPage_importer.Text = "导入数据库";
            this.tabPage_importer.UseVisualStyleBackColor = true;
            this.tabPage_importer.DragDrop += new System.Windows.Forms.DragEventHandler(this.Tab_import_DragDrop);
            this.tabPage_importer.DragEnter += new System.Windows.Forms.DragEventHandler(this.Tab_DragEnter);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.listView_empty);
            this.panel2.Location = new System.Drawing.Point(186, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(537, 634);
            this.panel2.TabIndex = 1;
            // 
            // listView_empty
            // 
            this.listView_empty.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView_empty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_empty.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView_empty.FullRowSelect = true;
            this.listView_empty.GridLines = true;
            this.listView_empty.Location = new System.Drawing.Point(3, 3);
            this.listView_empty.MultiSelect = false;
            this.listView_empty.Name = "listView_empty";
            this.listView_empty.Size = new System.Drawing.Size(531, 628);
            this.listView_empty.TabIndex = 2;
            this.listView_empty.UseCompatibleStateImageBehavior = false;
            this.listView_empty.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "id";
            this.columnHeader1.Width = 163;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "code";
            this.columnHeader2.Width = 165;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.button_import);
            this.panel1.Controls.Add(this.button_export_database);
            this.panel1.Controls.Add(this.label_data_number_content);
            this.panel1.Controls.Add(this.label_data_number_title);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 634);
            this.panel1.TabIndex = 0;
            // 
            // button_import
            // 
            this.button_import.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_import.Location = new System.Drawing.Point(3, 24);
            this.button_import.Name = "button_import";
            this.button_import.Size = new System.Drawing.Size(168, 50);
            this.button_import.TabIndex = 0;
            this.button_import.Text = "导入数据文件";
            this.button_import.UseVisualStyleBackColor = true;
            this.button_import.Click += new System.EventHandler(this.button_import_Click);
            // 
            // button_export_database
            // 
            this.button_export_database.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_export_database.Enabled = false;
            this.button_export_database.Location = new System.Drawing.Point(3, 80);
            this.button_export_database.Name = "button_export_database";
            this.button_export_database.Size = new System.Drawing.Size(168, 50);
            this.button_export_database.TabIndex = 4;
            this.button_export_database.Text = "导出数据库";
            this.button_export_database.UseVisualStyleBackColor = true;
            this.button_export_database.Click += new System.EventHandler(this.button_export_database_Click);
            // 
            // label_data_number_content
            // 
            this.label_data_number_content.AutoSize = true;
            this.label_data_number_content.Location = new System.Drawing.Point(74, 9);
            this.label_data_number_content.Name = "label_data_number_content";
            this.label_data_number_content.Size = new System.Drawing.Size(11, 12);
            this.label_data_number_content.TabIndex = 2;
            this.label_data_number_content.Text = "0";
            // 
            // label_data_number_title
            // 
            this.label_data_number_title.AutoSize = true;
            this.label_data_number_title.Location = new System.Drawing.Point(3, 9);
            this.label_data_number_title.Name = "label_data_number_title";
            this.label_data_number_title.Size = new System.Drawing.Size(65, 12);
            this.label_data_number_title.TabIndex = 1;
            this.label_data_number_title.Text = "数据条数：";
            // 
            // tabPage_log
            // 
            this.tabPage_log.Controls.Add(this.text_log);
            this.tabPage_log.Location = new System.Drawing.Point(4, 22);
            this.tabPage_log.Name = "tabPage_log";
            this.tabPage_log.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_log.Size = new System.Drawing.Size(729, 646);
            this.tabPage_log.TabIndex = 1;
            this.tabPage_log.Text = "通讯记录";
            this.tabPage_log.UseVisualStyleBackColor = true;
            // 
            // openDbFileDialog
            // 
            this.openDbFileDialog.Filter = "sqlite3 files (*.db)|*.db";
            this.openDbFileDialog.RestoreDirectory = true;
            // 
            // openImportFileDialog
            // 
            this.openImportFileDialog.Filter = "数据源文件(*.db,*.xls,*.xlsx)|*.db;*.xls;*.xlsx|sqlite3 files (*.db)|*.db|Microsoft Ex" +
    "cel file(*.xls,*.xlsx)|*.xls;*.xlsx";
            this.openImportFileDialog.RestoreDirectory = true;
            // 
            // saveDb
            // 
            this.saveDb.DefaultExt = "db";
            this.saveDb.Filter = "All files (*.*)|*.*|sqlite3 files (*.db)|*.db";
            this.saveDb.FilterIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 696);
            this.Controls.Add(this.tabControl);
            this.Name = "Form1";
            this.Text = "mTicket";
            this.tabControl.ResumeLayout(false);
            this.tabPage_main.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel_start.ResumeLayout(false);
            this.panel_start.PerformLayout();
            this.tabPage_importer.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.TabPage tabPage_importer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_import;
        private System.Windows.Forms.Label label_data_number_title;
        private System.Windows.Forms.Label label_data_number_content;
        private System.Windows.Forms.Button button_export_database;
        private System.Windows.Forms.OpenFileDialog openImportFileDialog;
        private System.Windows.Forms.SaveFileDialog saveDb;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView listView_empty;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}

