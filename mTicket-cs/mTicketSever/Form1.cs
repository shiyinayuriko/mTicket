using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var t = new TcpSever {Port = 8000};

            DataBaseHandler db = DataHandler.getDataBaseHandler("C:/Users/文戎/Desktop/data.db");

            t.CallbackList.Add("aaa", new SampleCallback(this));
            t.CallbackList.Add("ping", new PingCallback(this));
            t.CallbackList.Add("connect", new ConnectCallback(this));
            t.CallbackList.Add("codeTable", new CodeTableCallback(this, db));
            t.CallbackList.Add("syncCheckin", new SyncCallback(this));
            t.StartListen();
            button1.Enabled = false;
            db.SetCheckinDatas(new[] { new CheckinData() { checkin_time = "1-2-3-4", id = 222 } });
        }
    }
}
