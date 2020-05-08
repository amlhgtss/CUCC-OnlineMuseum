using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
public class WebInterfaceData  {

	
}

public class CustomUtilites
{
    //读取文档
    public static string LoadStreamAssetFile(string filePath)
    {
        string url = Application.streamingAssetsPath + "/" + filePath;
#if UNITY_EDITOR
        Debug.Log(File.ReadAllText(url));
        return File.ReadAllText(url);
#elif UNITY_ANDROID
 		    UnityWebRequest a = UnityWebRequest.Get(url);
 		    a.SendWebRequest();
 		    while (!a.downloadHandler.isDone) { }
 		    return a.downloadHandler.text;
#endif
    }

    public static string GetRandomKey()
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
        long t = (System.DateTime.Now.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位     
        
        return t.ToString();
    }

    public static Texture2D Base64StringToTexture(string base64Str)
    {
        try
        {
            //将base64头部信息替换
            base64Str = base64Str.Replace("data:image/png;base64,", "").Replace("data:image/jgp;base64,", "")
                .Replace("data:image/jpg;base64,", "").Replace("data:image/jpeg;base64,", "");
            byte[] bytes = Convert.FromBase64String(base64Str);
            Texture2D texture = new Texture2D(10, 10);
            texture.LoadImage(bytes);
            return texture;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}

//本地存储的参数反序列化结构
[System.Serializable]
public class Config
{  
    public string Status;
    public int OS;
    public string Url_Base;
    public string Url_Get_Code;
    public string Url_Login;
}
//server response
[System.Serializable]
public class CodeResponse
{
    public bool success;
    public string message;
    public string code;
    public string timestamp;
    public string result;

}

[System.Serializable]
public class LoginResponse
{
    public bool success;
    public string message;
    public string code;
    public LoginResult result;
    public string timestamp;
}
[System.Serializable]
public class LoginResult
{
    public string multi_depart;
    public UserInfo userInfo;
    public Departs departs;
    public string token;
}

public class UserInfo
{

}

public class Departs
{

}

