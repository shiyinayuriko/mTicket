using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mTickLibs.Tools
{
    public class Base64Tools
    {
        public static string ToBase64(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        public static string FromBase64(string base64)
        {
            byte[] outputb = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(outputb);
        }
    }
}
