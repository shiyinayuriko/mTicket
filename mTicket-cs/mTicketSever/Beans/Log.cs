using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace mTicket.Beans
{
    class Log
    {
        private const int LockOverTime = 10000;

        private static readonly ReaderWriterLock ScanLock = new ReaderWriterLock();
        private static readonly StreamWriter ScanWriter = new StreamWriter("./scan.log", true);
        public static void ScanLog(string endpoint, string str)
        {
            ScanLock.AcquireReaderLock(LockOverTime);
            string[] lines = str.Split('\n');
            foreach (var line in lines)
            {
                if(line.Trim().Equals("")) continue;
                ScanWriter.WriteLine(endpoint + ":" + line);
            }
            
            ScanWriter.Flush();
            ScanLock.ReleaseReaderLock();
        }

        private static readonly ReaderWriterLock NetLock = new ReaderWriterLock();
        private static readonly StreamWriter NetWriter = new StreamWriter("./net.log", true);
        public static void NetLog(string line)
        {
            NetLock.AcquireReaderLock(LockOverTime);
            NetWriter.WriteLine(line);
            NetWriter.Flush();
            NetLock.ReleaseReaderLock();
        }
    }
}
