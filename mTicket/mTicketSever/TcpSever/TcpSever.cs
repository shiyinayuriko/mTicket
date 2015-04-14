using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace mTicketSever
{
    class TcpSever
    {
        private int port;
        private Socket server;
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        public void StartListen()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.listenProcess), new object());
        }
        private void listenProcess(object state)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(IPAddress.Any, port));
            while (true)
            {
                server.Listen(100);
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.reciveProcess), server.Accept());
            }

        }
        private void reciveProcess(object p)
        {
            Socket client_Con = (Socket)p;
            try 
            {
                NetworkStream ns = new NetworkStream(client_Con);
                StreamReader sr = new StreamReader(ns, Encoding.Default);
                string str = sr.ReadLine();
                IPEndPoint EndPoint = (System.Net.IPEndPoint)client_Con.RemoteEndPoint;

                string s = reciveCalkBack(str,EndPoint);
                //TODO Encoding
                client_Con.Send(Encoding.GetEncoding("UTF-8").GetBytes(str+Environment.NewLine));
            }
            catch(Exception e)
            {
                //TODO
                throw e;
            }
        }

        public Dictionary<string,ICallback> callbackList = new Dictionary<string,ICallback>();
        private string reciveCalkBack(string r,IPEndPoint endPoint)
        {
            var keys = callbackList.Keys;
            string ret = "\0";
            foreach(var key in keys){
                if (r.StartsWith(key))
                {
                    SocketBackEventArgs e = new SocketBackEventArgs() { ReciveData = r, EndPoint = endPoint };
                    ret = callbackList[key].dealCommand(e);
                }
            }
            return ret;
        }

    }
}
