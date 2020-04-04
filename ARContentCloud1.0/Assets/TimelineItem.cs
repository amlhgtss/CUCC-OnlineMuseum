using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimelineItem : MonoBehaviour {
    public Utilities.ItemInfo mInfo;
    public Text mTitle;
    public Image mImage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateInfo(Utilities.ItemInfo info)
    {      
            mInfo = info;
            mTitle.text = info.title;
            mImage.sprite = Sprite.Create(info.TempTexture, new Rect(0, 0, info.TempTexture.width, info.TempTexture.height), mImage.sprite.pivot);    
    }
    public void updatePosition(Transform parent)
    {
        transform.SetParent(parent);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, GetComponent<RectTransform>().anchoredPosition.y);


    }
    public void undockItem()
    {
        if(transform.parent!=transform.root)
        transform.SetParent(transform.root);
    }
}
