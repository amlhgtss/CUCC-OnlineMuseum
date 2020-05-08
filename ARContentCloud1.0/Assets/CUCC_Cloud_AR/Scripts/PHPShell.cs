using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using UnityEngine;
using LitJson;
using UnityEngine.Networking;
using System.Text;

//aa
/// <summary>
/// php connection shell , enable unity comunitcate with php to post request, get stuff , put bytes to and download different type files
/// <param name="url">请求地址</param>
/// <param name="actionResult">回调结果委托，处理请求对象</param>
/// usage of IMultipartFormSection
/// List<IMultipartFormSection> formData = new List<IMultipartFormSection>(); 
/// formData.Add(new MultipartFormDataSection("field1=foo&field2=bar")); 
/// formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));
/// </summary>

public class phpShell : MonoBehaviour {

    static phpShell instance;


    public static phpShell Instance
    {
        get
        {
            if (instance == null) {
                Debug.Log("cannot find webrequest gameobject, create one for you");
                GameObject go = new GameObject("phpShell");
                instance = go.AddComponent<phpShell>();
            }
            return instance;
        }

    }
    #region request

    private void Awake()
    {
        instance = this;
    }


    public void Get(string url, Action<UnityWebRequest> actionResult)
    {
        StartCoroutine(_Get(url, actionResult));
    }

    public void Get(string url, Dictionary<string, string> dicList, Action<UnityWebRequest> actionResult)
    {
        Debug.Log("get" + url);
        StartCoroutine(_Get(url, dicList,actionResult));
    }

    public void GetWithData(string url, Dictionary<string, string> data, Dictionary<string, string> dicList, Action<UnityWebRequest> actionResult)
    {
        Debug.Log("get" + url);
        StringBuilder builder = new StringBuilder();
        builder.Append(url);
        if (data.Count > 0)
        {
            builder.Append("?");
            int i = 0;
            foreach (var item in data)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
        }
        Debug.Log("get" + url);
        StartCoroutine(_Get(url, dicList, actionResult));
    }

    public void Post(string serverUrl, List<IMultipartFormSection> listformData, Action<UnityWebRequest> actionResult)
    {
        StartCoroutine(_Post(serverUrl, listformData, actionResult));
    }

	public void Post(string serverUrl, WWWForm listformData, Action<UnityWebRequest> actionResult)
	{
		StartCoroutine(_Post(serverUrl, listformData, actionResult));
	}

    public void PostJson(string serverUrl, JsonData postJason, Action<UnityWebRequest> actionResult)
    {
        Debug.Log("post" + serverUrl);
        StartCoroutine(_PostJson(serverUrl, postJason, actionResult));
    }

	public void PostJson(string serverUrl, JsonData postJason, Action<UnityWebRequest> actionResult,Dictionary<string,string> dicList)
	{
		StartCoroutine(_PostJson(serverUrl, postJason, actionResult,dicList));
	}

    public void PostJson(string serverUrl, JsonData postJason, Action<UnityWebRequest> actionResult,string token)
    {
        StartCoroutine(_PostJson(serverUrl, postJason, actionResult,token));
    }

    public void Put(string url, byte[] contentBytes, Action<bool> actionResult)
    {
        StartCoroutine(_Put(url, contentBytes, actionResult, ""));
    }

    public void DownloadFile(string url ,string downloadFilePathAndName, Action<UnityWebRequest> actionResult)
    {
        StartCoroutine(_DownloadFile(url, downloadFilePathAndName, actionResult));
    }

    public void GetTexture(string url, Action<Texture2D> actionResult)
    {
        StartCoroutine(_GetTexture(url, actionResult));
    }
    public void GetTexture(string url, Action<Texture2D,int> actionResult,int refference)
    {
        StartCoroutine(_GetTexture(url, actionResult, refference));
    }

