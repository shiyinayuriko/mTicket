using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using mTicket.Beans;
using mTickLibs.codeData;

namespace mTicket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            label_IpAddress_Content.Text = GetIp();
        }

        private string _dbFileName;
        private void button_Database_Browse_Click(object sender, EventArgs e)
        {
            if (openDbFileDialog.ShowDialog() == DialogResult.OK)
            {
                _dbFileName = openDbFileDialog.FileName;
                UpdateFilename();
            }
        }

        private void button_Database_Browse_DragDrop(object sender, DragEventArgs e)
        {
            _dbFileName  = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            UpdateFilename();
        }

        private void button_Database_Browse_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None;
        }

        private void UpdateFilename()
        {
            label_Database_Content.Text = _dbFileName;
            button_startListen.Enabled = true;
            _db = DataHandler.getDataBaseHandler(_dbFileName);
        }


        private DataBaseHandler _db;
        private void button_startListen_onClick(object sender, EventArgs e)
        {
            var t = new TcpSever { Port = Convert.ToInt32(Text_Port_Content.Text) };


            //            t.CallbackList.Add("aaa", new SampleCallback(this));
            t.CallbackList.Add("ping", new PingCallback(text_log));
            t.CallbackList.Add("connect", new ConnectCallback(text_log));
            t.CallbackList.Add("codeTable", new CodeTableCallback(text_log, _db));
            t.CallbackList.Add("syncCheckin", new SyncCallback(text_log, listView_checkin, _db));
            t.StartListen();
            button_startListen.Enabled = false;
            button_Database_Browse.Enabled = false;
            Text_Port_Content.Enabled = false;

            //            db.SetCheckinDatas(new[] { new CheckinData() { checkin_time = "1-2-3-4", id = 222 } });
            //            var tmp = db.GetCheckinDatas(1);
        }

        private void Text_Port_Content_TextChanged(object sender, EventArgs e)
        {
            string newPort = Text_Port_Content.Text.ToCharArray().Where(char.IsNumber).Aggregate("", (current, ch) => current + ch);
            Text_Port_Content.Text = newPort;
        }

        private void listView_checkin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_checkin.SelectedItems.Count == 0) return;
            int id = Convert.ToInt32(listView_checkin.SelectedItems[0].SubItems[0].Text);
            CodeDataDetail codeTable = _db.LoadCodeDataDetail(id);
            listView_info.Items.Clear();
            foreach (var pair in codeTable.info)
            {
                var record = new ListViewItem(pair.Key);
                record.SubItems.Add(pair.Value);
                listView_info.Items.Add(record);
            }
            for (var i=0;i<codeTable.checkin.Length;i++)
            {
                var record = new ListViewItem("进出记录-"+(i+1));
                record.SubItems.Add(codeTable.checkin[i].checkin_time);
                listView_info.Items.Add(record);
            }
        }
        private static string GetIp()   //获取本地IP
        {
            IPHostEntry IpEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            for (int i = 0; i != IpEntry.AddressList.Length; i++)
            {
                if (IpEntry.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return IpEntry.AddressList[i].ToString();
                }
            }
            return "未成功获取..";
        }
    }
}
