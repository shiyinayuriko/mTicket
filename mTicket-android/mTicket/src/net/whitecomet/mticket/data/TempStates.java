package net.whitecomet.mticket.data;

import net.whitecomet.mticket.data.beans.SeverSettings;

import com.google.gson.Gson;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;

public class TempStates {
	private static TempStates instance = null;
	public static TempStates instance(Context context){
		if(instance==null) instance = new TempStates(context);
		return instance;
	}
	private TempStates(Context context){
		mSharedPreferences = context.getApplicationContext().getSharedPreferences("States", Context.MODE_PRIVATE);
        ipAddress = mSharedPreferences.getString("ipAddress", null);
        int mport = mSharedPreferences.getInt("port", -1);
        port = (mport==-1?null:mport);
        syncTimetamp = mSharedPreferences.getInt("syncTimetamp", 0);
        databaseUpdateTime = mSharedPreferences.getInt("databaseUpdateTime", 0);
	}
	private SharedPreferences mSharedPreferences;
	
	private String ipAddress = null;
	private Integer port = null;
	public String getIpaddress(){
		return ipAddress;
	}
	public Integer getPort(){
		return port;
	}
	public void setHost(String ipAddress,Integer port){
		this.ipAddress = ipAddress;
		this.port = port;
        Editor mEditor = mSharedPreferences.edit();  
        
        mEditor.putString("ipAddress", ipAddress);
        
        if(port==null)
            mEditor.remove("port");
        else
        	mEditor.putInt("port",port);
       
        mEditor.commit();
	}
	
	private long syncTimetamp = 0;
	public long getSyncTimetamp() {
		return syncTimetamp;
	}
	public void setSyncTimetamp(long syncTimetamp) {
		this.syncTimetamp = syncTimetamp;
        Editor mEditor = mSharedPreferences.edit();  
    	mEditor.putLong("syncTimetamp",syncTimetamp);
        mEditor.commit();
	}
	
	private long databaseUpdateTime = 0;
	public long getDatabaseUpdateTime() {
		return databaseUpdateTime;
	}
	public void setDatabaseUpdateTime(long databaseUpdateTime) {
		this.databaseUpdateTime = databaseUpdateTime;
        Editor mEditor = mSharedPreferences.edit();  
    	mEditor.putLong("databaseUpdateTime",databaseUpdateTime);
        mEditor.commit();
	}
	

	
	
	public SeverSettings severSettings = null;
	public void setSeverSettings(String settings){
		Gson gson = new Gson();
		severSettings = gson.fromJson(settings, SeverSettings.class);
	}

}
