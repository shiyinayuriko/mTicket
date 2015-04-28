package net.whitecomet.mticket.data;

import net.whitecomet.mticket.data.beans.CodeInfo;
import net.whitecomet.mticket.data.beans.CodeTabel;
import android.content.ContentValues;
import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.database.sqlite.SQLiteStatement;
import android.os.Handler;
import android.os.Message;

public class Database extends SQLiteOpenHelper {
	public static final String dataBaseName = "coding.db";
	public static final int dbVersion = 1;

	public static final String codeTableName = "code";
	public static final String codeInfoTableName = "code_info";
	public static final String codeInfoColumnTableName = "code_info_column";
	public static final String checkinTableName = "checkin";

	public Database(Context context) {
		super(context, dataBaseName, null, dbVersion);
	}

	@Override
	public void onCreate(SQLiteDatabase db) {
	}
	
	public void initializeCodeTable(CodeTabel table,Handler handler) {
		SQLiteDatabase db = getWritableDatabase();
		db.execSQL("DROP TABLE IF EXISTS " + codeTableName);
		db.execSQL("DROP TABLE IF EXISTS " + codeInfoTableName);
		db.execSQL("DROP TABLE IF EXISTS " + codeInfoColumnTableName);
		db.execSQL("DROP TABLE IF EXISTS " + checkinTableName);

		String sql;

		sql = "CREATE TABLE "
				+ codeInfoColumnTableName
				+ " (_ID INTEGER PRIMARY KEY,column_index INTEGER,column_name TEXT );";
		db.execSQL(sql);
		String[] columns = table.columns;
		for (int i = 0; i < columns.length; i++) {
			ContentValues values = new ContentValues();
			values.put("column_index", i);
			values.put("column_name", columns[i]);
			db.insert(codeInfoColumnTableName, null, values);
		}
		ContentValues values = new ContentValues();
		values.put("column_index", -1);
		values.put("column_name", columns.length);
		db.insert(codeInfoColumnTableName, null, values);

		sql = "CREATE TABLE " + codeTableName + " (_ID INTEGER PRIMARY KEY,id INTEGER,code TEXT );";
		db.execSQL(sql);
		sql = "CREATE TABLE " + codeInfoTableName + " (_ID INTEGER PRIMARY KEY,id INTEGER";
		for (int i = 0; i < columns.length; i++) sql += ",arg" + i + " TEXT";
		sql += " );";
		db.execSQL(sql);
		
		sql = "insert into "+codeTableName+"(id,code) values(?,?)";
		String sql2 = "insert into "+codeInfoTableName+"(id";
		for(int i=0;i<columns.length;i++) sql2 += ",arg" +i;
		sql2 += ") values(?";
		for(int i=0;i<columns.length;i++) sql2 += ",?";
		sql2 += ")";
		

		int index = 0;

		SQLiteStatement stat1 = db.compileStatement(sql);
		SQLiteStatement stat2 = db.compileStatement(sql2);
		db.beginTransaction();
		for(CodeInfo info :table.infos){
			index++;
			if(handler!=null && index%TempStates.severSettings.proress_step_update_database==0){
				Message msg = new Message();
				msg.what = -4;
				msg.arg1 = index;		
				msg.arg2 = table.infos.length;
				handler.sendMessage(msg);
			}
			
			stat1.bindLong(1, info.id);
			stat1.bindString(2, info.code);

			stat2.bindLong(1, info.id);
			for(int i=0;i<info.info.length;i++){
				stat2.bindString(2+i, info.info[i]);

			}
			stat1.execute();
			stat2.execute();
		}
		db.setTransactionSuccessful();
		db.endTransaction();

		sql = "CREATE TABLE " + checkinTableName + " (_ID INTEGER PRIMARY KEY,id INTEGER,time TEXT );";
		db.execSQL(sql);
		db.close();
	}

	@Override
	public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
		// TODO Auto-generated method stub
	}

}
