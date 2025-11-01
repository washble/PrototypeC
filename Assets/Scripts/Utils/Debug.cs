//#if UNITY_EDITOR 
//#define DEBUG
//#endif

using UnityEngine;
using System;
using Object = UnityEngine.Object;

public static class Debug 
{
    public static bool isDebugBuild
    {
	    get { return UnityEngine.Debug.isDebugBuild; }
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log (object message)
    {   
        UnityEngine.Debug.Log (message);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log (object message, Object context)
    {   
        UnityEngine.Debug.Log (message, context);
    }
		
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogError (object message)
    {   
        UnityEngine.Debug.LogError (message);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]	
    public static void LogError (object message, Object context)
    {   
        UnityEngine.Debug.LogError (message, context);
    }
 
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogWarning (object message)
    {   
        UnityEngine.Debug.LogWarning (message);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")] 
    public static void LogWarning (object message, Object context)
    {   
        UnityEngine.Debug.LogWarning (message, context);
    }
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")] 
    public static void LogFormat (string format, params object[] args)
    {   
        UnityEngine.Debug.LogFormat(format, args);
    }
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")] 
    public static void LogFormat (Object context, string format, params object[] args)
    {   
        UnityEngine.Debug.LogFormat(context, format, args);
    }
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")] 
    public static void LogWarningFormat (string format, params object[] args)
    {   
        UnityEngine.Debug.LogWarningFormat (format, args);
    }
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")] 
    public static void LogWarningFormat (Object context, string format, params object[] args)
    {   
        UnityEngine.Debug.LogWarningFormat (context, format, args);
    }
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")] 
    public static void LogErrorFormat (string format, params object[] args)
    {   
        UnityEngine.Debug.LogErrorFormat (format, args);
    }
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")] 
    public static void LogErrorFormat (Object context, string format, params object[] args)
    {   
        UnityEngine.Debug.LogErrorFormat (context, format, args);
    }
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")] 
    public static void LogException (Exception exception)
    {   
        UnityEngine.Debug.LogException(exception);
    }
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")] 
    public static void LogException (Exception exception, Object context)
    {   
        UnityEngine.Debug.LogException(exception);
    }
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")] 
    public static void Assert (bool condition, Object context)
    {   
        UnityEngine.Debug.Assert(condition, context);
    }
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")] 
    public static void Assert (bool condition, object message)
    {   
        UnityEngine.Debug.Assert(condition, message);
    }
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")] 
    public static void Assert (bool condition, string format)
    {   
        UnityEngine.Debug.Assert(condition, format);
    } 
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")] 
    public static void AssertFormat (bool condition, string format, params object[] args)
    {   
        UnityEngine.Debug.AssertFormat(condition, format, args);
    }
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")] 
    public static void AssertFormat (
        bool condition,
        Object context,
        string format,
        params object[] args)
    {   
        UnityEngine.Debug.AssertFormat(condition, context, format, args);
    }
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")] 
    public static void DebugBreak()
    {
        UnityEngine.Debug.DebugBreak();
    }
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")] 
    public static void DrawLine(Vector3 start, Vector3 end, Color color = default(Color), float duration = 0.0f, bool depthTest = true)
    {
 	    UnityEngine.Debug.DrawLine(start, end, color, duration, depthTest);
    }
	
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 dir, Color color = default(Color), float duration = 0.0f, bool depthTest = true)
    {
	    UnityEngine.Debug.DrawRay(start, dir, color, duration, depthTest);
    }
 	
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Assert(bool condition)
    {
	    if (!condition) throw new Exception();
    }

    public static class logger
    {
        [System.Diagnostics.Conditional("UNITY_EDITOR")] 
        public static void Log (LogType logType, string tag, object message)
        {   
            UnityEngine.Debug.unityLogger.Log(logType, tag, message);
        }
        
        [System.Diagnostics.Conditional("UNITY_EDITOR")] 
        public static void Log (LogType logType, object message, Object context)
        {   
            UnityEngine.Debug.unityLogger.Log(logType, message, context);
        }
    }
}