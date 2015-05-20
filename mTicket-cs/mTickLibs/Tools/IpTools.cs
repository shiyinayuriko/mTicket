using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace mTickLibs.Tools
{
    public class IpTools
    {
        public static string GetIp()   //获取本地IP
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            for (int i = 0; i != ipEntry.AddressList.Length; i++)
            {
                if (ipEntry.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ipEntry.AddressList[i].ToString();
                }
            }
            return null;
        }

        public static string GetSeverIp(int port)
        {
            string localIp = GetIp();
            string ipPrx = localIp.Substring(0, localIp.LastIndexOf('.')+1);

            List<PingRet> pingRets = new List<PingRet>();
            for (int i = 1; i <= 255; i++)
            {

                PingRet prt = new PingRet
                {
                    mrEvent = new ManualResetEvent(false),
                    ip = ipPrx + i,
                    port = port
                };

                pingRets.Add(prt);
                ThreadPool.QueueUserWorkItem(Ping, prt);
            }
            foreach (var pingRet in pingRets)
            {
                pingRet.mrEvent.WaitOne();
                if (pingRet.ip != null) return pingRet.ip;
            }

            return null;
        }

        private static void Ping(object state)
        {
            string ip = ((PingRet) state).ip;
            int port = ((PingRet) state).port;

            PingReply reply = new Ping().Send(ip, 500);

            if (reply != null && reply.Status == IPStatus.Success)
            {
                try
                {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.SendTimeout = 5000;
                    socket.ReceiveTimeout = 5000;
                    socket.Connect(ip, port);

                    String pingLine = "ping " + new Random().NextDouble();

                    socket.Send(Encoding.UTF8.GetBytes(pingLine+"\n"));

                    var sr = new StreamReader(new NetworkStream(socket), Encoding.GetEncoding("UTF-8"));
                    var str = sr.ReadLine();

                    ((PingRet)state).ip = pingLine.Equals(str) ? ip : null;

                    socket.Close();
                }
                catch (Exception)
                {
                    ((PingRet)state).ip = null;
                }
            }
            else
            {
                ((PingRet) state).ip = null;
            }
            ((PingRet) state).mrEvent.Set();
        }
    }

    class PingRet
    {
        internal string ip;
        internal int port;
        internal ManualResetEvent mrEvent;
    }
}
