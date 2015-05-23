package net.whitecomet.mticket.nfc;

import java.io.IOException;
import java.util.Arrays;

import net.whitecomet.mticket.MainActivity;
import net.whitecomet.mticket.data.Database;
import net.whitecomet.mticket.data.beans.CodeDataReturn;
import net.whitecomet.mticket.logic.LogicChecker;
import net.whitecomet.mticket.logic.LogicException;
import android.app.Activity;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.nfc.NfcAdapter;
import android.nfc.Tag;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
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
					new String[] {
					android.nfc.tech.MifareUltralight.class.getName(),
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
		logicChecker = new LogicChecker(context);
	}

	public void onPause(Activity activity){
        mAdapter.disableForegroundDispatch(activity);

	}
	public void onResume(Activity activity){
		mAdapter.enableForegroundDispatch(activity, pendingIntent, intentFilters, techList);
	}
	
	private LogicChecker logicChecker;
	public void dealwithIntent(Intent intent, Handler handler){
		Tag tag = intent.getParcelableExtra(NfcAdapter.EXTRA_TAG);
		Toast.makeText(context, intent.getAction(), Toast.LENGTH_LONG).show();
		
		try {
			AbstractNfcCard card = AbstractNfcCard.getNfcCard(tag);
			byte[] data = card.getData();
			CardBean cardBean = CardBean.fromBytes(data);
			

			CodeDataReturn codeData = Database.getInstance(context).getCodeInfo(card.getIdHex());
			boolean isPass = logicChecker.checkin(codeData,cardBean);
			
			Bundle bundle = new Bundle();
			bundle.putSerializable("lastCheckCodeData", codeData);
			bundle.putString("checkCode", card.getIdHex());
			bundle.putSerializable("cardBean", cardBean.clone());
			
			if(isPass){
				cardBean.id = codeData.id;
				cardBean.canIn = !cardBean.canIn;
				cardBean.lastTime = System.currentTimeMillis();
			}

			byte[] ret = cardBean.toBytes(data.length);
			card.writeData(ret);
			Message msg = new Message();
			msg.what = 0;
			msg.setData(bundle);
			if(handler!=null) handler.sendMessage(msg);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (LogicException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
}
