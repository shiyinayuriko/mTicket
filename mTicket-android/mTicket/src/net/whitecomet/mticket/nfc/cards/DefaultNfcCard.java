package net.whitecomet.mticket.nfc.cards;

import java.io.IOException;

import net.whitecomet.mticket.nfc.AbstractNfcCard;
import android.nfc.Tag;

public class DefaultNfcCard extends AbstractNfcCard {

	protected Tag tag;

	public DefaultNfcCard(Tag tag) {
		this.tag = tag;
	}

	@Override
	public byte[] getData() throws IOException {
		return new byte[0];
	}

	@Override
	public byte[] getIdBytes() {
		return tag.getId();
	}

	@Override
	public String getIdHex() {
		byte[] id = tag.getId();
		String ret = "";
		for(int i=0;i<id.length;i++){
			ret+=String.format(":%1$02X",id[i]);
		}
		return ret.substring(1);
	}

	@Override
	public void writeData(byte[] data) throws IOException {
	}

}
