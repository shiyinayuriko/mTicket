using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mTicket
{
    class ConnectCallback:ICallback
    {
        private Form1 form1;

        public ConnectCallback(Form1 form1)
        {
            this.form1 = form1;
        }
        public void UpdateLine(SocketBackEventArgs e)
        {
            form1.textBox1.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ms:::") + e.EndPoint + ":" + e.ReciveData + Environment.NewLine);
        }

        public string DealCommand(SocketBackEventArgs e)
        {

            UpdateLine(e);
            return SettingBean.Instance.GetJson();
        }
    }
}
