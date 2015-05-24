package net.whitecomet.mticket.nfc;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class CardBean implements Serializable,Cloneable {
	private static final long serialVersionUID = -902601060794226603L;
	public int id;
	public boolean canIn;
	public long lastTime;

	public static CardBean fromBytes(byte[] bytes){
		CardBean card = new CardBean();
		int id = 0;
		for(int i=3;i>=0;i--){
			id = (id <<8) | (bytes[i] & 0xFF);
		}

		boolean canIn = ((bytes[4]&1)==1) ;
		long lastTime = 0;
		for(int i=15;i>=8;i--){
			lastTime = (lastTime <<8) | (bytes[i] & 0xFF);
		}
		
		card.id = id;
		card.canIn = canIn;
		card.lastTime = lastTime;
		return card;
	}
	
	public byte[] toBytes(int len){
		byte[] bytes = new byte[len];
		for(int i=0;i<4;i++){
			bytes[i] = (byte) ((this.id >> (i*8)) & 0xFF);
		}
		bytes[4] = (byte) (this.canIn? 1: 0);
		for(int i=8;i<16;i++){
			bytes[i] = (byte) ((this.lastTime >> (i*8)) & 0xFF);
		}
		return bytes;
	}
	
//	public static byte[] getByteLine(byte[][] bytes){
//		int len = 0;
//		for(int i=0;i<bytes.length;i++){
//			len += bytes.length;
//		}
//		byte[] ret = new byte[len];
//		int counter = 0;
//		for(int i=0;i<bytes.length;i++){
//			for(int j=0;j<bytes[i].length;j++){
//				ret[counter] = bytes[i][j];
//			}
//		}
//		
//		return ret;
//	}
	@Override
	public CardBean clone(){
		CardBean card = new CardBean();
		card.id = this.id;
		card.canIn = this.canIn;
		card.lastTime = this.lastTime;
		return card;
	}
	
	@Override
	public String toString() {
		return String.format("card id:%1$d\ncan in:%2$s\nlastTime:%3$s\n", this.id,this.canIn+"",new Date(this.lastTime).toString());
	}
}	

