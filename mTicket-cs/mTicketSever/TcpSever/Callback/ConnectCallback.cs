using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mTicket
{
    class ConnectCallback:ICallback
    {
        private Form1 form1;

        public ConnectCallback(TextBox text_log):base(text_log)
        {
        }

        public override string DealCommand(SocketBackEventArgs e)
        {
            var recieveStr = (string) e.ReciveData;
            var name = recieveStr.Substring(recieveStr.IndexOf(' ')+1);
            HostNameHolder.Instance.SetName(e.EndPoint.Address,name);
            return SettingBean.Instance.GetJson();
        }
    }
}
