package net.whitecomet.mticket;

import java.util.TimerTask;

import com.google.gson.Gson;

import net.whitecomet.mticket.data.Database;
import net.whitecomet.mticket.data.TempStates;
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

public class ConnectionService extends Service {
	TCPClient tcp;
	@Override
	public void onCreate() {
		super.onCreate();
		tcp = new TCPClient(this.getApplicationContext());
	}
	
	ConnectionServiceBinder binder = new ConnectionServiceBinder();
	@Override
	public IBinder onBind(Intent arg0) {
		return binder;
	}

	
	@Override
	public int onStartCommand(Intent intent, int flags, int startId) {
		super.onStartCommand(intent, flags, startId);
		
//		mTimer.schedule(new MyTimerTask(), 0 , 10 * 1000);
		
		return START_REDELIVER_INTENT;
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
			// TODO Auto-generated method stub
		}
	}
}
