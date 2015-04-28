using System;
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
        public void UpdateLine(SocketBackEventArgs e)
        {
            form1.textBox1.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ms:::") + e.EndPoint + ":" + e.ReciveData + Environment.NewLine);
        }

        public string DealCommand(SocketBackEventArgs e)
        {
            UpdateLine(e);
            return (string)e.ReciveData;
        }
    }
}
