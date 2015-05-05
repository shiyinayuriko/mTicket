using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        protected override string OnDealCommand(string commandStr, string[] commandParams, string endPointName, IPEndPoint endPoint)
        {
            return SettingBean.Instance.GetJson();
        }
    }
}
