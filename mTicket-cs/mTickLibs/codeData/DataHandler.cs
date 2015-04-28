using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
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

        public static string CodeTableName = "code";
        public static string CodeInfoTableName = "code_info";
        public static string CodeInfoColumnTableName = "code_info_column";
        public static string CheckinTableName = "checkin";

        public static void SaveDatabse(CodeTable codeTable, string path)
        {
            SQLiteConnection.CreateFile(path);

            SQLiteConnection conn = new SQLiteConnection();
            conn.ConnectionString = new SQLiteConnectionStringBuilder {DataSource = path}.ToString();
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);
            //column table
            cmd.CommandText = "CREATE TABLE " + CodeInfoColumnTableName + " (column_index INTEGER,column_name TEXT );";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "insert into " + CodeInfoColumnTableName + " (column_index, column_name) values(@column_index, @column_name)";
            string[] columns = codeTable.columns;
            for (var i = 0; i < columns.Length; i++)
            {
                cmd.Parameters.AddRange(new[]
                {
                    new SQLiteParameter("@column_index", i),
                    new SQLiteParameter("@column_name", columns[i]),
                });
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = "CREATE TABLE " + CodeTableName + " (_id INTEGER PRIMARY KEY,code TEXT );";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "CREATE TABLE " + CodeInfoTableName + " (_id INTEGER PRIMARY KEY";
            for (int i = 0; i < columns.Length; i++) cmd.CommandText += ",arg" + i + " TEXT";
            cmd.CommandText += " );";
            cmd.ExecuteNonQuery();

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


            cmd.CommandText ="CREATE TABLE " + CheckinTableName + " (_id INTEGER,time TEXT );";
            cmd.ExecuteNonQuery();
            //TODO
        }

        public static CodeTable LoadDatabase(string path)
        {
            CodeTable table = new CodeTable();

            SQLiteConnection conn = new SQLiteConnection();
            conn.ConnectionString = new SQLiteConnectionStringBuilder { DataSource = path }.ToString();
            conn.Open();

            int columnNum;

            using(SQLiteCommand cmd = new SQLiteCommand(conn))
            {
                cmd.CommandText = "SELECT COUNT(*) FROM "+CodeInfoColumnTableName;
                columnNum = int.Parse(cmd.ExecuteScalar().ToString());
            }
            table.columns = new string[columnNum];
            using (SQLiteCommand cmd = new SQLiteCommand(conn))
            {
                cmd.CommandText = "SELECT * FROM " + CodeInfoColumnTableName;
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int index = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    table.columns[index] = name;
                }
            }

            using (SQLiteCommand cmd= new SQLiteCommand(conn))
            {
                List<CodeInfo> infos = new List<CodeInfo>();
                cmd.CommandText = string.Format("SELECT * FROM {0},{1} WHERE {0}._id = {1}._id", CodeTableName, CodeInfoTableName);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CodeInfo info = new CodeInfo();
                    info.info = new string[columnNum];
                    for(var i = 0;i<reader.FieldCount;i++)
                    {
                        string filedName = reader.GetName(i);
                        switch (filedName)
                        {
                            case "_id":
                                info.id = reader.GetInt32(i);break;
                            case "code":
                                info.code = reader.GetString(i);break;
                            default :
                                int argn = Convert.ToInt32(filedName.Substring(3));
                                info.info[argn] = reader.GetString(i);break;
                        }
                    }
                    infos.Add(info);
                }
                table.infos = infos.ToArray();
            }
            //TODO checkin
            return table;
        }
    }
}
