package net.whitecomet.mticket.logic;

public class LogicException extends Exception{
	private static final long serialVersionUID = -5568686964034679631L;
	public LogicException(Exception e) {
		super(e);
	}
}