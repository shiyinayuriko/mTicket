package net.whitecomet.mticket.nfc;

import java.io.IOException;
import java.util.Arrays;
import java.util.List;

import net.whitecomet.mticket.nfc.cards.DefaultNfcCard;
import net.whitecomet.mticket.nfc.cards.MifareClassicNfcCard;
import net.whitecomet.mticket.nfc.cards.MifareUltralightNfcCard;
import android.nfc.Tag;

public abstract class AbstractNfcCard {
	public static AbstractNfcCard getNfcCard(Tag tag) throws IOException{
		 List<String> techList = Arrays.asList(tag.getTechList());
		 if(techList.contains("android.nfc.tech.MifareUltralight")) return new MifareUltralightNfcCard(tag);
		 else if(techList.contains("android.nfc.tech.MifareClassic")) return new MifareClassicNfcCard(tag);
//		 else if(techList.contains("android.nfc.tech.NfcA")) return new MifareUltralightNfcCard(tag);
		 return new DefaultNfcCard(tag);
	}
	public abstract byte[] getData() throws IOException;
	public abstract void writeData(byte[] data) throws IOException;
	public abstract byte[] getIdBytes();
	public abstract String getIdHex();
}
