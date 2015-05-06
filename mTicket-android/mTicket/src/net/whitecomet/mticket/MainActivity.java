package net.whitecomet.mticket;

import java.io.Serializable;
import java.util.Random;

import net.whitecomet.mticket.ConnectionService.ConnectionServiceBinder;
import net.whitecomet.mticket.data.Database;
import net.whitecomet.mticket.data.TempStates;
import net.whitecomet.mticket.data.beans.CheckinData;
import net.whitecomet.mticket.data.beans.CodeDataReturn;
import net.whitecomet.mticket.scanner.CaptureActivity;
import android.support.v7.app.ActionBarActivity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.ComponentName;
import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.content.res.Configuration;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.Handler;
import android.os.IBinder;
import android.os.Message;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class MainActivity extends ActionBarActivity {

	private TextView textLastCheckCode;
	private TextView textLastCheckCodeData;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		bindService();
		TempStates tempStates = TempStates.instance(this);
		EditText editTextIpAddress = (EditText) findViewById(R.id.editText_ipAddress);
		EditText editTextPort = (EditText) findViewById(R.id.editText_port);

		editTextIpAddress.setText(tempStates.getIpaddress());
		editTextPort.setText(tempStates.getPort() == null ? null : tempStates.getPort() + "");
		
		textLastCheckCode = (TextView) findViewById(R.id.text_lastCheckCode);
		textLastCheckCodeData = (TextView) findViewById(R.id.text_lastCheckCodeData);
	}

	@Override
	protected void onDestroy() {
		super.onDestroy();
	}

	@Override
	public void onBackPressed() {
		moveTaskToBack(true);
	}
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			return true;
		}
		return super.onOptionsItemSelected(item);
	}

	ConnectionServiceBinder connectionBinder;
	private ServiceConnection serviceConnection = new ServiceConnection() {
		@Override
		public void onServiceConnected(ComponentName arg0, IBinder arg1) {
			connectionBinder = (ConnectionServiceBinder) arg1;
		}

		@Override
		public void onServiceDisconnected(ComponentName arg0) {
		}
	};

	private void bindService() {
		Intent intent = new Intent(this.getApplicationContext(),
				ConnectionService.class);
		bindService(intent, serviceConnection, BIND_AUTO_CREATE);
	}

	public void searchHost(View view) {
		final EditText editTextIpAddress = (EditText) findViewById(R.id.editText_ipAddress);
		final EditText editTextPort = (EditText) findViewById(R.id.editText_port);
		int port;
		try {
			port = Integer.parseInt(editTextPort.getText().toString());
		} catch (Exception e) {
			port = 8000;
		}

		final CharSequence strDialogTitle = getString(R.string.ProgressDialog_wait_title);
		final CharSequence strDialogBody = getString(R.string.ProgressDialog_search_content);
		final ProgressDialog myDialog = ProgressDialog.show(this,
				strDialogTitle, strDialogBody, true);
		myDialog.setCancelable(false);
		connectionBinder.searchHost(port, new Handler() {
			@Override
			public void handleMessage(Message msg) {
				myDialog.dismiss();
				AlertDialog.Builder builder = new AlertDialog.Builder(MainActivity.this);
				builder.setTitle(getString(R.string.AlertDialog_searchResult_title_failure));
				builder.setNeutralButton(getString(R.string.close), null);

				switch (msg.what) {
				case 0:
					String ip = msg.getData().getString("ip");
					int port = msg.getData().getInt("port");
					String str = String.format(getString(R.string.AlertDialog_searchResult_content_0), ip, port);
					builder.setMessage(str);
					builder.setTitle(getString(R.string.AlertDialog_searchResult_title_success));
					editTextIpAddress.setText(ip);
					editTextPort.setText(port + "");
					break;
				case 1:
					builder.setMessage(getString(R.string.AlertDialog_searchResult_content_1));
					editTextIpAddress.setText("");
					editTextPort.setText("");
					break;
				case 2:
					builder.setMessage(getString(R.string.AlertDialog_searchResult_content_2));
					editTextIpAddress.setText("");
					editTextPort.setText("");
					break;
				}
				builder.show();
			}
		});

	}

	public void connectHost(final View view) {
		final EditText editTextIpAddress = (EditText) findViewById(R.id.editText_ipAddress);
		final EditText editTextPort = (EditText) findViewById(R.id.editText_port);
		int port;
		try {
			port = Integer.parseInt(editTextPort.getText().toString());
		} catch (Exception e) {
			port = -1;
		}
		final String ipAddress = editTextIpAddress.getText().toString();

		final CharSequence strDialogTitle = getString(R.string.ProgressDialog_wait_title);
		final CharSequence strDialogBody = getString(R.string.ProgressDialog_connect_content);
		final ProgressDialog myDialog = ProgressDialog.show(this, strDialogTitle, strDialogBody, true);
		myDialog.setCancelable(false);
		connectionBinder.connectHost(ipAddress, port, new Handler() {
			@Override
			public void handleMessage(Message msg) {
				myDialog.dismiss();
				AlertDialog.Builder builder = new AlertDialog.Builder( MainActivity.this);
				builder.setCancelable(false);
				switch (msg.what) {
				case 0:
					editTextIpAddress.setEnabled(false);
					editTextPort.setEnabled(false);
					Button connectButton = (Button) findViewById(R.id.button_connect_host);
					connectButton.setEnabled(false);
					connectButton.setVisibility(View.GONE);
					Button disconnectButton = (Button) findViewById(R.id.button_disconnect_host);
					disconnectButton.setEnabled(true);
					disconnectButton.setVisibility(View.VISIBLE);
					((Button) findViewById(R.id.button_search_host)).setEnabled(false);
					((Button) findViewById(R.id.button_open_camera)).setEnabled(true);

					builder.setTitle(getString(R.string.AlertDialog_connectResult_title_success));
					builder.setMessage(getString(R.string.AlertDialog_connectResult_content_0));
					builder.setPositiveButton(getString(R.string.yes),new OnClickListener() {
						@Override
						public void onClick(DialogInterface dialog, int which) {
							MainActivity.this.updateDatebase(view);
						}
					});
					builder.setNegativeButton(getString(R.string.no), new OnClickListener() {
						@Override
						public void onClick(DialogInterface arg0, int arg1) {
							connectionBinder.startSync();
						}
					});
					
					builder.show();
					break;
				case 1:
					builder.setTitle(getString(R.string.AlertDialog_connectResult_title_failure));
					builder.setMessage(getString(R.string.AlertDialog_connectResult_content_1));
					builder.setNeutralButton(getString(R.string.close), null);
					builder.show();
					break;
				case 2:
					builder.setTitle(getString(R.string.AlertDialog_connectResult_title_failure));
					builder.setMessage(getString(R.string.AlertDialog_connectResult_content_2));
					builder.setNeutralButton(getString(R.string.close), null);
					builder.show();
					break;
				}
			}
		});
	}

	public void disconnectHost(View view) {
		EditText editTextIpAddress = (EditText) findViewById(R.id.editText_ipAddress);
		EditText editTextPort = (EditText) findViewById(R.id.editText_port);
		editTextIpAddress.setEnabled(true);
		editTextPort.setEnabled(true);
		Button connectButton = (Button) findViewById(R.id.button_connect_host);
		connectButton.setEnabled(true);
		connectButton.setVisibility(View.VISIBLE);
		Button disconnectButton = (Button) findViewById(R.id.button_disconnect_host);
		disconnectButton.setEnabled(false);
		disconnectButton.setVisibility(View.GONE);
		((Button) findViewById(R.id.button_search_host)).setEnabled(true);
		
		connectionBinder.stopSync();
	}

	private void updateDatebase(final View view) {
		final CharSequence strDialogTitle = getString(R.string.ProgressDialog_wait_title);
		final CharSequence strDialogBody = getString(R.string.ProgressDialog_updateDatabase_content__0);

		final ProgressDialog myDialog = new ProgressDialog(this);
		myDialog.setCancelable(false);

		myDialog.setTitle(strDialogTitle);
		myDialog.setMessage(strDialogBody);
		myDialog.setProgressStyle(ProgressDialog.STYLE_HORIZONTAL);
		myDialog.setIndeterminate(false);
		myDialog.show();
		connectionBinder.updateDatebase(new Handler() {
			@Override
			public void handleMessage(Message msg) {
				switch (msg.what) {
				case -1:
					myDialog.setMessage(getString(R.string.ProgressDialog_updateDatabase_content__1));
					myDialog.setProgress(5);
					break;
				case -2:
					myDialog.setMessage(getString(R.string.ProgressDialog_updateDatabase_content__2));
					myDialog.setProgress(15);
					break;
				case -3:
					myDialog.setMessage(getString(R.string.ProgressDialog_updateDatabase_content__3));
					myDialog.setProgress(40);
					break;
				case -4:
					myDialog.setProgress(40+msg.arg1*60/msg.arg2);
					break;
				case 0:
					myDialog.dismiss();
					connectionBinder.startSync();
					break;
				case 1:
					myDialog.dismiss();
					disconnectHost(view);
					AlertDialog.Builder builder = new AlertDialog.Builder(MainActivity.this);
					builder.setTitle(getString(R.string.AlertDialog_searchResult_title_failure));
					builder.setNeutralButton(getString(R.string.close), null);
					builder.setTitle(getString(R.string.AlertDialog_connectResult_title_failure));
					builder.setMessage(getString(R.string.AlertDialog_connectResult_content_1));
					builder.setNeutralButton(getString(R.string.close), null);
					builder.show();
					break;
				}
			}
		});
	}
	
	public void openCamera(View view){
		startActivityForResult(new Intent(this,CaptureActivity.class),CaptureActivity.REQUEST_DEFAULT);
	}
	

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if(data==null) return;
		
		CodeDataReturn lastCheckCodeData = (CodeDataReturn) data.getSerializableExtra("lastCheckCodeData");
		String lastCheckCode = data.getStringExtra("lastCheckCode");
		if(lastCheckCode!=null) textLastCheckCode.setText(lastCheckCode);
		if(lastCheckCodeData!=null){
			textLastCheckCodeData.setText(lastCheckCodeData.toString());
		}else{
			textLastCheckCodeData.setText(getString(R.string.textview_checkCodeData_default));
		}
	}
	
	public void test2(View view){
		int id = new Random().nextInt(20000)+1;
		
		Database.getInstance(this).checkin(id);
		AlertDialog.Builder builder = new AlertDialog.Builder(this);
		builder.setTitle("checkin");
		builder.setMessage(id+"");
		builder.show();
	}
}