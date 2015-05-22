using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using mTicket;
using mTicket.Beans;
using mTicketClient.Properties;
using mTickLibs.codeData;
using Microsoft.Win32;
using Newtonsoft.Json;
using Settings = mTickLibs.Beans.Settings;
using Timer = System.Timers.Timer;

namespace mTicketClient
{
    class ServiceContainer
    {
        private readonly TcpClient _tcp;
        private Settings _settings;

        private readonly Form _form;
        public delegate void OnUpdateState(string message);
        public OnUpdateState Invoke;

        private DataBaseHandler _db;

        private LogicChecker _logicChecker;
        public ServiceContainer(string ipAddr, int port, Form form)
        {
            _tcp = new TcpClient(ipAddr,port);
            _form = form;

            _timer = new Timer();
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Elapsed += SyncCheckinEvent;
            _timer.Interval = 300000;
        }

        public delegate void OnFinishInitialize();
        public OnFinishInitialize FinishInitialize;
        public void ConnectHost()
        {
            try
            {
                _tcp.Connect();
                String json = _tcp.Call("connect");
                _settings = JsonConvert.DeserializeObject<Settings>(json.Trim());
                _tcp.TimeOut = _settings.tcp_timeout;
                _logicChecker = new LogicChecker(_settings.checkin_logic);

                //TODO 记录历史服务器地址
                UpdateStete("连接服务器成功");
            }
            catch (Exception e)
            {
                MessageBox.Show(Resources.ServiceContainer_Error_occurd + e.StackTrace);
                Environment.Exit(0);
            }
            finally
            {
                _tcp.DisConnect();
            }


            try
            {
                string path = ConfigurationManager.AppSettings["databasePath"];

                var result = MessageBox.Show(Resources.ServiceContainer_ConnectHost_AskForUpdateDatabase, "", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    _tcp.Connect();
                    UpdateStete("正在下载数据");
                    string json = _tcp.Call("codeTable");
                    UpdateStete("开始解析数据");
                    CodeTable table = JsonConvert.DeserializeObject<CodeTable>(json.Trim());
                    UpdateStete("导入数据中");
                    DataBaseHandler.SaveCodeTable(table, path);
                    UpdateStete("导入数据完成");
                }
                _db = DataHandler.getDataBaseHandler(path);
            }
            catch (Exception e)
            {
                MessageBox.Show(Resources.ServiceContainer_Error_occurd + e.StackTrace);
                Environment.Exit(0);
            }
            finally
            {
                _tcp.DisConnect();
            }

            if (_form.InvokeRequired)
            {
                _form.Invoke(new OnFinishInitialize(FinishInitialize));
            }
            else
            {
                FinishInitialize();
            }

            UpdateStete("就绪");
        }

        private readonly Timer _timer ;
        public void StartSync()
        {
            new Thread(DelaySyncCheckin).Start();

            _timer.Stop();
            _timer.Interval = _settings.timer;
            _timer.Start();
        }
        public void StopSync()
        {
            _timer.Stop();
        }

        private void DelaySyncCheckin()
        {
            Thread.Sleep(5000);
            SyncCheckin();
        }
        private void SyncCheckinEvent(object sender,  ElapsedEventArgs e)
        {
            SyncCheckin();
        }

        private bool _isSyncing;
        private void SyncCheckin()
        {
            if (_isSyncing) return;
            _isSyncing = true;


            //TODO


            _isSyncing = false;
        }


        public delegate void CheckinSuccess(bool isSuccess, CodeDataDetail codeData);
        public void Checkin(string code, string device, CheckinSuccess checkinSuccess)
        {
            try
            {
                CodeDataDetail data = _db.LoadCodeDataDetail(code);
                //TODO updateResult

                bool isPass = _logicChecker.Checkin(data);
                if (isPass) _db.Checkin(data.id,device);


                if (_form.InvokeRequired)
                    _form.Invoke(new CheckinSuccess(checkinSuccess), isPass, data);
                else
                    checkinSuccess(isPass, data);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace);
            }

        }






        private void UpdateStete(string message)
        {
            if (_form.InvokeRequired)
            {
                _form.Invoke(new OnUpdateState(Invoke), message);
            }
            else
            {
                Invoke(message);
            }
        }
    }
}
