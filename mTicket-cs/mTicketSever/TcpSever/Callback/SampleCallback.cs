using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mTicket
{
    class SampleCallback:ICallback
    {
        private Form1 form1;

        public SampleCallback(TextBox text_log) : base(text_log)
        {
        }

        public override string DealCommand(SocketBackEventArgs e)
        {
            return (string)e.ReciveData;
        }
    }
}
