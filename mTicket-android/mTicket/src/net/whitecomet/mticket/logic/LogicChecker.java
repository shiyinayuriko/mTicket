package net.whitecomet.mticket.logic;

import org.mozilla.javascript.Context;
import org.mozilla.javascript.Function;
import org.mozilla.javascript.ScriptableObject;

import net.whitecomet.mticket.data.Database;
import net.whitecomet.mticket.data.TempStates;
import net.whitecomet.mticket.data.beans.CodeDataReturn;
import net.whitecomet.mticket.nfc.CardBean;

import com.google.gson.Gson;

public class LogicChecker {
    private static final String preCheckin = "function pre(tmps) {var tmp = eval('(' + tmps + ')');return checkin(tmp);}";
    private static final String preCheckinIc = "function pre(tmps,tmps2,time) {var tmp = eval('(' + tmps + ')');var tmp2 = eval('(' + tmps2 + ')');return checkinIc(tmp,tmp2,time);}";

    private android.content.Context context;
    public LogicChecker(android.content.Context context){
    	this.context = context.getApplicationContext();
    }
    
	public boolean checkin(CodeDataReturn codeData) throws LogicException{
		if(codeData==null) return false;
		
		String json = new Gson().toJson(codeData);
		boolean result = callScript(json);
		if(result) Database.getInstance(context).checkin(codeData.id);
		return result;
	}
	
	private boolean callScript(String json) throws LogicException{
		try{
			//TODO 预编译
			Context rhino = Context.enter();
	        rhino.setOptimizationLevel(-1);
	        ScriptableObject scope = rhino.initStandardObjects();

	        rhino.evaluateString(scope,TempStates.instance(context).severSettings.checkin_logic, "funtionCheckin", 0, null);
	        Function function = rhino.compileFunction(scope, preCheckin, "preCheckin", 1, null);	
			
	        Object result = function.call(rhino, scope, scope,  new Object[]{json});
	        Context.exit();
          
			return (boolean)result;
		}catch(Exception e){
	    	throw new LogicException(e);
	    }
	}
	
	public boolean checkin(CodeDataReturn codeData,CardBean card) throws LogicException{
		if(codeData==null) return false;
		
		String json = new Gson().toJson(codeData);
		String json2 = new Gson().toJson(card);
		boolean result = callScript(json,json2);
		if(result) Database.getInstance(context).checkin(codeData.id);
		return result;
	}
	
	private boolean callScript(String json,String json2) throws LogicException{
		try{
			//TODO 预编译
			Context rhino = Context.enter();
	        rhino.setOptimizationLevel(-1);
	        ScriptableObject scope = rhino.initStandardObjects();

	        rhino.evaluateString(scope,TempStates.instance(context).severSettings.checkin_logic, "funtionCheckin", 0, null);
	        Function function = rhino.compileFunction(scope, preCheckinIc, "preCheckinIc", 1, null);	
			
	        Object result = function.call(rhino, scope, scope,  new Object[]{json,json2,System.currentTimeMillis()});
	        Context.exit();
          
			return (boolean)result;
		}catch(Exception e){
	    	throw new LogicException(e);
	    }
	}
}
