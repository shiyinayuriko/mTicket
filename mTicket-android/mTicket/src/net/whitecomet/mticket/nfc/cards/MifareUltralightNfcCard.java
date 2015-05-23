package net.whitecomet.mticket.nfc.cards;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import android.nfc.Tag;
import android.nfc.tech.MifareUltralight;

public class MifareUltralightNfcCard extends DefaultNfcCard {

	public MifareUltralightNfcCard(Tag tag) throws IOException {
		super(tag);
	}

	private static final int PAGE_START = 6;
	private static final int PAGE_END = 15;
	@Override
	public byte[] getData() throws IOException {
		byte[] ret = new byte[(PAGE_END-PAGE_START+1) * 4];
		MifareUltralight mu = MifareUltralight.get(tag);
		mu.connect();
		for(int i=PAGE_START;i<=PAGE_END;i++){
			byte[] page = mu.readPages(i);
			for(int j=0;j<4;j++) ret[(i-PAGE_START)*4+j] = page[j];
		}
		mu.close();
		return ret;
	}
	
	@Override
	public void writeData(byte[] data) throws IOException {
		MifareUltralight mu = MifareUltralight.get(tag);
		mu.connect();
		for(int i=PAGE_START;i<=PAGE_END;i++){
			byte[] tmp = Arrays.copyOfRange(data, (i-PAGE_START)*4, (i-PAGE_START)*4+4);
			mu.writePage(i, tmp);
		}
		mu.close();
	}
}
