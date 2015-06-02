package net.whitecomet.mticket.logic;

import org.mozilla.javascript.Context;
import org.mozilla.javascript.Function;
import org.mozilla.javascript.ScriptableObject;

import net.whitecomet.mticket.data.Database;
import net.whitecomet.mticket.data.TempStates;
import net.whitecomet.mticket.data.beans.CodeDataDetail;

import com.google.gson.Gson;

public class LogicChecker {
    private static final String preCheckin = "function pre(tmps) {var tmp = eval('(' + tmps + ')');return checkin(tmp);}";

	private ScriptableObject scope;
	private Function function;
	
    public LogicChecker(String js){
    	Context rhino = Context.enter();
        rhino.setOptimizationLevel(-1);
        
        rhino.setOptimizationLevel(-1);
        scope = rhino.initStandardObjects();

        rhino.evaluateString(scope,js , "funtionCheckin", 0, null);
        function = rhino.compileFunction(scope, preCheckin, "preCheckin", 1, null);	
		
        Context.exit();

    }
    
	public boolean checkin(CodeDataDetail codeData) throws LogicException{
		if(codeData==null) return false;
		
		String json = new Gson().toJson(codeData);
		boolean result = callScript(json);
		return result;
	}
	
	private boolean callScript(String json) throws LogicException{
		try{
			Context rhino = Context.enter();
	        Object result = function.call(rhino, scope, scope,  new Object[]{json});
	        Context.exit();
			return (boolean)result;
		}catch(Exception e){
	    	throw new LogicException(e);
	    }
	}
}
