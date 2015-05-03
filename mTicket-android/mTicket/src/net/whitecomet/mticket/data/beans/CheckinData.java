package net.whitecomet.mticket.data.beans;

import java.io.Serializable;

public class CheckinData implements Serializable{
	private static final long serialVersionUID = -7463946408455090315L;
	public int id;
    public String checkin_time;
    public long sync_time = 0;
}
