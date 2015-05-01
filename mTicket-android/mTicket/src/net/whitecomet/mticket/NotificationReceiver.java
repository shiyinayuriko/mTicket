package net.whitecomet.mticket;

import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.support.v4.app.NotificationCompat;
import android.util.Log;

public class NotificationReceiver extends BroadcastReceiver {

	private static final int ID = 1;
	private static int totalUpload = 0;
	private static int totalDownload = 0;
	
	@Override
	public void onReceive(Context context, Intent intent) {
		NotificationManager mNotificationManager =(NotificationManager)context.getSystemService(Context.NOTIFICATION_SERVICE);
		
		NotificationCompat.Builder mBuilder = new NotificationCompat.Builder(context); 
		mBuilder.setSmallIcon(R.drawable.ic_launcher);
		
		Intent resultIntent = new Intent(context, MainActivity.class);
		PendingIntent resultPendingIntent = PendingIntent.getActivity(context, 0, resultIntent, PendingIntent.FLAG_UPDATE_CURRENT);
		mBuilder.setContentIntent(resultPendingIntent);
		
		String action = intent.getAction();
		if(context.getString(R.string.broadcast_connected).equals(action)){
			mBuilder.setContentTitle(context.getString(R.string.notification_connected_title));
			mBuilder.setContentText(context.getString(R.string.notification_connected_content));
		}else if(context.getString(R.string.broadcast_disconnected).equals(action)){
			mBuilder.setContentTitle(context.getString(R.string.notification_disconnected_title));
			String content = String.format(context.getString(R.string.notification_disconnected_content),totalUpload,totalDownload);
			mBuilder.setContentText(content);
			totalUpload = 0;
			totalDownload = 0;
		}else if(context.getString(R.string.broadcast_sync_error).equals(action)){
			mBuilder.setContentTitle(context.getString(R.string.notification_sync_error_title));
			String content = String.format(context.getString(R.string.notification_sync_error_content), intent.getStringExtra("errorInfo"));
			mBuilder.setContentText(content);
		}else if(context.getString(R.string.broadcast_sync_start).equals(action)){
			mBuilder.setContentTitle(context.getString(R.string.notification_sync_start_title));
			String content = String.format(context.getString(R.string.notification_sync_start_content), intent.getIntExtra(("checkinNum") , -1));
			mBuilder.setContentText(content);
		}else if(context.getString(R.string.broadcast_sync_end).equals(action)){
			mBuilder.setContentTitle(context.getString(R.string.notification_sync_end_title));
			String syncTime = intent.getStringExtra("syncTime");
			int uploadNum = intent.getIntExtra(("uploadNum") , -1);
			int downloadNum = intent.getIntExtra(("downloadNum") , -1);
			String content = String.format(context.getString(R.string.notification_sync_end_content), syncTime,uploadNum,downloadNum);
			mBuilder.setContentText(content);
			totalUpload += uploadNum;
			totalDownload += downloadNum;
		}		
		
		Notification notify = mBuilder.build();
		notify.flags = Notification.FLAG_NO_CLEAR;
		if(context.getString(R.string.broadcast_disconnected).equals(action)) notify.flags = Notification.FLAG_AUTO_CANCEL;
		mNotificationManager.notify(ID,notify);
	}
}
