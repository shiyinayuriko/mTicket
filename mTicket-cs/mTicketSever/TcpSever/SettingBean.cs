using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace mTicket
{
    class SettingBean
    {
        [JsonIgnore]
        public static readonly SettingBean Instance = GetSettings();

        public int timer;
        public int proress_step_update_database;
        public int tcp_timeout;

        public string GetJson()
        {
            var serialStr = JsonConvert.SerializeObject(this);
            return serialStr;
        }

        public static SettingBean GetSettings()
        {
            var ret = new SettingBean
            {
                timer = Convert.ToInt32(ConfigurationManager.AppSettings["timer"]),
                proress_step_update_database = Convert.ToInt32(ConfigurationManager.AppSettings["proress_step_update_database"])
                tcp_timeout = Convert.ToInt32(ConfigurationManager.AppSettings["tcp_timeout"])
            };
            return ret;
        }
    }


}
