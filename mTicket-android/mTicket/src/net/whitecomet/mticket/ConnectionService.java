package net.whitecomet.mticket;

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
		try {
			tcp.link();
		} catch (SocketConnectException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return START_REDELIVER_INTENT;
	}	
	
	public class ConnectionServiceBinder extends Binder{
    	public void searchHost(final int port,Handler handler){
    		final Handler myhandler;
			if(handler==null) myhandler = new Handler();
			else myhandler = handler;
    		
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
	    	    			msg.setData(b);
	    	    			myhandler.sendMessage(msg);
	    	    		}
	    			} catch (NotWifiException e) {
	    				myhandler.sendEmptyMessage(2);
	    			}
    			}
    		}.start();
    	}
    	
    	public void send(String str,Handler handler){
    		try {
				tcp.call(str);
			} catch (NoInputStringException | SocketConnectException e) {
				e.printStackTrace();
			}
    	}
    }
}
