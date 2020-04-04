using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OnlineMuseumUI : UIBehaviour {
    public static OnlineMuseumUI mInstance;
    public Transform[] AvailableSlots;
    public int CurrentSlot = 0;
    public GameObject InfoPanel;
    public GameObject ControlPannel;
    public GameObject ModelObj;
    public GameObject TempModel;
    public Utilities.ItemInfo []  InRoomItemInfos;
    public Text InfoText;
    public Image InfoImage;
    public MediaPlayer Player;
	// Use this for initialization
	void Start () {
        base.RegisterEvents();
    }
	
	// Update is called once per frame
	void Update () {
        OnlineMuseumUI.mInstance = this;

    }

    public void on3DModelClick(GameObject go)
    {
        // Debug.Log(go.name);
        showInfo(go.name, true);
    }

    private void showInfo(string infoName,bool show)
    {
        ControlPannel.SetActive(!show);
        ModelObj.SetActive(show);
        InfoPanel.SetActive(show);
        int index = 0;
        for (int i = 0;i<InRoomItemInfos.Length;i++)
        {
            if (InRoomItemInfos[i].name == infoName)
            {
                index = i;
                break;
            }
        }
        if (!TempModel && show)
        {
            TempModel= Instantiate(InRoomItemInfos[index].TempModel, Vector3.zero, InRoomItemInfos[index].TempModel.transform.rotation, ModelObj.transform);
            TempModel.transform.localPosition = Vector3.zero;
            TempModel.transform.localRotation = InRoomItemInfos[index].TempModel.transform.rotation;
            InfoText.text = InRoomItemInfos[index].TextInfo;
            InfoImage.sprite = Sprite.Create(InRoomItemInfos[index].TempTexture, new Rect(0f, 0f, InRoomItemInfos[index].TempTexture.width, InRoomItemInfos[index].TempTexture.height), InfoImage.sprite.pivot);
           
            Player.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, InRoomItemInfos[index].VideoUrl, true);
        }
        if (!show && TempModel != null)
        {
            Destroy(TempModel);
            Player.m_VideoPath = "";
            Player.Stop();
        }
        
    }

    public override void onButtonClick(GameObject go)
    {
        switch (go.name)
        {
            case Utilities.ButtonNames_onlineMuseum.NEXT_SLOT:
                if (CurrentSlot < AvailableSlots.Length-1)
                {
                    CurrentSlot++;
                }
                else {
                    CurrentSlot = 0;
                }
                Camera.main.transform.SetParent(AvailableSlots[CurrentSlot]);
                Camera.main.transform.localPosition = Vector3.zero;
                break;
            case Utilities.ButtonNames.QUIT:
                Debug.Log("quit app");
                Application.Quit();
                break;
            case Utilities.ButtonNames.INFO_BACK:
                showInfo("", false);
                break;

        }
    }
}
