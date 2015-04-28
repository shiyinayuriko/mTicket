using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace mTicketSever
{
    class SettingBean
    {
        [JsonIgnore]
        public static readonly SettingBean Instance = GetSettings();
        
        public int timer;

        public string GetJson()
        {
            var serialStr = JsonConvert.SerializeObject(this);
            return serialStr;
        }

        public static SettingBean GetSettings()
        {
            var ret = new SettingBean
            {
                timer = Convert.ToInt32(ConfigurationManager.AppSettings["timer"])
            };
            return ret;
        }
    }


}
