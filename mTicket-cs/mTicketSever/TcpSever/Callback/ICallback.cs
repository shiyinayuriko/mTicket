using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
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

        protected void OnUpdateLine(string reciveString, string ret, string endPointName, IPEndPoint endPoint)
        {
            string info = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + endPointName + "(" + endPoint + ")";
            _textLogBox.AppendText(info + Environment.NewLine);
            
            string reciveString2 = reciveString.Length > 160 ? reciveString.Substring(0, 150)+"......" : reciveString;
            _textLogBox.AppendText("Recieve:" + reciveString2 + Environment.NewLine);
         
            string ret2 = ret.Length > 160 ? ret.Substring(0, 150) + "......" : ret;
            _textLogBox.AppendText("Send:" + ret2 + Environment.NewLine);
            _textLogBox.AppendText(Environment.NewLine);
        }
        

        protected abstract string OnDealCommand(string commandStr, string[] commandParams, string endPointName, IPEndPoint endPoint);

        public virtual string DealCommand(SocketBackEventArgs e)
        {
            string reciveString = (string) e.ReciveData;
            var cmds = reciveString.Split(' ');

            var commandParams = new string[cmds.Length-2];
            for (int i = 2; i < cmds.Length; i++)
            {
                commandParams[i - 2] = FromBase64(cmds[i]);
            }

            var endPointName =  FromBase64(cmds[1]);
            string ret = OnDealCommand(cmds[0],commandParams, endPointName, e.EndPoint);

            string nI = string.Join(" ", commandParams);
            UpdateLine(cmds[0] + " " + nI, ret, endPointName, e.EndPoint);

            return ToBase64(ret);
        }


        public delegate void OnUpdateLineM(string reciveString, string ret, string endPointName, IPEndPoint endPoint);

        protected void UpdateLine(string reciveString, string ret, string endPointName, IPEndPoint endPoint)
        {
            if (_textLogBox.InvokeRequired)
            {
                _textLogBox.Invoke(new OnUpdateLineM(OnUpdateLine), reciveString, ret, endPointName, endPoint);
            }
            else
            {
                OnUpdateLine(reciveString, ret, endPointName, endPoint);
            }
      
        }



        protected static string ToBase64(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        protected static string FromBase64(string base64)
        {
            byte[] outputb = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(outputb);
        }
    }
}
