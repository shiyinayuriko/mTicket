package net.whitecomet.mticket.data;

import net.whitecomet.mticket.data.beans.SeverSettings;

import com.google.gson.Gson;

import android.content.Context;
import android.content.SharedPreferences;

public class TempStates {
	public static String ipAddress = null;
	public static Integer port = null;
	//TODO
	public static int updateCode = 0;
	public static SeverSettings severSettings = null;
	
	public static void load(Context context){
        SharedPreferences mSharedPreferences = context.getApplicationContext().getSharedPreferences("States", Context.MODE_PRIVATE);  
        ipAddress = mSharedPreferences.getString("ipAddress", null);
        int mport = mSharedPreferences.getInt("port", -1);
        port = (mport==-1?null:mport);
	}
	public static void save(Context context){
        SharedPreferences mSharedPreferences = context.getApplicationContext().getSharedPreferences("States", Context.MODE_PRIVATE);  
        SharedPreferences.Editor mEditor = mSharedPreferences.edit();  
        mEditor.putString("ipAddress", ipAddress);
        mEditor.putInt("port", port==null?-1:port);
        mEditor.commit();
	}
	
	public static void setSeverSettings(String settings){
		Gson gson = new Gson();
		severSettings = gson.fromJson(settings, SeverSettings.class);
	}
	public static void setHost(){
		//TODO
	}
}
