using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mTickLibs.Tools
{
    public class TimeTools
    {
        public static long CurrentTimeMillis()
        {
            long time = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
            return time;
        }
    }
}
