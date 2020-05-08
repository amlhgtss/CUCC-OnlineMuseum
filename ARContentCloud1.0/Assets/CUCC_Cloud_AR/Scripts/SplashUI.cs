using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SplashUI : UIBehaviour {

    public string[] SceneNames;

    // Use this for initialization
    void Start() {
        base.RegisterEvents();
    }
    private void OnDestroy()
    {
        base.RemoveEvents();
    }
    // Update is called once per frame
    void Update() {

    }

    public override void onButtonClick(GameObject go)
    {
        switch (go.name)
        {
            case Utilities.ButtonNames.QUIT:
                Debug.Log("Quit app");
                Application.Quit();
                break;
            case Utilities.ButtonNames_onlineMuseum.VisualView:
               // SceneManager.LoadScene("MuseumOnline3DView");
                SceneManager.LoadScene(SceneNames[0]);
                break;
            case Utilities.ButtonNames_onlineMuseum.TimeLineView:
                //SceneManager.LoadScene("MuseumOnlineTimeLineView");
                SceneManager.LoadScene(SceneNames[1]);
                break;
            case Utilities.ButtonNames_onlineMuseum.LocationalView:
                //SceneManager.LoadScene("MuseumOnlineLocationalView");
                SceneManager.LoadScene(SceneNames[2]);
                break;
        }
    }
}
