using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mTicketClient.Properties;

namespace mTicketClient.Modules
{
    public class LocalSettings
    {
        public static long GetSyncTimetamp()
        {
            return Settings.Default.SyncTime;
        }

        public static void SaveSyncTimestamp(long timestamp)
        {
            Settings.Default.SyncTime = timestamp;
            Settings.Default.Save();
        }

        private static readonly StringBuilder ScanLog = new StringBuilder();
        public static void AppendScanLog(String logLine)
        {
            String dateStr = DateTime.Now.ToString("yyyy-MM-dd hh:mm;ss");
            ScanLog.Append(dateStr + ":" + logLine + "\n");
        }
        public static string GetScanLog()
        {
            return ScanLog.ToString();
        }
        public static void ClearScanLog()
        {
            ScanLog.Length = 0;
        }
    }
}
