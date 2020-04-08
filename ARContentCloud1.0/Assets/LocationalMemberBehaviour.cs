using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LocationalMemberBehaviour : MonoBehaviour {
    public Utilities.ItemInfo mInfo;
    public GameObject MainObj;
    public Text Title;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init()
    {
        Title.text = mInfo.title;
        if (mInfo.TempModel)
        {
            Destroy(MainObj.transform.GetChild(0).gameObject);
            GameObject temp = Instantiate(mInfo.TempModel, MainObj.transform);
            temp.transform.localScale = 2000 * temp.transform.localScale;

        }
    }
}
