using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace mTicket
{
    class CodeTableCallback:ICallback
    {
        private readonly CodeTable _codeTable;
        public CodeTableCallback(TextBox text_log, DataBaseHandler dbHandler):base(text_log)
        {
            _codeTable = dbHandler.LoadCodeTable();
        }

        protected override string OnDealCommand(string commandStr, string[] commandParams, string endPointName, IPEndPoint endPoint)
        {
            return _codeTable.GetJson();
        }
    }
}
