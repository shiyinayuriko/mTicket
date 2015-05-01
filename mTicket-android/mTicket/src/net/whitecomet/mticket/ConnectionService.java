package net.whitecomet.mticket;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;
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
		mTimer.schedule(new MyTimerTask(), 5000 , TempStates.instance(this).severSettings.timer);
		
		
		
		return START_REDELIVER_INTENT;
	}	
	
	@Override
	public void onDestroy() {
		mTimer.cancel();
		super.onDestroy();
	}
	
	public CheckinData[] syncCheckin(CheckinData[] checkin) throws Exception{
		CheckinData[] retCheckin = null ;
		try{
			tcp.connect();
			Gson gson = new Gson();
			
			long timestamp = TempStates.instance(this).getSyncTimetamp();
			String ret = tcp.call("syncCheckin "+ timestamp + " " + gson.toJson(checkin));
			
			long newTimestamp = Long.parseLong(ret.substring(0,ret.indexOf(' ')));
			String json = ret.substring(ret.indexOf(' ')+1);
			
			retCheckin = gson.fromJson(json, CheckinData[].class); 
			
			Database.getInstance(ConnectionService.this).addSyncedCheckinData(retCheckin);
			Database.getInstance(ConnectionService.this).setMarksSynced(newTimestamp);
			
			TempStates.instance(this).setSyncTimetamp(newTimestamp);
		}catch(SocketConnectException | NoInputStringException e){
			throw e;
		}finally{
			tcp.disConnect();
		}
		return retCheckin==null?new CheckinData[0]:retCheckin;
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
						
						sendBroadcast(new Intent(getString(R.string.broadcast_connected)));
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
    		mTimer.schedule(new MyTimerTask(), 5000 , TempStates.instance(ConnectionService.this).severSettings.timer);
    	}
    	public void stopSync(){
    		mTimer.cancel();
			sendBroadcast(new Intent(getString(R.string.broadcast_disconnected)));
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
    		boolean result = callScript(json);
    		if(result) Database.getInstance(ConnectionService.this).checkin(table.id);
    		return result;
    	}
    }
	
    private static final String preCheckin = "function pre(tmps) {var tmp = eval('(' + tmps + ')');return checkin(tmp);}";
	private boolean callScript(String json) throws LogicException{
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
		DateFormat formater = new SimpleDateFormat("yy-MM-dd hh:mm:ss",Locale.getDefault());
		@Override
		public void run() {
			try {
				CheckinData[] checkin = Database.getInstance(ConnectionService.this).markUnsynced();
				Intent intent = new Intent(getString(R.string.broadcast_sync_start));
				intent.putExtra("checkinNum", checkin.length);
				sendBroadcast(intent);
				
				CheckinData[] retCheckin = syncCheckin(checkin);
				Intent intent2 = new Intent(getString(R.string.broadcast_sync_end));
				String dateStr = formater.format(new Date());
				
				intent2.putExtra("syncTime", dateStr);
				intent2.putExtra("uploadNum", checkin.length);
				intent2.putExtra("downloadNum", retCheckin.length);
				sendBroadcast(intent2);
			} catch (Exception e) {
				Intent intent = new Intent(getString(R.string.broadcast_sync_error));
				intent.putExtra("errorInfo", e.getMessage());
				sendBroadcast(intent);
			}
		}
	}
}
