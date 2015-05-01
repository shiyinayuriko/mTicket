package net.whitecomet.mticket;

import java.util.Timer;
import java.util.TimerTask;

import org.mozilla.javascript.Context;
import org.mozilla.javascript.Function;
import org.mozilla.javascript.ScriptableObject;

import com.google.gson.Gson;

import net.whitecomet.mticket.data.Database;
import net.whitecomet.mticket.data.Database.CodeDataReturn;
import net.whitecomet.mticket.data.TempStates;
import net.whitecomet.mticket.data.beans.CheckinData;
import net.whitecomet.mticket.data.beans.CodeTable;
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
    	public void connectHost(final String ipAddress,final int port,Handler handler){
    		tcp.setSeverAddress(ipAddress, port);
    		final Handler myhandler= (handler==null?new Handler():handler);
			new Thread(){
    			@Override
    			public void run() {
    	    		try {
						tcp.connect();
						String json = tcp.call("connect");
						TempStates.instance(ConnectionService.this).severSettings = new Gson().fromJson(json.trim(), SeverSettings.class);

			    		TempStates.instance(ConnectionService.this).setHost(ipAddress, port);
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
						CodeTable table = new Gson().fromJson(json.trim(), CodeTable.class);
						myhandler.sendEmptyMessage(-3);
						Database.getInstance(ConnectionService.this).initializeCodeTable(table,myhandler);
						myhandler.sendEmptyMessage(0);
						
						TempStates.instance(ConnectionService.this).setDatabaseUpdateTime(System.currentTimeMillis());
						TempStates.instance(ConnectionService.this).setSyncTimetamp(0);
					}catch(SocketConnectException | NoInputStringException e){
						myhandler.sendEmptyMessage(1);
					}finally{
						tcp.disConnect();
					}
				};
			}.start();
    	}
    	

    	public boolean checkin(String code) throws LogicException{
    		CodeDataReturn table = Database.getInstance(ConnectionService.this).getCodeInfo(code);
			if(table==null) return false;
			
    		String json = new Gson().toJson(table);
    		boolean result = call(json);
    		if(result) Database.getInstance(ConnectionService.this).checkin(table.id);
    		return result;
    	}
    }
	
    private static final String preCheckin = "function pre(tmps) {var tmp = eval('(' + tmps + ')');return checkin(tmp);}";
	private boolean call(String json) throws LogicException{
		try{
			//TODO 预编译
			Context rhino = Context.enter();
	        rhino.setOptimizationLevel(-1);
	        ScriptableObject scope = rhino.initStandardObjects();

	        rhino.evaluateString(scope,TempStates.instance(ConnectionService.this).severSettings.checkin_logic, "funtionCheckin", 0, null);
	        Function function = rhino.compileFunction(scope, preCheckin, "preCheckin", 1, null);	
			
	        Object result = function.call(rhino, scope, scope,  new Object[]{json});
	        Context.exit();
          
			return (boolean)result;
		}catch(Exception e){
	    	throw new LogicException(e);
	    }
	}
	
	public static class LogicException extends Exception{
		private static final long serialVersionUID = -5568686964034679631L;
		public LogicException(Exception e) {
			super(e);
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
