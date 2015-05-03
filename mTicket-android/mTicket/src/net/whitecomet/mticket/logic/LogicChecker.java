package net.whitecomet.mticket.logic;

import org.mozilla.javascript.Context;
import org.mozilla.javascript.Function;
import org.mozilla.javascript.ScriptableObject;

import net.whitecomet.mticket.data.Database;
import net.whitecomet.mticket.data.TempStates;
import net.whitecomet.mticket.data.beans.CodeDataReturn;

import com.google.gson.Gson;

public class LogicChecker {
    private static final String preCheckin = "function pre(tmps) {var tmp = eval('(' + tmps + ')');return checkin(tmp);}";

    private android.content.Context context;
    public LogicChecker(android.content.Context context){
    	this.context = context.getApplicationContext();
    }
    
	public boolean checkin(String code) throws LogicException{
		CodeDataReturn codeData = Database.getInstance(context).getCodeInfo(code);
		return checkin(codeData);
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
}
