using System;
using System.Windows.Forms;

namespace mTicket
{
    public partial class Importer : Form
    {
        CodeTable _dataTable = null;
        public Importer()
        {
            InitializeComponent();
        }

        private void buttonExcute_Click(object sender, System.EventArgs e)
        {
            if (saveDb.ShowDialog() == DialogResult.OK)
            {
                var fName = saveDb.FileName;
                DataBaseHandler.SaveCodeTable(_dataTable, fName);
            }
        }

        private void Importer_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            var fileName = path.Substring(path.LastIndexOf('\\') + 1);

            try
            {
                if (path.EndsWith(".xls") || path.EndsWith(".xlsx"))
                {
                    var dataTable = DataHandler.LoadExcels(path);
                    buttonExcute.Enabled = true;
                    //TODO 添加
                    _dataTable = dataTable;
                    infoLabel.Text = dataTable.infos.Length + "/" + 0;
                }
                else if (path.EndsWith(".db"))
                {
                    //Data.Instance().LoadData(path);
                }
                else
                {
                    MessageBox.Show("导入文件格式错误");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Importer_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None;
        }
    }
}
