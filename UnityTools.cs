/****************************************************
	文件：UnityTools.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/10 21:58   	
	功能：Unity相关功能封装
*****************************************************/
#if _CLIENTLOGIC_
using UnityEngine;
#endif
using System.Collections;
using System.Collections.Generic;

public class UnityTools
{
    public static string playerPrefsGetString(string key)
    {
#if _CLIENTLOGIC_
        return PlayerPrefs.GetString(key);
#else
        return "";
#endif
    }

    public static void playerPrefsSetString(string key, string value)
    {
#if _CLIENTLOGIC_
        PlayerPrefs.SetString(key,value);
#endif
    }

    public static void setTimeScale(float value)
    {
#if _CLIENTLOGIC_
        Time.timeScale = value;
#endif
    }

    public static void Log(object message)
    {
#if _CLIENTLOGIC_
        UnityEngine.Debug.Log(message);
#else
        System.Console.WriteLine(message);
#endif
    }

    public static void LogError(object message)
    {
#if _CLIENTLOGIC_
		Debug.LogError(message);
#endif
    }

}