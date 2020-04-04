using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MemberType
{
Year,
month,
day,
hours
}

public class TimeLineMemberBehaviour : MonoBehaviour {

    public MemberType mType;
    public GameObject MemberPrefab;
    public RectTransform ScaleReferance;
    public bool Initialized;
    public int StartYear;
    public int CurrentYear;
    public int CurrentMonth;
    private GameObject temp;
    private float scaleFactor = 3000f;
    private float resetFactor = 2100f;
	// Use this for initialization
	void Start () {
        switch (mType)
        {
            case MemberType.Year:
                resetFactor = 2100f;
                scaleFactor = 4000f;
            
               TimelineUIBehaviour.mInstance.AddContents(StartYear, transform.GetChild(0).gameObject);
                break;
            case MemberType.month:
                resetFactor = 10000f;
                scaleFactor = 20000f;
                break;
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (ScaleReferance.rect.width > scaleFactor && !Initialized)
        {
            
            init();
           
        }
        else if(ScaleReferance.rect.width <= resetFactor)
        {
            
            Reset();
        }

	}

    private void Reset()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
        switch (mType)
        {
            case MemberType.Year:
                TimelineUIBehaviour.mInstance.updateItemPositionByStartYear(StartYear, transform.GetChild(0));
                break;
            case MemberType.month:
                TimelineUIBehaviour.mInstance.updateItemPositionByYear(CurrentYear, transform.GetChild(0));
                break;
        }
        Initialized = false;

    }

    private void init()
    {
        switch (mType)
        {
            case MemberType.Year:
              
                if (transform.childCount> 2)
                {
                    foreach (Transform child in transform)
                    {
                        child.gameObject.SetActive(true);
                        if(child.GetComponent<TimeLineMemberBehaviour>())
                        TimelineUIBehaviour.mInstance.updateItemPositionByYear(child.GetComponent<TimeLineMemberBehaviour>().CurrentYear, child);
                    }
                       
                    transform.GetChild(0).gameObject.SetActive(false);
                  
                        
                    Initialized = true;
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    for (int i = 0; i < 10; i++)
                    {
                        if (MemberPrefab)
                        {
                            temp = Instantiate(MemberPrefab, transform);
                            temp.GetComponentInChildren<Text>().text = (StartYear + i).ToString();
                            temp.GetComponent<TimeLineMemberBehaviour>().CurrentYear = StartYear + i;
                            temp.GetComponent<TimeLineMemberBehaviour>().ScaleReferance = ScaleReferance;
                            TimelineUIBehaviour.mInstance.updateItemPositionByYear(StartYear + i, temp.transform.GetChild(0));
                        }
                    }
               
                    Initialized = true;
                }
               
                break;
            case MemberType.month:
                if (transform.childCount > 2)
                {
                    foreach (Transform child in transform)
                    {
                        child.gameObject.SetActive(true);
                        if (child.GetComponent<TimeLineMemberBehaviour>())
                            TimelineUIBehaviour.mInstance.updateItemPositionByMonth(CurrentYear, child.GetComponent<TimeLineMemberBehaviour>().CurrentMonth, child);
                    }
                       
                    transform.GetChild(0).gameObject.SetActive(false);
                  
                      
                    Initialized = true;
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    for (int i = 0; i < 12; i++)
                    {
                        if (MemberPrefab)
                        {
                            temp = Instantiate(MemberPrefab, transform);
                            temp.GetComponentInChildren<Text>().text = (i+1).ToString();
                            temp.GetComponent<TimeLineMemberBehaviour>().CurrentMonth = i + 1;
                            temp.GetComponent<TimeLineMemberBehaviour>().ScaleReferance = ScaleReferance;
                            TimelineUIBehaviour.mInstance.updateItemPositionByMonth(CurrentYear,i + 1, temp.transform.GetChild(0));
                        }
                    }
                 
                    Initialized = true;
                }
                break;
            case MemberType.day:
                break;
            case MemberType.hours:
                break;
        }
    }
}
