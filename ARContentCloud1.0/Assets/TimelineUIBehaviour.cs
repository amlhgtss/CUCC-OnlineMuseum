using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimelineUIBehaviour : UIBehaviour {
    public RectTransform ScaleRefferance;
    public float ScaleFactor;
    public static TimelineUIBehaviour mInstance;
    public Utilities.ItemInfo[] DemoItems;
    public GameObject TimeLineItem;
    public List<TimelineItem> TimelineItems = new List<TimelineItem>();
    public GameObject InfoPanel;
    public Text InfoText;
    public Image InfoImage;
    public MediaPlayer Player;
    public GameObject TempModel;
    public GameObject MainObj;
    public GameObject Timeline;
    // Use this for initialization
    private void Awake()
    {
        mInstance = this;
    }
    void Start () {
        base.RegisterEvents();

    }
    private void OnDestroy()
    {
        base.RemoveEvents();
    }
    // Update is called once per frame
    void Update () {
       
	}

    public override void onButtonClick(GameObject go)
    {
        switch (go.name)
        {
            case Utilities.ButtonNames_onlineMuseum.ScaleUp:
                onScale(true);
                break;
            case Utilities.ButtonNames_onlineMuseum.ScaleDown:
                onScale(false);
                break;
            case Utilities.ButtonNames_onlineMuseum.TimelineInfo:
                Timeline.SetActive(false);
                InfoPanel.SetActive(true);
                MainObj.SetActive(true);
                Utilities.ItemInfo temp = go.GetComponent<TimelineItem>().mInfo;
                InfoText.text = temp.TextInfo;
                InfoImage.sprite = Sprite.Create(temp.TempTexture, new Rect(0f, 0f, temp.TempTexture.width, temp.TempTexture.height), InfoImage.sprite.pivot);
                if(temp.VideoUrl!="")
                    Player.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, temp.VideoUrl, true);

                TempModel = Instantiate(temp.TempModel, Vector3.zero, temp.TempModel.transform.rotation, MainObj.transform);
                TempModel.transform.localPosition = Vector3.zero;
                TempModel.transform.localRotation = temp.TempModel.transform.rotation;
                break;
            case Utilities.ButtonNames.INFO_BACK:
                Timeline.SetActive(true);
                InfoPanel.SetActive(false);
                MainObj.SetActive(false);
                Destroy(TempModel);
                break;
            case Utilities.ButtonNames.QuitView:
                SceneManager.LoadSceneAsync(0);
                break;

        }
    }

   

    public void AddContents(int year,GameObject target)
    {
        Debug.Log(year);
        foreach (Utilities.ItemInfo info in DemoItems)
        {
            if (info.Year >= year && info.Year <year + 10)
            {
                GameObject temp = Instantiate(TimeLineItem, target.transform);
                TimelineItems.Add(temp.GetComponent<TimelineItem>());
                temp.GetComponent<TimelineItem>().updateInfo(info);
                temp.GetComponent<TimelineItem>().updatePosition(target.transform);
            }
        }
     
    }

    public void updateItemPositionByStartYear(int year, Transform target)
    {
        
        foreach (TimelineItem item in TimelineItems)
        {
            if (item.mInfo.Year >= year && item.mInfo.Year < year + 10)
            {
                item.updatePosition(target.transform);
            }
        }
    }

    public void updateItemPositionByYear(int year, Transform target)
    {
        foreach (TimelineItem item in TimelineItems)
        {
            if (year == item.mInfo.Year)
            {
                item.updatePosition(target.transform);
            }
        }
    }

    public void updateItemPositionByMonth(int year,int month, Transform target)
    {
        foreach (TimelineItem item in TimelineItems)
        {
            if (month == item.mInfo.month && year == item.mInfo.Year)
            {
                item.updatePosition(target.transform);
            }
        }
    }

    private void onScale(bool up)
    {
        //ScaleRefferance.sizeDelta = new Vector2(2000 * (1 + bar.value * 10), ScaleRefferance.rect.height);

        //foreach (TimelineItem t in TimelineItems)
        //{
        //    t.undockItem();
        //}

        if (up && ScaleRefferance.rect.width < 40000)
        {
            ScaleRefferance.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ScaleRefferance.rect.width + ScaleFactor);
            if (ScaleRefferance.rect.width >= 3000)
                ScaleFactor = 3000;
        }
        else if (!up && ScaleRefferance.rect.width >= 2000 + ScaleFactor)
        {
            ScaleRefferance.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ScaleRefferance.rect.width - ScaleFactor);
            if (ScaleRefferance.rect.width <= 2000 + ScaleFactor)
                ScaleFactor = 500;
        }

    }


    
}
