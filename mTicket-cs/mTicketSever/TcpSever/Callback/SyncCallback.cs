using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using mTicket.Beans;
using mTickLibs.codeData;
using Newtonsoft.Json;

namespace mTicket
{
    class SyncCallback : ICallback
    {
        private readonly DataBaseHandler _db;
        private ListView _liseview ;
        public SyncCallback(TextBox text_log, ListView liseview, DataBaseHandler db) : base(text_log)
        {
            _db = db;
            _liseview = liseview;
            CheckinData[] checkins = _db.GetAllCheckinDatas();
            foreach (var checkin in checkins)
            {
                var record = new ListViewItem(checkin.id + "");
                record.SubItems.Add(checkin.checkin_time);
                record.SubItems.Add("default");
                record.SubItems.Add(checkin.sync_time+"");
                _liseview.Items.Add(record);
            }
        }

        protected override string OnDealCommand(string commandStr, string[] commandParams, string endPointName, IPEndPoint endPoint)
        {
            long timestamp = Convert.ToInt64(commandParams[0]);
            CheckinData[] retCheckin = _db.GetCheckinDatas(timestamp);

            string json = commandParams[1].Trim();
            CheckinData[] checkins = JsonConvert.DeserializeObject<CheckinData[]>(json);
            long newTimestamp = _db.SetCheckinDatas(checkins, endPointName);

            LogTools.ScanLog(endPointName,commandParams[2]);

            foreach (var checkin in checkins)
            {
                var record = new ListViewItem(checkin.id + "");
                record.SubItems.Add(checkin.checkin_time);
                record.SubItems.Add(endPointName);
                record.SubItems.Add(timestamp+"");
                _liseview.Items.Add(record);
            }

            return (newTimestamp+1) +" " + JsonConvert.SerializeObject(retCheckin);
        }
    }
}
