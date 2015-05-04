using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
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
        }

        
        
        private void button_startListen_onClick(object sender, EventArgs e)
        {
            var t = new TcpSever { Port = Convert.ToInt32(Text_Port_Content.Text) };

            DataBaseHandler db = DataHandler.getDataBaseHandler(_dbFileName);

            //            t.CallbackList.Add("aaa", new SampleCallback(this));
            t.CallbackList.Add("ping", new PingCallback(text_log));
            t.CallbackList.Add("connect", new ConnectCallback(text_log));
            t.CallbackList.Add("codeTable", new CodeTableCallback(text_log, db));
            t.CallbackList.Add("syncCheckin", new SyncCallback(text_log, db));
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
