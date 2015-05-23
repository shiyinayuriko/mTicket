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
using mTicketClient.Modules;
using mTicketClient.Properties;
using mTickLibs.codeData;
using mTickLibs.IcCardAdapter;
using mTickLibs.Tools;
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
        public delegate void OnFinishCheckin(bool isSuccess,string code, string device,CodeDataDetail codeData);
        public OnFinishCheckin FinishCheckin;

        public delegate void OnFinishSync(List<CheckinData> checkinDatas);
        public OnFinishSync FinishSync;

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
                    LocalSettings.SaveSyncTimestamp(0);
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
            var checkin = _db.MarkUnsynced();

            try
            {
                _tcp.Connect();
                UpdateStete("链接服务器");
                string ret = _tcp.Call("syncCheckin", new[]
                {
                    LocalSettings.GetSyncTimetamp() + "",
                    JsonConvert.SerializeObject(checkin), 
                    LocalSettings.GetScanLog()
                });
                UpdateStete("传输数据完成");

                long newTimestamp = Convert.ToInt64(ret.Substring(0, ret.IndexOf(' ')));
                String json = ret.Substring(ret.IndexOf(' ') + 1);
                var retCheckin = JsonConvert.DeserializeObject<CheckinData[]>(json);
                UpdateStete("解析数据完成");
                _db.AddSyncedCheckinData(retCheckin);
                _db.SetMarksSynced(newTimestamp);

                UpdateStete("同步数据完成");

                LocalSettings.SaveSyncTimestamp(newTimestamp);
                LocalSettings.ClearScanLog();
                
                if (_form.InvokeRequired)
                {
                    _form.Invoke(new OnFinishSync(FinishSync), retCheckin.ToList());
                }
                else
                {
                    FinishSync(retCheckin.ToList());
                }
            }
            catch (Exception e)
            {
                UpdateStete("同步发生异常");
            }
            finally
            {
                _tcp.DisConnect();
            }
            _isSyncing = false;
        }


        public void Checkin(string code, string device)
        {
            LocalSettings.AppendScanLog(device + ":" + code);
            try
            {
                CodeDataDetail data = _db.LoadCodeDataDetail(code);

                bool isPass = _logicChecker.Checkin(data);
                if (isPass) _db.Checkin(data.id,device);


                if (_form.InvokeRequired)
                    _form.Invoke(new OnFinishCheckin(FinishCheckin), isPass, code, data);
                else
                    FinishCheckin(isPass,code, device,data);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace);
            }

        }

        public byte[][] CheckinIc(string code, byte[][] bytes)
        {
            IcCardStruct icCard = IcCardBean.FromByte(IcCardBean.GetBytesLine(bytes));
           
            LocalSettings.AppendScanLog(String.Format("IC:{0} {1} {2}", code, icCard.CanIn?"in":"out",icCard.LastTime));


            CodeDataDetail data = _db.LoadCodeDataDetail(code);
            bool isPass = _logicChecker.Checkin(data, icCard);
            if (isPass)
            {
                _db.Checkin(data.id, "IC" + (icCard.CanIn ? "in" : "out"));
             
                icCard.Id = data.id;
                icCard.LastTime = TimeTools.CurrentTimeMillis();
                icCard.CanIn = !icCard.CanIn;
            }
            byte[][] ret = IcCardBean.CopyToBytes(IcCardBean.ToBytes(icCard), bytes);
            return ret;
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

        public CheckinData[] GetCheckinDatas()
        {
            return _db.GetAllCheckinDatas();
        }

        internal CodeDataDetail LoadCodeDataDetail(int id)
        {
            return _db.LoadCodeDataDetail(id);
        }
    }
}
