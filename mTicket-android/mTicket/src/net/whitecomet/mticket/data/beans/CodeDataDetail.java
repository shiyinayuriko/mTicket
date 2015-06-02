package net.whitecomet.mticket.data.beans;

import java.io.Serializable;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;

public class CodeDataDetail implements Serializable{
	private static final long serialVersionUID = 2898986580341574916L;
	public Map<String,String> info;
	public List<CheckinData> checkin;
	public int id;
	public String code;
	
	@Override
	public String toString() {
		String ret = "";
		for(Entry<String, String> entry:info.entrySet()){
			ret+=entry.getKey()+":"+entry.getValue()+"\n";
		}
		for(CheckinData checkinData:checkin){
			ret+=checkinData.checkin_time+"\n";
		}
		return ret;
	}
}
