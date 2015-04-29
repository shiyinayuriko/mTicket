package net.whitecomet.mticket;

import java.util.Date;
import java.util.Timer;
import java.util.TimerTask;

import com.google.gson.Gson;

import net.whitecomet.mticket.data.Database;
import net.whitecomet.mticket.data.TempStates;
import net.whitecomet.mticket.data.beans.CheckinData;
import net.whitecomet.mticket.data.beans.CodeTabel;
import net.whitecomet.mticket.data.beans.SeverSettings;
import net.whitecomet.mticket.tcpClient.NoInputStringException;
import net.whitecomet.mticket.tcpClient.NotWifiException;
import net.whitecomet.mticket.tcpClient.SocketConnectException;
import net.whitecomet.mticket.tcpClient.TCPClient;
import android.app.Service;
import android.content.Intent;
import android.os.Binder;
import android.os.Bundle;
import android.os.Handler;
import android.os.IBinder;
import android.os.Message;
import android.util.Log;

public class ConnectionService extends Service {
	private TCPClient tcp;
	@Override
	public void onCreate() {
		super.onCreate();
		tcp = new TCPClient(this.getApplicationContext());
	}
	
	private ConnectionServiceBinder binder = new ConnectionServiceBinder();
	@Override
	public IBinder onBind(Intent arg0) {
		return binder;
	}

	private Timer mTimer = new Timer();

	@Override
	public int onStartCommand(Intent intent, int flags, int startId) {
		super.onStartCommand(intent, flags, startId);
		
		mTimer.cancel();
		mTimer = new Timer();
		mTimer.schedule(new MyTimerTask(), 0 , TempStates.instance(this).severSettings.timer);
		
		return START_REDELIVER_INTENT;
	}	
	
	@Override
	public void onDestroy() {
		mTimer.cancel();
		super.onDestroy();
	}
	
	public void syncCheckin(){
		try{
			tcp.connect();
			Gson gson = new Gson();
			
			long timestamp = TempStates.instance(this).getSyncTimetamp();
			CheckinData[] checkin = Database.getInstance(ConnectionService.this).markUnsynced();
			
			String ret = tcp.call("syncCheckin "+ timestamp + " " + gson.toJson(checkin));
			
			long newTimestamp = Long.parseLong(ret.substring(0,ret.indexOf(' ')));
			String json = ret.substring(ret.indexOf(' ')+1);
			
			CheckinData[] retCheckin = gson.fromJson(json, CheckinData[].class); 
			
			Database.getInstance(ConnectionService.this).addSyncedCheckinData(retCheckin);
			Database.getInstance(ConnectionService.this).setMarksSynced(newTimestamp);
			
			TempStates.instance(this).setSyncTimetamp(newTimestamp);
		}catch(SocketConnectException | NoInputStringException e){
			e.printStackTrace();
		}finally{
			tcp.disConnect();
		}
	}
	
	public class ConnectionServiceBinder extends Binder{
    	public void searchHost(final int port,Handler handler){
    		final Handler myhandler= (handler==null?new Handler():handler);
    		new Thread(){
    			@Override
    			public void run() {
	    			try {
	    				tcp.searchSever(port);
	    	    		if(tcp.getIp()==null) myhandler.sendEmptyMessage(1);
	    	    		else {
	    	    			Message msg = new Message();
	    	    			Bundle b =new Bundle();
	    	    			b.putString("ip", tcp.getIp());
	    	    			b.putInt("port", port);
	    	    			msg.setData(b);
	    	    			msg.what = 0;
	    	    			myhandler.sendMessage(msg);
	    	    		}
	    			} catch (NotWifiException e) {
	    				myhandler.sendEmptyMessage(2);
	    			}
    			}
    		}.start();
    	}
    	public void connectHost(String ipAddress,int port,Handler handler){
    		tcp.setSeverAddress(ipAddress, port);
    		final Handler myhandler= (handler==null?new Handler():handler);
			new Thread(){
    			@Override
    			public void run() {
    	    		try {
						tcp.connect();
						String json = tcp.call("connect");
						TempStates.instance(ConnectionService.this).severSettings = new Gson().fromJson(json.trim(), SeverSettings.class);
						myhandler.sendEmptyMessage(0);
					} catch (SocketConnectException|NoInputStringException e) {
						myhandler.sendEmptyMessage(1);
					}finally{
						tcp.disConnect();
					}
    			};
    		}.start();
    	}
    	
    	public void startSync(){
    		mTimer.cancel();
    		mTimer = new Timer();
    		mTimer.schedule(new MyTimerTask(), 0 , TempStates.instance(ConnectionService.this).severSettings.timer);
    	}
    	public void stopSync(){
    		mTimer.cancel();
    		ConnectionService.this.stopSelf();
    	}
    	
    	public void updateDatebase(Handler handler){
    		final Handler myhandler= (handler==null?new Handler():handler);
			new Thread(){
				@Override
				public void run() {
					try{
						tcp.connect();
						myhandler.sendEmptyMessage(-1);
						String json = tcp.call("codeTable");
						myhandler.sendEmptyMessage(-2);
						CodeTabel table = new Gson().fromJson(json.trim(), CodeTabel.class);
						myhandler.sendEmptyMessage(-3);
						Database.getInstance(ConnectionService.this).initializeCodeTable(table,myhandler);
						myhandler.sendEmptyMessage(0);
					}catch(SocketConnectException | NoInputStringException e){
						myhandler.sendEmptyMessage(1);
					}finally{
						tcp.disConnect();
					}
				};
			}.start();
    	}
    	
    }
	
	private class MyTimerTask extends TimerTask{
		@Override
		public void run() {
//			Log.i("log", new Date().toString());
			syncCheckin();
		}
	}
}
