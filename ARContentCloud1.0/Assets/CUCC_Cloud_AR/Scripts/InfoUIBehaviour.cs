using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InfoUIBehaviour : UIBehaviour {
    public GameObject TextPanel;
    public GameObject ImagePanel;
    public GameObject VideoPanel;
    public GameObject ModelPanel;
    public DemoUIBehaviour DemoUI;
    // Use this for initialization
    void Start() {
        base.RegisterEvents();

    }

    // Update is called once per frame
    void Update() {

    }

    public void SetTextInfo(string info, GameObject Sender)
    {
        TextPanel.SetActive(true);
        ImagePanel.SetActive(false);
        VideoPanel.SetActive(false);
        ModelPanel.SetActive(false);
        TextPanel.GetComponentInChildren<Text>().text = info;
        Sender.SetActive(false);
    }
    public void SetImageInfo(Sprite info, GameObject Sender)
    {
        TextPanel.SetActive(false);
        ImagePanel.SetActive(true);
        VideoPanel.SetActive(false);
        ModelPanel.SetActive(false);
        Debug.Log(info.name);
        ImagePanel.transform.GetChild(0).GetComponent<Image>().sprite = info;
        Sender.SetActive(false);
    }
    public void SetVideoInfo(GameObject Sender)
    {
        TextPanel.SetActive(false);
        ImagePanel.SetActive(false);
        VideoPanel.SetActive(true);
        ModelPanel.SetActive(false);
        Sender.SetActive(false);
    }
    public void SetModelInfo(GameObject Sender)
    {
        TextPanel.SetActive(false);
        ImagePanel.SetActive(false);
        VideoPanel.SetActive(false);
        ModelPanel.SetActive(true);
        Sender.SetActive(false);
    }

    public override void onButtonClick(GameObject go)
    {
        switch (go.name)
        {
            case Utilities.ButtonNames.INFO_BACK:
                DemoUI.gameObject.SetActive(true);
                gameObject.SetActive(false);
                break;

        }
    }
}
