package net.whitecomet.mticket.data;

import net.whitecomet.mticket.data.beans.CodeTabel;
import android.content.ContentValues;
import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.database.sqlite.SQLiteStatement;

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

	public void initializeCodeTable(CodeTabel table) {
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

		sql = "CREATE TABLE " + checkinTableName + " (_ID INTEGER PRIMARY KEY,id INTEGER,time TEXT );";
		db.execSQL(sql);
		
//        SQLiteStatement stat = db.compileStatement(sql);  

	}

	@Override
	public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
		// TODO Auto-generated method stub
	}

}
