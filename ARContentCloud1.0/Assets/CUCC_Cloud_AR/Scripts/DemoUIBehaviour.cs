using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DemoUIBehaviour : UIBehaviour {

	// Use this for initialization
	void Start () {
        base.RegisterEvents();
	}

    public InfoUIBehaviour InfoUI;
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDisable()
    {
       // base.RemoveEvents();
    }

    public override void onButtonClick(GameObject go)
    {
        switch (go.name)
        {
            case Utilities.ButtonNames.TEXT_INFO:
                InfoUI.gameObject.SetActive(true);
                InfoUI.SetTextInfo(go.GetComponentInChildren<Text>().text, gameObject);
                break;
            case Utilities.ButtonNames.IMAGE_INFO:
                InfoUI.gameObject.SetActive(true);
                InfoUI.SetImageInfo(go.transform.GetChild(0).GetComponent<Image>().sprite, gameObject);
                break;
            case Utilities.ButtonNames.VIDEO_INFO:
                InfoUI.gameObject.SetActive(true);
                InfoUI.SetVideoInfo(gameObject);
                break;
            case Utilities.ButtonNames.MODEL_INFO:
                InfoUI.gameObject.SetActive(true);
                InfoUI.SetModelInfo(gameObject);
                break;


        }
    }
}
