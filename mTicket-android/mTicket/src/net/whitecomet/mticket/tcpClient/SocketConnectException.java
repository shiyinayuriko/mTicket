package net.whitecomet.mticket.tcpClient;

import java.io.IOException;

public class SocketConnectException extends Exception {
	private static final long serialVersionUID = 1816020629542286330L;
	public SocketConnectException(IOException e) {
		super(e);
	}
}
