using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppConfig : Singleton<AppConfig>
{
    public Config Config;

    public Scene PreviourScene;
    public Scene CurrentScene;

    public static string OS;
    public static string X_Access_Token;
    public static string ServerUrl;

    public static string Command_GetCode;
    public static string Command_Login;

    public static string UserName;
    public static string Password;
    public static string Code;
    #region delegates
    public delegate void OnModeChangeDelegate(bool mode);
    public static OnModeChangeDelegate ModeChange;
    #endregion
    //public AndriodInfo info;


    // Use this for initialization
    void Start()
    {
        X_Access_Token="";
        Config = JsonUtility.FromJson<Config>(CustomUtilites.LoadStreamAssetFile("Config.txt"));
        OS = Config.OS.ToString();
        ServerUrl = Config.Url_Base;
        Command_GetCode = Config.Url_Get_Code;
        Command_Login = Config.Url_Login;
        SceneManager.activeSceneChanged += OnSecneChanged;
    }

    //public void SetVRmode(bool mode)
    //{
    //    Debug.Log("set confi mode to " + mode);
    //    Config.VRMode = mode;
    //    ModeChange(mode);
    //}

    public bool IsConfigReady()
    {
        if (Config.Status == "Ready")
        {
            return true;
        }
        return false;
    }

    private void OnSecneChanged(Scene old, Scene newScene)
    {
        CurrentScene = newScene;
        PreviourScene = old;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
