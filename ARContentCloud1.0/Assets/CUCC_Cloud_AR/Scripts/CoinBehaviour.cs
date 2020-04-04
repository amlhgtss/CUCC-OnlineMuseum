using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinBehaviour : MonoBehaviour {

    public Vector3 ResizeFactor ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Utilities.RotateSelf(gameObject,.02f);

    }

    public void UpdateSize(Slider slider) {
        transform.localScale = (1+ slider.value*2)* ResizeFactor;
    }
}
