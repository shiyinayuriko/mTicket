using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using mTicket.Beans;
using mTickLibs.codeData;
using mTickLibs.IcCardAdapter;
using RawInput_dll;

namespace mTicketClient
{
    public partial class Scanner : Form
    {
        private readonly ServiceContainer _service;

        public Scanner(string ipAddr, int port)
        {
            InitializeComponent();
            _service = new ServiceContainer(ipAddr, port, this)
            {
                Invoke = OnUpdateState,
                FinishInitialize = OnFinishInitialize,
                FinishCheckin = OnFinishCheckin,
                FinishSync = OnFinishSync
            };

            Thread t = new Thread(_service.ConnectHost);
            t.Start();
        }

        private void OnUpdateState(string message)
        {
            label_client_state.Text = message;
        }


        private void OnFinishInitialize()
        {
            textBox_manually_input.Enabled = true;
            listView_checkin.Enabled = true;
            listView_info.Enabled = true;

            InitialListView();

            _service.StartSync();

            const bool captureOnlyInForeground = true;
            
            RawInput rawinput = new RawInput(Handle, captureOnlyInForeground);
//            rawinput.AddMessageFilter();
            Win32.DeviceAudit();
            rawinput.KeyPressed += OnKeyPressed;

            var loader = new LoaderManager();
            loader.DealwithCard = DealwithCard;
            loader.StartAutoLoad();
        }
        private byte[][] DealwithCard(AbstractIcCard card)
        {
            var ret = _service.CheckinIc(card.GetIdHex(), card.GetData());
            return ret;
        }

        private readonly Dictionary<string,KeyHandler> _handlers = new Dictionary<string, KeyHandler>(); 
        private void OnKeyPressed(object sender, RawInputEventArg e)
        {
            //TODO not sure Standard PS/2 Keyboard
            if (e.KeyPressEvent.Name.Contains("Standard PS/2 Keyboard")) return;

            if (textBox_manually_input.Focused) listView_checkin.Focus();
            if(!_handlers.ContainsKey(e.KeyPressEvent.DeviceName))
            {
                _handlers.Add(e.KeyPressEvent.DeviceName,new KeyHandler());
            }
            string ret = _handlers[e.KeyPressEvent.DeviceName].Pressed(e.KeyPressEvent);
            if (ret != null)
            {
                Console.WriteLine(ret);
                _service.Checkin(ret,e.KeyPressEvent.Name);
            }
        }

        private void OnFinishCheckin(bool isSuccess,string codes,string devices, CodeDataDetail codeData)
        {
            var record = new ListViewItem(codeData==null?"":codeData.id + "");
            record.SubItems.Add(codes);
            record.SubItems.Add(isSuccess + "");
            record.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd hh:mm;ss"));
            record.SubItems.Add(devices);
            listView_checkin.Items.Add(record);

            string str = "扫描结果:" + codes + Environment.NewLine;
            str += "扫描设备:" + devices + Environment.NewLine;
            str += isSuccess ? "通过" : "未通过" + Environment.NewLine;
            if (codeData != null)
            {
                foreach (var info in codeData.info)
                {
                    str += info.Key + ":" + info.Value + Environment.NewLine;
                }
                foreach (var checkinData in codeData.checkin)
                {
                    str += checkinData.checkin_time + ":" + checkinData.sync_from;
                }
            }
            text_scan_result.Text =str;

            lable_pass.BackColor = !isSuccess ? Color.Red : SystemColors.Window;
            lable_pass.Text = isSuccess ? "通过" : "未通过";
        }
        public void InitialListView()
        {
            CheckinData[] checkins = _service.GetCheckinDatas();
            foreach (var checkin in checkins)
            {
                var record = new ListViewItem(checkin.id + "");
                record.SubItems.Add("");
                record.SubItems.Add("");
                record.SubItems.Add(checkin.checkin_time);
                record.SubItems.Add(checkin.sync_from);
                listView_checkin.Items.Add(record);
            }
        }

        public void OnFinishSync(List<CheckinData> checkinDatas)
        {
            foreach (var checkin in checkinDatas)
            {
                var record = new ListViewItem(checkin.id + "");
                record.SubItems.Add("");
                record.SubItems.Add("");
                record.SubItems.Add(checkin.checkin_time);
                record.SubItems.Add(checkin.sync_from);
                listView_checkin.Items.Add(record);
            }
        }

        private void listView_checkin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_checkin.SelectedItems.Count == 0) return;

            listView_info.Items.Clear();
            string ids = listView_checkin.SelectedItems[0].SubItems[0].Text;
            int id;
            if(!Int32.TryParse(ids, out id)) return;

            CodeDataDetail codeTable = _service.LoadCodeDataDetail(id);
            foreach (var pair in codeTable.info)
            {
                var record = new ListViewItem(pair.Key);
                record.SubItems.Add(pair.Value);
                listView_info.Items.Add(record);
            }
            for (var i = 0; i < codeTable.checkin.Length; i++)
            {
                var record = new ListViewItem("进出记录-" + (i + 1));
                record.SubItems.Add(codeTable.checkin[i].checkin_time);
                listView_info.Items.Add(record);
            }
        }

        private void textBox_manually_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int) Keys.Enter)
            {
                _service.Checkin(textBox_manually_input.Text.Trim(), "keyboard");
                textBox_manually_input.Text = "";
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Environment.Exit(0);
        }
    }
}
