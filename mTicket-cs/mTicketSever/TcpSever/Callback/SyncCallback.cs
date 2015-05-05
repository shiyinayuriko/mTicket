using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using mTickLibs.codeData;
using Newtonsoft.Json;

namespace mTicket
{
    class SyncCallback : ICallback
    {
        private readonly DataBaseHandler _db;
        public SyncCallback(TextBox text_log, DataBaseHandler db) : base(text_log)
        {
            _db = db;
        }

        protected override string OnDealCommand(string commandStr, string[] commandParams, string endPointName, IPEndPoint endPoint)
        {
            long timestamp = Convert.ToInt64(commandParams[0]);
            CheckinData[] retCheckin = _db.GetCheckinDatas(timestamp);

            string json = commandParams[1].Trim();
            long newTimestamp = _db.SetCheckinDatas(JsonConvert.DeserializeObject<CheckinData[]>(json),endPointName);

            return (newTimestamp+1) +" " + JsonConvert.SerializeObject(retCheckin);
        }
    }
}
