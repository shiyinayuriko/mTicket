using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using mTicket.Beans;
using mTicket.Properties;
using mTickLibs.codeData;
using mTickLibs.Tools;

namespace mTicket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            label_IpAddress_Content.Text = IpTools.GetIp() ?? "未找到IP地址";
        }
        private void Tab_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None;
        }



        private void button_Database_Browse_Click(object sender, EventArgs e)
        {
            if (openDbFileDialog.ShowDialog() == DialogResult.OK)
            {
                var dbFileName = openDbFileDialog.FileName;
                UpdateFilename(dbFileName);
            }
        }
        private void Tab_main_DragDrop(object sender, DragEventArgs e)
        {
            var dbFileName = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            try
            {
                UpdateFilename(dbFileName);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }
        private void UpdateFilename(string dbFileName)
        {
            _db = DataHandler.getDataBaseHandler(dbFileName);
            label_Database_Content.Text = dbFileName;
            button_startListen.Enabled = true;
        }


        private DataBaseHandler _db;
        private void button_startListen_onClick(object sender, EventArgs e)
        {
            var t = new TcpSever { Port = Convert.ToInt32(Text_Port_Content.Text) };

            t.CallbackList.Add("ping", new PingCallback(text_log));
            t.CallbackList.Add("connect", new ConnectCallback(text_log));
            t.CallbackList.Add("codeTable", new CodeTableCallback(text_log, _db));
            t.CallbackList.Add("syncCheckin", new SyncCallback(text_log, listView_checkin, _db));
            t.StartListen();
            button_startListen.Enabled = false;
            button_Database_Browse.Enabled = false;
            Text_Port_Content.Enabled = false;
        }

        private void Text_Port_Content_TextChanged(object sender, EventArgs e)
        {
            string newPort = Text_Port_Content.Text.ToCharArray().Where(char.IsNumber).Aggregate("", (current, ch) => current + ch);
            Text_Port_Content.Text = newPort;
        }

        private void listView_checkin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_checkin.SelectedItems.Count == 0) return;
            //TODO do what?
            var id0 = Convert.ToInt32(listView_checkin.SelectedItems[0].SubItems[0].Text);

            CodeDataDetail codeTable = _db.LoadCodeDataDetail(id0);
            listView_info.Items.Clear();
            foreach (var pair in codeTable.info)
            {
                var record = new ListViewItem(pair.Key);
                record.SubItems.Add(pair.Value);
                listView_info.Items.Add(record);
            }
            for (var i=0;i<codeTable.checkin.Length;i++)
            {
                var record = new ListViewItem("进出记录-"+(i+1));
                record.SubItems.Add(codeTable.checkin[i].checkin_time);
                listView_info.Items.Add(record);
            }
        }

        private CodeTable _importDataTable = null;
        private void Tab_import_DragDrop(object sender, DragEventArgs e)
        {
            var path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            ImportData(path);
        }

        private void button_import_Click(object sender, EventArgs e)
        {
            if (openImportFileDialog.ShowDialog() == DialogResult.OK)
            {
                var path = openImportFileDialog.FileName;
                ImportData(path);
            }
        }
        private void ImportData(string path)
        {
            try
            {
                if (path.EndsWith(".xls") || path.EndsWith(".xlsx")||path.EndsWith(".db"))
                {
                    CodeTable dataTable = null;

                    if(path.EndsWith(".xls") || path.EndsWith(".xlsx")) dataTable = DataHandler.LoadExcels(path);
                    else using (var database = DataHandler.getDataBaseHandler(path))
                    {
                        dataTable = database.LoadCodeTable();
                    }
                   
                    var newTable = DataHandler.CombineCodeTable(_importDataTable, dataTable);

                    if (newTable == null)
                    {
                        MessageBox.Show(Resources.Form1_ImportData_DataSauce_not_match);
                    }
                    else
                    {
                        var oldLength = 0;
                        if (_importDataTable != null) oldLength = _importDataTable.infos.Length;
                        var newLength = newTable.infos.Length;
                        var additionLength = 0;
                        if (dataTable != null) additionLength = dataTable.infos.Length;

                        string str = String.Format(Resources.Form1_ImportData_ReportMessage, newLength - oldLength, newLength, oldLength + additionLength - newLength);

                        _importDataTable = newTable;
                        button_export_database.Enabled = true;
                        label_data_number_content.Text = _importDataTable.infos.Length.ToString();

                        MessageBox.Show(str);
                        UpdateEmptyCodeList();
                    }
                }
                else
                {
                    MessageBox.Show(Resources.Form1_Tab_import_DragDrop_error_filename_extension);
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void button_export_database_Click(object sender, EventArgs e)
        {
            if (saveDb.ShowDialog() == DialogResult.OK)
            {
                var fName = saveDb.FileName;
                DataBaseHandler.SaveCodeTable(_importDataTable, fName);
            }
        }

        private readonly Dictionary<int,CodeInfo> _emptyTable = new Dictionary<int, CodeInfo>();
        private void UpdateEmptyCodeList()
        {
            _emptyTable.Clear();
            foreach (var info in _importDataTable.infos)
            {
                if(!info.code.Trim().Equals("")) continue;;
                if(!_emptyTable.ContainsKey(info.id))
                    _emptyTable.Add(info.id,info);
            }

            listView_empty.Columns.Clear();
            for(var i=0;i<_importDataTable.columns.Length;i++)
            {
                listView_empty.Columns.Add(_importDataTable.columns[i]);
            }

            listView_empty.Items.Clear();
            foreach (var info in _emptyTable.Values)
            {

                var record = new ListViewItem(info.info[0]);
                for (int i = 1; i < info.info.Length; i++)
                {
                    record.SubItems.Add(info.info[i]);
                }
                listView_empty.Items.Add(record);
            }
        }
    
    }
}
