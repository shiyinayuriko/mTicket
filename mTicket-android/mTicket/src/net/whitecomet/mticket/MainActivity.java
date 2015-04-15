package net.whitecomet.mticket;

import net.whitecomet.mticket.ConnectionService.ConnectionServiceBinder;
import android.support.v7.app.ActionBarActivity;
import android.content.ComponentName;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.Handler;
import android.os.IBinder;
import android.os.Message;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;


public class MainActivity extends ActionBarActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        
        Intent intent = new Intent(this.getApplicationContext(),ConnectionService.class);
        bindService(intent, serviceConnection, BIND_AUTO_CREATE);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }
    
    
    
    
	ConnectionServiceBinder connectionBinder;
	private ServiceConnection serviceConnection = new ServiceConnection(){
		@Override
		public void onServiceConnected(ComponentName arg0, IBinder arg1) {
			connectionBinder = (ConnectionServiceBinder) arg1;
		}

		@Override
		public void onServiceDisconnected(ComponentName arg0) {
		}
	};
	
	
    public void search(View view){
        connectionBinder.searchHost(8000, new Handler(){
    		@Override
    		public void handleMessage(Message msg) {
    			super.handleMessage(msg);
    		}
        });
    }
    
    public void link(View view){
        Intent intent = new Intent(this.getApplicationContext(),ConnectionService.class);
        startService(intent);
    }
    public void send(View view){
    	connectionBinder.send("aaa test", null);
    }
}
