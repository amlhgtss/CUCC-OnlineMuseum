using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }

    protected void RegisterEvents()
    {
        Debug.Log("add events");
        UIButtons.NormalButtonClick += onButtonClick;
    }

    protected void RemoveEvents()
    {
        Debug.Log("remove events");
        UIButtons.NormalButtonClick -= onButtonClick;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public virtual void onButtonClick(GameObject go)
    {
        Debug.Log(go.name + "@@@@@@@@@@@@@@@@@@@@");
    }


}
