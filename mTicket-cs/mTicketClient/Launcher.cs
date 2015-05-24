using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using mTicketClient.Properties;
using mTickLibs.Tools;

namespace mTicketClient
{
    public partial class Launcher : Form
    {
        public Launcher()
        {
            InitializeComponent();
        }
        private void Text_Port_Content_TextChanged(object sender, EventArgs e)
        {
            string newPort = textBox_Port_Content.Text.ToCharArray().Where(char.IsNumber).Aggregate("", (current, ch) => current + ch);
            textBox_Port_Content.Text = newPort;
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            textBox_Port_Content.Enabled = false;
            textBox_IpAddress_Content.Enabled = false;
            button_search.Enabled = false;
            button_connect.Enabled = false;

            Thread t = new Thread(SearchHost);
            t.Start();
        }
        private void SearchHost()
        {
            string ip = IpTools.GetSeverIp(Convert.ToInt32(textBox_Port_Content.Text));
            if (textBox_IpAddress_Content.InvokeRequired)
            {
                textBox_IpAddress_Content.Invoke(new OnEnableSearch(EnableSearch), ip);
            }
            else
            {
                EnableSearch(ip);
            }
        }
        public delegate void OnEnableSearch(string ip);

        private void EnableSearch(string ip)
        {
            textBox_Port_Content.Enabled = true;
            textBox_IpAddress_Content.Enabled = true;
            button_search.Enabled = true;
            button_connect.Enabled = true;
            if (ip == null)
                MessageBox.Show(Resources.Launcher_EnableSearch_host_not_found);
            else textBox_IpAddress_Content.Text = ip;
        }


        internal string ResultIpAddr;
        internal int ResultPort;
        private void button_connect_Click(object sender, EventArgs e)
        {
            string ipAddrS = textBox_IpAddress_Content.Text;
            string portS = textBox_Port_Content.Text;

            if (!TestIpPort(ipAddrS, portS))
            {
                MessageBox.Show(Resources.Launcher_button_connect_Click_error_host_settings);
            }

            string ipAddr = ipAddrS;
            int port = Int32.Parse(portS);

            DialogResult = DialogResult.OK;
            ResultIpAddr = ipAddr;
            ResultPort = port;
            Close();
        }

        private bool TestIpPort(string ip, string port)
        {
            try
            {
               Int32.Parse(port);
            }
            catch (Exception)
            {
                return false;
            }
            IPAddress ip2;
            
            return IPAddress.TryParse(ip, out ip2);
        }
    }
}
