using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using mTicket.Beans;
using mTickLibs.Tools;

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
            string line = "";
            string info = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + endPointName + "(" + endPoint + ")";
            line += info + Environment.NewLine;

            string reciveString2 = reciveString.Length > 160 ? reciveString.Substring(0, 150)+"......" : reciveString;
            line += "Recieve:" + reciveString2 + Environment.NewLine;
         
            string ret2 = ret.Length > 160 ? ret.Substring(0, 150) + "......" : ret;
            line += "Send:" + ret2 + Environment.NewLine ;
            _textLogBox.AppendText(line + Environment.NewLine);
            //TODO should implement in codeTableCallback
            LogTools.NetLog(info + Environment.NewLine + "Recieve:" + reciveString + Environment.NewLine + "Send:" + (reciveString.StartsWith("codeTable") ? ret2 : ret) + Environment.NewLine);

        }
        

        protected abstract string OnDealCommand(string commandStr, string[] commandParams, string endPointName, IPEndPoint endPoint);

        public virtual string DealCommand(SocketBackEventArgs e)
        {
            string reciveString = (string) e.ReciveData;
            var cmds = reciveString.Split(' ');

            var commandParams = new string[cmds.Length-2];
            for (int i = 2; i < cmds.Length; i++)
            {
                commandParams[i - 2] = Base64Tools.FromBase64(cmds[i]);
            }

            var endPointName = Base64Tools.FromBase64(cmds[1]);
            string ret = OnDealCommand(cmds[0],commandParams, endPointName, e.EndPoint);

            string nI = string.Join(" ", commandParams);
            UpdateLine(cmds[0] + " " + nI, ret, endPointName, e.EndPoint);

            return Base64Tools.ToBase64(ret);
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

    }
}
