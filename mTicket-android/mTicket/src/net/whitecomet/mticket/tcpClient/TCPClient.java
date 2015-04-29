package net.whitecomet.mticket.tcpClient;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.HashSet;
import java.util.Set;

import net.whitecomet.mticket.data.TempStates;

import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.wifi.WifiManager;

public class TCPClient {
	private String ip = null;
	private int port;
	private Context context;
	public TCPClient(Context context){
		this.context = context.getApplicationContext();
	}
	
	public String getIp() {
		return ip;
	}
	public int getPort() {
		return port;
	}
	public void setSeverAddress(String ip,int port){
		this.ip = ip;
		this.port = port;
	}
	
	public void searchSever(int port) throws NotWifiException{
		this.port = port;
		if(!isWifi()) throw new NotWifiException();
		String localIp = getWifiIp();
		final String preIp = localIp.substring(0,localIp.lastIndexOf(".")+1);
		ip = null;
		Set<Thread> threads =new HashSet<Thread>();
		for(int ipx = 0;ipx<=255;ipx++){
			Thread thread = new MyThread(preIp + ipx);
			threads.add(thread);
			thread.start();
		}

		for(Thread t:threads){
			try {
				t.join();
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	}
	private class MyThread extends Thread{
		private String curIp;
		private static final String PING = "ping -c 1 -w 0.5 ";

		private MyThread(String curIp) {
			this.curIp = curIp;
		}
		@Override
		public void run() {
			Process proc = null;
			try {
				proc = Runtime.getRuntime().exec(PING+curIp);
				int result = proc.waitFor();
				if (result == 0) {
					Socket socket = new Socket(curIp, port);
					socket.setSoTimeout(5000);
			        BufferedReader br = new BufferedReader(new InputStreamReader(socket.getInputStream()));
			        BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream()));
			        String pingLine = "ping "+Math.random();
			        bw.write(pingLine+"\n");
			        bw.flush();
			        String str = br.readLine();
			        if(str.equals(pingLine)){
			        	TCPClient.this.ip = curIp;
			        }
			        socket.close();
				}
			} catch (IOException|InterruptedException e) {
				e.printStackTrace();
			}finally{
				if(proc!=null) proc.destroy();
			}
		}
	}
	
	private Socket socket;
	private BufferedReader br;
	private BufferedWriter bw;
	
	public void connect() throws SocketConnectException{
		try {
			socket = new Socket(ip, port);
			socket.setSoTimeout(TempStates.instance(context).severSettings==null?50000:TempStates.instance(context).severSettings.tcp_timeout);
	        br = new BufferedReader(new InputStreamReader(socket.getInputStream()));
	        bw = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream()));
		} catch (UnknownHostException e) {
			throw new SocketConnectException(e);
		} catch (IOException e) {
			throw new SocketConnectException(e);
		}
	}
	
	public String call(String s) throws NoInputStringException, SocketConnectException{
		if(s==null || s.trim().equals("")) throw new NoInputStringException();
		
		try {
			bw.write(s + (s.endsWith("\n")?"":"\n"));
	        bw.flush();
	        String str = br.readLine();
	        return str;
		} catch (IOException e) {
			throw new SocketConnectException(e);
		}
	}
	
	public void disConnect(){
		try {
			br.close();
			bw.close();
	        socket.close();
		} catch (IOException e) {
			e.printStackTrace();
		}

	}

	private boolean isWifi() {
		ConnectivityManager connectivityManager = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
		NetworkInfo activeNetInfo = connectivityManager.getActiveNetworkInfo();
		if (activeNetInfo != null && activeNetInfo.getType() == ConnectivityManager.TYPE_WIFI) {
			return true;
		}
		return false;
	}
	private String getWifiIp(){
		WifiManager wm = (WifiManager) context.getSystemService(Context.WIFI_SERVICE);  
		int hostip = wm.getConnectionInfo().getIpAddress();
        String ip = (hostip & 0xFF)+"."+((hostip>>8)&0xFF)+ "." + ((hostip >> 16 ) & 0xFF) +"."+((hostip >> 24 ) & 0xFF);  
        return ip;
	}
}
