using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace mTicket
{
    public abstract class ICallback
    {
        private readonly TextBox _textLogBox;
        protected ICallback(TextBox textbox)
        {
            _textLogBox = textbox;
        }

        public virtual void UpdateLine(SocketBackEventArgs e, string ret)
        {
            string info = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + e.EndPoint + " " + HostNameHolder.Instance.GetName(e.EndPoint.Address);
            _textLogBox.AppendText(info + Environment.NewLine);
            _textLogBox.AppendText("Recieve:" + e.ReciveData + Environment.NewLine);
            _textLogBox.AppendText("Send:" + ret + Environment.NewLine);
        }

        public abstract string DealCommand(SocketBackEventArgs e);
    }
}
