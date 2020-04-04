using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SplashUI : UIBehaviour {

    // Use this for initialization
    void Start() {
        base.RegisterEvents();
    }

    // Update is called once per frame
    void Update() {

    }

    public override void onButtonClick(GameObject go)
    {
        switch (go.name)
        {
            case Utilities.ButtonNames.QUIT:
                Application.Quit();
                break;
            case Utilities.ButtonNames_onlineMuseum.VisualView:
                SceneManager.LoadSceneAsync("MuseumOnline3DView");
                break;
            case Utilities.ButtonNames_onlineMuseum.TimeLineView:
                break;
            case Utilities.ButtonNames_onlineMuseum.LocationalView:
                break;
        }
    }
}
