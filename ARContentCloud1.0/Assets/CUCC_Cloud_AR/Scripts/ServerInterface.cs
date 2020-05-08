using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ServerInterface : MonoBehaviour {
    public RawImage codeimage;
    public InputField UserName;
    public InputField Password;
    public InputField Code;
    public GameObject LoginPanel;
    private string timeStamp;
    /// <summary>
    /// 创建短连接header
    /// </summary>
    /// <returns></returns>
    private Dictionary<string, string> CreateHeader()
    {
        Dictionary<string, string> header = new Dictionary<string, string>();
        
      //  header.Add("Content-Type", "application/json");
        header.Add("os", AppConfig.OS);
        if(AppConfig.X_Access_Token!="")
        header.Add("X-Access-Token", AppConfig.X_Access_Token);
       
        return header;

    }

    private void Start()
    {
        UserName.onEndEdit.AddListener(setUserName);
        Password.onEndEdit.AddListener(setPassword);
        Code.onEndEdit.AddListener(setCode);
    }

    private void Update()
    {
       
    }

    #region login 
    public void getCode()
    {
        timeStamp = CustomUtilites.GetRandomKey();
        string url = AppConfig.ServerUrl + AppConfig.Command_GetCode + timeStamp;
        Debug.Log("try get from " + url);
        phpShell.Instance.Get(url, CreateHeader(), onCodeResult);
    }
    public void setUserName(string name)
    {
        AppConfig.UserName = name;
        Debug.Log(AppConfig.UserName);
    }
    public void setPassword(string psw)
    {
        AppConfig.Password = psw;
    }
    public void setCode(string code)
    {
        AppConfig.Code = code;
    }
    private void showCodeImage(string image)
    {
        Debug.Log(image);
        codeimage.texture = CustomUtilites.Base64StringToTexture(image);
        //"data:image/jpg;base64,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");
    }
    public void Login()
    {
        Dictionary<string, string> header = CreateHeader();
        JsonData data = new JsonData();
        data["captcha"] = AppConfig.Code;
        Debug.Log(AppConfig.Code);
        data["checkKey"] = timeStamp;// CustomUtilites.GetRandomKey();
        data["password"] = AppConfig.Password;
        data["username"] = AppConfig.UserName;
        Debug.Log(data.ToJson());
        string url = AppConfig.ServerUrl + AppConfig.Command_Login;
        phpShell.Instance.PostJson(url, data, onLoginResult, header);
    }
        #endregion





    #region webrequest action result
    private void onCodeResult(UnityWebRequest request)
    {
        Debug.Log("call back with " + request.downloadHandler.text);
        CodeResponse response = JsonUtility.FromJson<CodeResponse>(request.downloadHandler.text);
        codeimage.transform.parent.gameObject.SetActive(true);
        if (response.success)
        {
          
                  showCodeImage(response.result);
        }
          
    }
    private void onLoginResult(UnityWebRequest request)
    {
        Debug.Log("call back with " + request.downloadHandler.text);
        LoginResponse response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);
        if (response.success)
        {
            AppConfig.X_Access_Token = response.result.token;
            Debug.Log(AppConfig.X_Access_Token);
            LoginPanel.SetActive(false);
        }

    }
    #endregion


}
