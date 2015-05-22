using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mTicket.Beans;
using MSScriptControl;
using Newtonsoft.Json;

namespace mTicketClient
{
    public class LogicChecker
    {
        private const String PreCheckin = "function pre(tmps) {var tmp = eval('(' + tmps + ')');return checkin(tmp);}";

        private readonly ScriptControlClass _sc;
        public LogicChecker(string script)
        {
            _sc = new ScriptControlClass();
            _sc.Language = "javascript";

            _sc.AddCode(PreCheckin);
            _sc.AddCode(script);
        }

        public bool Checkin(CodeDataDetail codeData)
        {
            
            string json = JsonConvert.SerializeObject(codeData);
            var result = _sc.Eval("pre('" + json + "')");
            return (bool)result;
        }
    }
}
