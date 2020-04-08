using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocationalUIBehaviour : UIBehaviour {

    public static LocationalUIBehaviour mInstance;

    public Utilities.ItemInfo[] Items;
    public GameObject[] Locations;
    public GameObject LocationItemPrefab;

    public int StartYear = 1900;
    public int YearRange = 10;
    public int MemberCap = 31;

    public GameObject InfoPanel;
    public Text InfoText;
    public Image InfoImage;
    public MediaPlayer Player;
    public GameObject TempModel;
    public GameObject MainObj;
    public Transform QuitView;

    // Use this for initialization
    void Start () {
        base.RegisterEvents();
        mInstance = this;
        initItem();
    }

    private void OnDestroy()
    {
        base.RemoveEvents();
    }

    private void initItem()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Debug.Log(Items[i].Year);
            int index = findYear(Items[i].Year - Items[i].Year % 10);
            Debug.Log(index);
            GameObject temp = Instantiate(LocationItemPrefab, Locations[Items[i].locationID].transform.GetChild(index));
            temp.GetComponent<LocationalMemberBehaviour>().mInfo = Items[i];
            temp.GetComponent<LocationalMemberBehaviour>().Init();
        }
    }

    private int findYear(int year)
    {
        for (int i = 0; i < MemberCap; i ++)
        {
            if (StartYear + i * YearRange == year)
                return MemberCap - i;
        }
        return -1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void onButtonClick(GameObject go)
    {
        Debug.Log(go.name);
        switch (go.name)
        {
          
            case Utilities.ButtonNames_onlineMuseum.LocationalInfo:
                Debug.Log(1);
                QuitView.gameObject.SetActive(false);
                InfoPanel.SetActive(true);
                MainObj.SetActive(true);
                Utilities.ItemInfo temp = go.GetComponent<LocationalMemberBehaviour>().mInfo;
                Debug.Log(temp.TextInfo);
                InfoText.text = temp.TextInfo;
                InfoImage.sprite = Sprite.Create(temp.TempTexture, new Rect(0f, 0f, temp.TempTexture.width, temp.TempTexture.height), InfoImage.sprite.pivot);
                
                if (temp.VideoUrl != "")
                    Player.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, temp.VideoUrl, true);

                if (temp.TempModel)
                {  // 因为景区无法提供模型临时去掉
                    //TempModel = Instantiate(temp.TempModel, Vector3.zero, temp.TempModel.transform.rotation, MainObj.transform);
                    //TempModel.transform.localPosition = Vector3.zero;
                    //TempModel.transform.localRotation = temp.TempModel.transform.rotation;
                }

                break;
            case Utilities.ButtonNames.INFO_BACK:
                QuitView.gameObject.SetActive(true);
                InfoPanel.SetActive(false);
                MainObj.SetActive(false);
                Destroy(TempModel);
                break;
            case Utilities.ButtonNames.QuitView:
                SceneManager.LoadSceneAsync(0);
                break;

        }
    }
}
