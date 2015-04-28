using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            t.CallbackList.Add("aaa", new SampleCallback(this));
            t.CallbackList.Add("ping", new PingCallback(this));
            t.CallbackList.Add("connect", new ConnectCallback(this));
            t.CallbackList.Add("codeTable", new CodeTableCallback(this, "C:/Users/文戎/Desktop/data.db"));
            t.StartListen();
            button1.Enabled = false;          
        }
    }
}
