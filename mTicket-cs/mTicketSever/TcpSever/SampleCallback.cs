﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mTicketSever
{
    class SampleCallback:ICallback
    {
        private Form1 form1;

        public SampleCallback(Form1 form1)
        {
            this.form1 = form1;
        }
        public void updateLine(SocketBackEventArgs e)
        {
            form1.textBox1.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ms:::") + e.EndPoint + ":" + e.ReciveData + Environment.NewLine);
        }

        public string dealCommand(SocketBackEventArgs e)
        {
            updateLine(e);
            return (string)e.ReciveData;
        }
    }
}
