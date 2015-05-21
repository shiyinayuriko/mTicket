namespace mTicketClient
{
    partial class Scanner
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
            this.textBox_manually_input = new System.Windows.Forms.TextBox();
            this.label_manually_input = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView_checkin = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.code = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkin_time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.device = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_info = new System.Windows.Forms.ListView();
            this.Key = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label_client_state = new System.Windows.Forms.Label();
            this.lable_pass = new System.Windows.Forms.Label();
            this.text_scan_result = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_manually_input
            // 
            this.textBox_manually_input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_manually_input.Location = new System.Drawing.Point(87, 5);
            this.textBox_manually_input.Name = "textBox_manually_input";
            this.textBox_manually_input.Size = new System.Drawing.Size(285, 21);
            this.textBox_manually_input.TabIndex = 0;
            // 
            // label_manually_input
            // 
            this.label_manually_input.AutoSize = true;
            this.label_manually_input.Font = new System.Drawing.Font("宋体", 11F);
            this.label_manually_input.Location = new System.Drawing.Point(2, 11);
            this.label_manually_input.Name = "label_manually_input";
            this.label_manually_input.Size = new System.Drawing.Size(82, 15);
            this.label_manually_input.TabIndex = 1;
            this.label_manually_input.Text = "手动输入框";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listView_checkin);
            this.splitContainer1.Panel1.Controls.Add(this.label_manually_input);
            this.splitContainer1.Panel1.Controls.Add(this.textBox_manually_input);
            this.splitContainer1.Panel1.Controls.Add(this.label_client_state);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView_info);
            this.splitContainer1.Size = new System.Drawing.Size(375, 636);
            this.splitContainer1.SplitterDistance = 350;
            this.splitContainer1.TabIndex = 3;
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
            this.checkin_time,
            this.device});
            this.listView_checkin.FullRowSelect = true;
            this.listView_checkin.GridLines = true;
            this.listView_checkin.Location = new System.Drawing.Point(3, 64);
            this.listView_checkin.MultiSelect = false;
            this.listView_checkin.Name = "listView_checkin";
            this.listView_checkin.Size = new System.Drawing.Size(369, 283);
            this.listView_checkin.TabIndex = 4;
            this.listView_checkin.UseCompatibleStateImageBehavior = false;
            this.listView_checkin.View = System.Windows.Forms.View.Details;
            // 
            // id
            // 
            this.id.Text = "id";
            this.id.Width = 78;
            // 
            // code
            // 
            this.code.Text = "code";
            this.code.Width = 82;
            // 
            // checkin_time
            // 
            this.checkin_time.Text = "checkin_time";
            this.checkin_time.Width = 101;
            // 
            // device
            // 
            this.device.Text = "device";
            this.device.Width = 86;
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
            this.listView_info.Size = new System.Drawing.Size(369, 276);
            this.listView_info.TabIndex = 2;
            this.listView_info.UseCompatibleStateImageBehavior = false;
            this.listView_info.View = System.Windows.Forms.View.Details;
            // 
            // Key
            // 
            this.Key.Text = "属性";
            this.Key.Width = 172;
            // 
            // value
            // 
            this.value.Text = "值";
            this.value.Width = 166;
            // 
            // label_client_state
            // 
            this.label_client_state.AutoSize = true;
            this.label_client_state.Font = new System.Drawing.Font("宋体", 11F);
            this.label_client_state.Location = new System.Drawing.Point(20, 36);
            this.label_client_state.Name = "label_client_state";
            this.label_client_state.Size = new System.Drawing.Size(37, 15);
            this.label_client_state.TabIndex = 2;
            this.label_client_state.Text = "就绪";
            // 
            // lable_pass
            // 
            this.lable_pass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lable_pass.BackColor = System.Drawing.SystemColors.Window;
            this.lable_pass.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lable_pass.Location = new System.Drawing.Point(3, 3);
            this.lable_pass.Name = "lable_pass";
            this.lable_pass.Size = new System.Drawing.Size(366, 345);
            this.lable_pass.TabIndex = 3;
            this.lable_pass.Text = "┏ (゜ω゜)=☞";
            this.lable_pass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // text_scan_result
            // 
            this.text_scan_result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_scan_result.Location = new System.Drawing.Point(3, 3);
            this.text_scan_result.Multiline = true;
            this.text_scan_result.Name = "text_scan_result";
            this.text_scan_result.ReadOnly = true;
            this.text_scan_result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.text_scan_result.Size = new System.Drawing.Size(366, 276);
            this.text_scan_result.TabIndex = 4;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lable_pass);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.text_scan_result);
            this.splitContainer2.Size = new System.Drawing.Size(372, 636);
            this.splitContainer2.SplitterDistance = 350;
            this.splitContainer2.TabIndex = 5;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.Location = new System.Drawing.Point(12, 12);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer3.Size = new System.Drawing.Size(763, 642);
            this.splitContainer3.SplitterDistance = 381;
            this.splitContainer3.TabIndex = 4;
            // 
            // Scanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 666);
            this.Controls.Add(this.splitContainer3);
            this.Name = "Scanner";
            this.Text = "Scanner";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_manually_input;
        private System.Windows.Forms.Label label_manually_input;
        private System.Windows.Forms.Label label_client_state;
        private System.Windows.Forms.ListView listView_checkin;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader checkin_time;
        private System.Windows.Forms.ColumnHeader device;
        private System.Windows.Forms.ColumnHeader code;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView_info;
        private System.Windows.Forms.ColumnHeader Key;
        private System.Windows.Forms.ColumnHeader value;
        private System.Windows.Forms.Label lable_pass;
        public System.Windows.Forms.TextBox text_scan_result;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
    }
}