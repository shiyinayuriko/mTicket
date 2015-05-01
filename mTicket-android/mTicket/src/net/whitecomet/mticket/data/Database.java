package net.whitecomet.mticket.data;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import net.whitecomet.mticket.data.beans.CheckinData;
import net.whitecomet.mticket.data.beans.CodeInfo;
import net.whitecomet.mticket.data.beans.CodeTable;
import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.database.sqlite.SQLiteStatement;
import android.os.Handler;
import android.os.Message;
import android.provider.SyncStateContract.Columns;

public class Database extends SQLiteOpenHelper {
	public static final String dataBaseName = "coding.db";
	public static final int dbVersion = 1;

	public static final String codeTableName = "code";
	public static final String codeInfoTableName = "code_info";
	public static final String codeInfoColumnTableName = "code_info_column";
	public static final String checkinTableName = "checkin";

	private static Database instance = null;
	
	private SQLiteDatabase db;
	public static Database getInstance(Context context){
		if(instance==null) instance = new Database(context.getApplicationContext());
		return instance;
	}
	private Context context;
	private Database(Context context) {
		super(context, dataBaseName, null, dbVersion);
		db = getWritableDatabase();
	}

	@Override
	public void onCreate(SQLiteDatabase db) {
		// TODO Auto-generated method stub
	}
	
	public void initializeCodeTable(CodeTable table,Handler handler) {
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
			if(handler!=null && index%TempStates.instance(context).severSettings.proress_step_update_database==0){
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

		sql = "CREATE TABLE " + checkinTableName + " (_ID INTEGER PRIMARY KEY,id INTEGER,checkin_time TIME,sync_time timestamp);";
		db.execSQL(sql);
		
		columns = null;
	}

	@Override
	public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
		// TODO Auto-generated method stub
	}

	public void checkin(int id){
		String sql = "insert into " + checkinTableName + " (id, checkin_time) values("+id+", datetime('now'))";
		db.execSQL(sql);
	}
	
	public CheckinData[] markUnsynced(){
		ArrayList<CheckinData> list = new ArrayList<CheckinData>();
		
		ContentValues values = new ContentValues();
		values.put("sync_time", "-");
		db.update(checkinTableName, values, "sync_time is NULL", null);
		
		Cursor c = db.query(checkinTableName, new String[]{"id","checkin_time"}, "sync_time=?", new String[]{"-"}, null, null, null);
		while(c.moveToNext()){
			CheckinData checkinData = new CheckinData();
			checkinData.id = c.getInt(c.getColumnIndex("id"));
			checkinData.checkin_time = c.getString(c.getColumnIndex("checkin_time"));
			list.add(checkinData);
		}
		c.close();
		return list.toArray(new CheckinData[0]);
	}
	public void setMarksSynced(long timestamp){
		ContentValues values = new ContentValues();
		values.put("sync_time", timestamp);
		db.update(checkinTableName, values, "sync_time =?", new String[]{"-"});
	}
	public void addSyncedCheckinData(CheckinData[] checkinDatas){
		String sql = "insert into "+checkinTableName+"(id,checkin_time,sync_time) values(?,?,?)";
		SQLiteStatement stat = db.compileStatement(sql);
		db.beginTransaction();
		
		for(CheckinData checkin:checkinDatas){
			stat.bindLong(1, checkin.id);
			stat.bindString(2, checkin.checkin_time);
			stat.bindLong(3, checkin.sync_time);
			stat.execute();
		}
		db.setTransactionSuccessful();
		db.endTransaction();
	}

	private String[] columns = null;
	public CodeDataReturn getCodeInfo(String code){
		if(columns == null){
			Cursor c = db.query(codeInfoColumnTableName,null,null,null,null,null,null);
			String[] columnsTmp = new String[c.getCount()];
			while(c.moveToNext()){
				columnsTmp[c.getInt(c.getColumnIndex("column_index"))] =  c.getString(c.getColumnIndex("column_name"));
			}
			c.close();
			columns = columnsTmp;
		}

		Cursor ci = db.query(codeTableName,null,"code=?",new String[]{code},null,null,null,1+"");
		if(ci.moveToFirst()){
			CodeDataReturn ret = new CodeDataReturn();
			ret.id = ci.getInt(ci.getColumnIndex("id"));
			ret.code = ci.getString(ci.getColumnIndex("code"));
			ci.close();
			
			Cursor cin = db.query(codeInfoTableName, null, "id = ?", new String[]{ret.id+""}, null, null, null,1+"");
			ret.info = new HashMap<String, String>();
			cin.moveToFirst();
			for(int i=0;i<columns.length;i++){
				String value = cin.getString(cin.getColumnIndex("arg"+i));
				ret.info.put(columns[i], value);
			}
			cin.close();
			
			Cursor cck = db.query(checkinTableName, null, "id = ?", new String[]{ret.id+""}, null, null, null);
			ret.checkin = new ArrayList<CheckinData>();
			while(cck.moveToNext()){
				CheckinData checkin = new CheckinData();
				checkin.id = cck.getInt(cck.getColumnIndex("id"));
				checkin.checkin_time = cck.getString(cck.getColumnIndex("checkin_time"));
				ret.checkin.add(checkin);
			}
			cck.close();
			
			return ret;
		}else {
			ci.close();
			return null;
		}
	}
	
	public static class CodeDataReturn{
		public Map<String,String> info;
		public List<CheckinData> checkin;
		public int id;
		public String code;
	}
}
