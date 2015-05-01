using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using mTickLibs.codeData;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace mTicket
{
    public class DataHandler
    {
        public static CodeTable LoadExcels(string fileName)
        {
            FileStream file = null;
            CodeTable table = null;
            try
            {
                file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                IWorkbook workbook;
                if (fileName.EndsWith(".xlsx")) workbook = new XSSFWorkbook(file);
                else workbook = new HSSFWorkbook(file);
                var sheet = workbook.GetSheetAt(0);

                table = new CodeTable();

                var headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;
                table.columns = new string[cellCount];
                for (int i = 0; i < cellCount; i++)
                {
                    table.columns[i] = headerRow.GetCell(i).ToString();
                }


                int rowCount = sheet.LastRowNum;
                table.infos = new CodeInfo[rowCount];
                for (int i = 1; i <= rowCount; i++)
                {
                    CodeInfo info = new CodeInfo();
                    var row = sheet.GetRow(i);
                    //info.id = Convert.ToInt32(row.GetCell(0).StringCellValue);
                    info.id = (int) row.GetCell(0).NumericCellValue;
                    info.code = row.GetCell(1).ToString();
                    info.info = new string[cellCount];
                    for (int j = 0; j < cellCount; j++)
                    {
                        info.info[j] = row.GetCell(j).ToString();
                    }
                    table.infos[i-1] = info;
                }
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                    file.Dispose();
                }
            }
            return table;
        }

        public static DataBaseHandler getDataBaseHandler(string path)
        {
            return new DataBaseHandler(path);
        }
    }

    public class DataBaseHandler
    {
        public static string CodeTableName = "code";
        public static string CodeInfoTableName = "code_info";
        public static string CodeInfoColumnTableName = "code_info_column";
        public static string CheckinTableName = "checkin";

        private readonly SQLiteConnection _conn;
        public DataBaseHandler(string path)
        {
            _conn = new SQLiteConnection();
            _conn.ConnectionString = new SQLiteConnectionStringBuilder { DataSource = path }.ToString();
            _conn.Open();
        }


        public CodeTable LoadCodeTable()
        {
            CodeTable table = new CodeTable();

            int columnNum;
            using (SQLiteCommand cmd = new SQLiteCommand(_conn))
            {
                cmd.CommandText = "SELECT COUNT(*) FROM " + CodeInfoColumnTableName;
                columnNum = Int32.Parse(cmd.ExecuteScalar().ToString());
            }
            table.columns = new string[columnNum];
            using (SQLiteCommand cmd = new SQLiteCommand(_conn))
            {
                cmd.CommandText = "SELECT * FROM " + CodeInfoColumnTableName;
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int index = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    table.columns[index] = name;
                }
                reader.Close();
            }

            using (SQLiteCommand cmd = new SQLiteCommand(_conn))
            {
                List<CodeInfo> infos = new List<CodeInfo>();
                cmd.CommandText = String.Format("SELECT * FROM {0},{1} WHERE {0}._id = {1}._id", CodeTableName, CodeInfoTableName);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CodeInfo info = new CodeInfo();
                    info.info = new string[columnNum];
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        string filedName = reader.GetName(i);
                        switch (filedName)
                        {
                            case "_id":
                                info.id = reader.GetInt32(i); break;
                            case "code":
                                info.code = reader.GetString(i); break;
                            default:
                                //"arg" = 3
                                int argn = Convert.ToInt32(filedName.Substring(3));
                                info.info[argn] = reader.GetString(i); break;
                        }
                    }
                    infos.Add(info);
                }
                reader.Close();
                table.infos = infos.ToArray();
            }
            return table;
        }
        public static void SaveCodeTable(CodeTable codeTable, string path)
        {
            SQLiteConnection.CreateFile(path);

            SQLiteConnection conn = new SQLiteConnection();
            conn.ConnectionString = new SQLiteConnectionStringBuilder { DataSource = path }.ToString();
            conn.Open();


            using (SQLiteCommand cmd = new SQLiteCommand(conn))
            {
                cmd.CommandText = "CREATE TABLE " + CodeInfoColumnTableName + " (column_index INTEGER,column_name TEXT );";
                cmd.ExecuteNonQuery();
            }
            string[] columns;
            using (SQLiteCommand cmd = new SQLiteCommand(conn))
            {
                cmd.CommandText = "insert into " + CodeInfoColumnTableName + " (column_index, column_name) values(@column_index, @column_name)";
                columns = codeTable.columns;
                for (var i = 0; i < columns.Length; i++)
                {
                    cmd.Parameters.AddRange(new[]
                {
                    new SQLiteParameter("@column_index", i),
                    new SQLiteParameter("@column_name", columns[i]),
                });
                    cmd.ExecuteNonQuery();
                }
            }
            using (SQLiteCommand cmd = new SQLiteCommand(conn))
            {
                cmd.CommandText = "CREATE TABLE " + CodeTableName + " (_id INTEGER PRIMARY KEY,code TEXT );";
                cmd.ExecuteNonQuery();
            }

            using (SQLiteCommand cmd = new SQLiteCommand(conn))
            {
                cmd.CommandText = "CREATE TABLE " + CodeInfoTableName + " (_id INTEGER PRIMARY KEY";
                for (int i = 0; i < columns.Length; i++) cmd.CommandText += ",arg" + i + " TEXT";
                cmd.CommandText += " );";
                cmd.ExecuteNonQuery();
            }

            using (SQLiteCommand cmd = new SQLiteCommand(conn))
            {
                var transaction = conn.BeginTransaction();
                cmd.CommandText = "replace into " + CodeTableName + " (_id, code) values(@_id, @code)";
                foreach (var info in codeTable.infos)
                {
                    cmd.Parameters.AddRange(new[]
                {
                    new SQLiteParameter("@_id", info.id),
                    new SQLiteParameter("@code", info.code),
                });
                    cmd.ExecuteNonQuery();
                }
                cmd.CommandText = "replace into " + CodeInfoTableName + " (_id";
                for (int i = 0; i < columns.Length; i++) cmd.CommandText += ",arg" + i;
                cmd.CommandText += ") values(@_id";
                for (int i = 0; i < columns.Length; i++) cmd.CommandText += ",@arg" + i;
                cmd.CommandText += " );";
                foreach (var info in codeTable.infos)
                {
                    cmd.Parameters.Add(new SQLiteParameter("@_id", info.id));
                    for (int i = 0; i < columns.Length; i++)
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@arg" + i, info.info[i]));
                    }
                    cmd.ExecuteNonQuery();
                }
                transaction.Commit();
            }

            using (SQLiteCommand cmd = new SQLiteCommand(conn))
            {
                cmd.CommandText = "CREATE TABLE " + CheckinTableName + " (_id INTEGER, checkin_time TIME, sync_time timestamp );";
                cmd.ExecuteNonQuery();  
            }
            conn.Close();
        }
      
        public CheckinData[] GetCheckinDatas(long fromTimestamp)
        {
            List<CheckinData> ret = new List<CheckinData>();
            using (SQLiteCommand cmd = new SQLiteCommand(_conn))
            {
                cmd.CommandText = "SELECT * FROM " + CheckinTableName + " WHERE sync_time>=" + fromTimestamp;
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CheckinData checkinData = new CheckinData();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        string filedName = reader.GetName(i);
                        switch (filedName)
                        {
                            case "_id": checkinData.id = reader.GetInt32(i); break;
                            case "checkin_time":
                                checkinData.checkin_time = reader.GetString(i);break;
                            case "sync_time": checkinData.sync_time = reader.GetInt64(i);break;
                        }
                    }
                    ret.Add(checkinData);
                }
                reader.Close();
            }
            return ret.ToArray();
        }
        public long SetCheckinDatas(CheckinData[] checkinDatas, bool hasOwnTime = false)
        {
            long time = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);

            using (SQLiteCommand cmd = new SQLiteCommand(_conn))
            {
                var transaction = _conn.BeginTransaction();
                cmd.CommandText = "insert into " + CheckinTableName + " (_id, checkin_time,sync_time) values(@_id,@checkin_time, @sync_time)";
                foreach (var checkinData in checkinDatas)
                {
                    cmd.Parameters.AddRange(new[]
                    {
                        new SQLiteParameter("@_id", checkinData.id),
                        new SQLiteParameter("@checkin_time", checkinData.checkin_time),
                        new SQLiteParameter("@sync_time",hasOwnTime?checkinData.sync_time:time),
                    });
                    cmd.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            return time;
        }
    }
}
