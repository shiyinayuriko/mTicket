﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace mTicket
{
    class PingCallback:ICallback
    {
        private Main form1;

        public PingCallback(TextBox text_log):base(text_log)
        {
        }

        protected override string OnDealCommand(string commandStr, string[] commandParams, string endPointName, IPEndPoint endPoint)
        {
            return null;
        }

        public override string DealCommand(SocketBackEventArgs e)
        {
            UpdateLine((string)e.ReciveData, (string)e.ReciveData, "raw", e.EndPoint);
            return (string) e.ReciveData;
        }
    }
}