    public void GetAssetBundle(string url, Action<AssetBundle> actionResult)
    {
        StartCoroutine(_GetAssetBundle(url, actionResult));
    }

    public void GetAudioClip(string url, Action<AudioClip> actionResult, AudioType audioType = AudioType.WAV)
    {
        StartCoroutine(_GetAudioClip(url, actionResult, audioType));
    }

    

   
    #endregion;

    #region coroutine fuctions
    IEnumerator _Get(string url, Action<UnityWebRequest> actionResult)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if(actionResult!=null)
            {
                actionResult(request);
            }
        }
    }

    IEnumerator _Get(string url,Dictionary<string ,string> dicList, Action<UnityWebRequest> actionResult)
    {
       
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            foreach (KeyValuePair<string, string> kvp in dicList)
            {
                Debug.Log(kvp.Key);
                Debug.Log(kvp.Value);
                request.SetRequestHeader(kvp.Key, kvp.Value);
            }
            yield return request.SendWebRequest();
        
            if (actionResult != null)
            {
                actionResult(request);
            }
        }
    }

    



    IEnumerator _GetAudioClip(string url, Action<AudioClip> actionResult, AudioType audioType = AudioType.WAV)
    {
        using (var request = UnityWebRequestMultimedia.GetAudioClip(url,audioType) ) {
            yield return request.SendWebRequest();
            if (!(request.isNetworkError||request.isHttpError))
            {
                if (actionResult != null)
                {
                    actionResult(DownloadHandlerAudioClip.GetContent(request));
                }
            }
        }
    }

    IEnumerator _Post(string url, List<IMultipartFormSection> listFormData, Action<UnityWebRequest> actionResult)
    {
        UnityWebRequest request = UnityWebRequest.Post(url, listFormData);     
        yield return request.SendWebRequest();
        if (actionResult != null)
        {
            actionResult(request);
        }

    }

	IEnumerator _Post(string url, WWWForm listFormData, Action<UnityWebRequest> actionResult)
	{
		UnityWebRequest request = UnityWebRequest.Post(url, listFormData);     
		yield return request.SendWebRequest();
		if (actionResult != null)
		{
			actionResult(request);
		}

	}

    IEnumerator _PostJson(string url, JsonData jD, Action<UnityWebRequest> actionResult)
    {
        UnityWebRequest request = UnityWebRequest.Post(url, "POST");
        if (jD!= null)
        {
            byte[] postBytes = System.Text.Encoding.Default.GetBytes(jD.ToJson());
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(postBytes);
        }  
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        if (actionResult != null)
        {
            actionResult(request);
        }
    }

	IEnumerator _PostJson(string url, JsonData jD, Action<UnityWebRequest> actionResult,Dictionary<string, string> dicList)
	{
		UnityWebRequest request = UnityWebRequest.Post(url, "POST");
		if (jD!= null)
		{
			byte[] postBytes = System.Text.Encoding.Default.GetBytes(jD.ToJson());
			request.uploadHandler = (UploadHandler)new UploadHandlerRaw(postBytes);
		}  
		request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");

		foreach (KeyValuePair<string, string> kvp in dicList)
		{
            Debug.Log(kvp.Key);
            Debug.Log(kvp.Value);
            request.SetRequestHeader(kvp.Key, kvp.Value);
		}

		yield return request.SendWebRequest();
		if (actionResult != null)
		{
			actionResult(request);
		}
	}


    IEnumerator _PostJson(string url, JsonData jD, Action<UnityWebRequest> actionResult, string token)
    {
        UnityWebRequest request = UnityWebRequest.Post(url, "POST");
        if (jD != null)
        {
            byte[] postBytes = System.Text.Encoding.Default.GetBytes(jD.ToJson());
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(postBytes);
        }
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", token);
        Debug.Log(request.GetRequestHeader("Authorization"));
        yield return request.SendWebRequest();
        if (actionResult != null)
        {
            actionResult(request);
        }
    }

    IEnumerator _Put(string url,byte[] contentBytes, Action<bool> actionResult,string contentType = "application/octet-stream")
    {
        UnityWebRequest request = new UnityWebRequest();
        UploadHandler uploader = new UploadHandlerRaw(contentBytes);
        uploader.contentType = contentType;
        yield return request.SendWebRequest();
        bool result = true;
        if (request.isNetworkError || request.isHttpError)
        {
            result = false;
        }
        if (actionResult != null)
        {
            actionResult(result);
        }       
    }

    IEnumerator _DownloadFile(string url, string downloadFilePathAndName, Action<UnityWebRequest> actionResult)
    {
        var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
        request.downloadHandler = new DownloadHandlerFile(downloadFilePathAndName);
        yield return request.SendWebRequest();
        if (actionResult != null)
        {
            actionResult(request);
        }
    }

    IEnumerator _GetAssetBundle(string url, Action<AssetBundle> actionResult)
    {
        UnityWebRequest request = new UnityWebRequest(url);
        DownloadHandlerAssetBundle handler = new DownloadHandlerAssetBundle(request.url, uint.MaxValue);
        request.downloadHandler = handler;
        yield return request.SendWebRequest();
        AssetBundle bundle = null;
        if (!(request.isNetworkError || request.isHttpError))
        {
            bundle = handler.assetBundle;
        }
        if (actionResult != null)
        {
            actionResult(bundle);
        }

    }

    IEnumerator _GetTexture(string url, Action<Texture2D> actionResult)
    {
        Debug.Log("get image from " + url);
        UnityWebRequest request = new UnityWebRequest(url);
        DownloadHandlerTexture downloadTexture = new DownloadHandlerTexture(true);
        request.downloadHandler = downloadTexture;
        yield return request.SendWebRequest();
        Texture2D texture = null;
        if (!(request.isNetworkError || request.isHttpError))
        {
            texture = downloadTexture.texture;
        }
        if (actionResult != null)
        {
            actionResult(texture);
        }
    }

     IEnumerator _GetTexture(string url, Action<Texture2D,int> actionResult,int refference)
    {
        UnityWebRequest request = new UnityWebRequest(url);
        DownloadHandlerTexture downloadTexture = new DownloadHandlerTexture(true);
        request.downloadHandler = downloadTexture;
        yield return request.SendWebRequest();
        Texture2D texture = null;
        if (!(request.isNetworkError || request.isHttpError))
        {
            texture = downloadTexture.texture;
        }
        if (actionResult != null)
        {
            actionResult(texture, refference);
        }
    }

    #endregion





    /*  #region temp use functions

      public string PHPURL = "shop.letinvr.com/api/";

      public ulong UserID = 0;
      [DllImport("__Internal")]
      private static extern string GetPHPList();

      public void onRequestAutority()
      {
          if (UserID == 0)
          {
              Debug.Log("client userId is empty");
              return;
          }
          StartCoroutine(RequestUserAuthority(""));
      }

      /// <summary>
      /// 获取PHP鉴权, temp use want replace it by sealed fucntions later.
      /// </summary>
      /// <returns></returns>
      IEnumerator RequestUserAuthority(string id)
      {
          var request = new UnityWebRequest(PHPURL + "login", "POST");

          JsonData data = new JsonData();

          // temperary hard code here, will replace it later from aplication installed on cvr devises
          data["mac_code"] = "M18234070288_mm-mm-mm-mm";

          byte[] postBytes = System.Text.Encoding.Default.GetBytes(data.ToJson());

          request.uploadHandler = (UploadHandler)new UploadHandlerRaw(postBytes);
          request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

          yield return request.Send();//.SendWebRequest();

          Debug.Log("Status Code" + request.responseCode);
          Debug.Log(request.downloadHandler.text);

      }

      public void RequestList()
      {
          Debug.Log(GetPHPList());
      }
      #endregion*/




}
