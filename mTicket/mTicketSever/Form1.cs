using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mTicketSever
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
            TcpSever t = new TcpSever();
            t.Port = 8000;
            t.callbackList.Add("aaa", new SampleCallback(this));
            t.StartListen();
            button1.Enabled = false;
            
        }
    }
}
