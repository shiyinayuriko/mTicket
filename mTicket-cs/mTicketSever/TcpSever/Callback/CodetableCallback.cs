using System;
using System.Collections.Generic;
using System.Linq;
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

        public override string DealCommand(SocketBackEventArgs e)
        {
            return _codeTable.GetJson();
        }
    }
}
