package net.whitecomet.mticket.nfc;

import net.whitecomet.mticket.MainActivity;
import android.app.Activity;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.nfc.NfcAdapter;
import android.nfc.Tag;
import android.widget.Toast;

public class NfcManager {
	private Context context;
	private NfcAdapter mAdapter;
	private String[][] techList = new String[][] {
			new String[] {
					android.nfc.tech.NfcA.class.getName(),
					android.nfc.tech.MifareClassic.class.getName()
					}, 
					new String[] {
					android.nfc.tech.NfcA.class.getName(),
					}, 
	};
	private IntentFilter[] intentFilters = new IntentFilter[] { 
			new IntentFilter(NfcAdapter.ACTION_TECH_DISCOVERED), 
			new IntentFilter(NfcAdapter.ACTION_TAG_DISCOVERED)
	};
	
	private PendingIntent pendingIntent;

	public NfcManager(Context context) {
		this.context = context.getApplicationContext();
		mAdapter = NfcAdapter.getDefaultAdapter(this.context);
		pendingIntent = PendingIntent.getActivity(this.context, 0, new Intent(this.context, MainActivity.class), 0);
		

	}

	public void onPause(Activity activity){
        mAdapter.disableForegroundDispatch(activity);

	}
	public void onResume(Activity activity){
		mAdapter.enableForegroundDispatch(activity, pendingIntent, intentFilters, techList);
	}
	public void dealwithIntent(Intent intent){
		Tag tag = intent.getParcelableExtra(NfcAdapter.EXTRA_TAG);
		Toast.makeText(context, intent.getAction(), Toast.LENGTH_LONG).show();
		tag.getTechList();
	}
}
