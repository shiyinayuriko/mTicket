using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mTicket
{
    class CodeTableCallback:ICallback
    {
        private Form1 form1;

        private CodeTable _codeTable;
        public CodeTableCallback(Form1 form1,string dataPath)
        {
            this.form1 = form1;
            _codeTable = DataHandler.LoadDatabase(dataPath);
        }
        public void UpdateLine(SocketBackEventArgs e)
        {
            form1.textBox1.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ms:::") + e.EndPoint + ":" + e.ReciveData + Environment.NewLine);
        }

        public string DealCommand(SocketBackEventArgs e)
        {

            UpdateLine(e);
            return _codeTable.GetJson();
        }
    }
}
