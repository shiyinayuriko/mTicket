using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using mTicket.Beans;

namespace mTicket
{
    class TcpSever
    {
        private Socket _server;
        public int Port { get; set; }

        public void StartListen()
        {
            ThreadPool.QueueUserWorkItem(ListenProcess, new object());
        }
        private void ListenProcess(object state)
        {
            _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _server.Bind(new IPEndPoint(IPAddress.Any, Port));
            while (true)
            {
                _server.Listen(100);
                ThreadPool.QueueUserWorkItem(ReciveProcess, _server.Accept());
            }

        }
        private void ReciveProcess(object p)
        {
            var clientCon = (Socket)p;
            try 
            {
                var ns = new NetworkStream(clientCon);
                var sr = new StreamReader(ns, Encoding.GetEncoding("UTF-8"));
                var str = sr.ReadLine();
                var endPoint = (IPEndPoint)clientCon.RemoteEndPoint;

                var s = ReciveCalkBack(str,endPoint);
                //TODO Encoding
                clientCon.Send(Encoding.GetEncoding("UTF-8").GetBytes(s + "\n"));
            }
            catch(Exception e)
            {
                LogTools.ErrorLog(e.Message);
            }
        }

        public Dictionary<string,ICallback> CallbackList = new Dictionary<string,ICallback>();
        private string ReciveCalkBack(string r,IPEndPoint endPoint)
        {
            var keys = CallbackList.Keys;
            var ret = "";
            foreach(var key in keys){
                if (r==null || !r.StartsWith(key)) continue;
                var e = new SocketBackEventArgs { ReciveData = r, EndPoint = endPoint };
                ret = CallbackList[key].DealCommand(e);
            }
            return ret;
        }

    }
}
