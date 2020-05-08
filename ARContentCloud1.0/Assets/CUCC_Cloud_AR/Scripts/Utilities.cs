using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Utilities : MonoBehaviour {

    public static void RotateSelf(GameObject self, float speed )
    {
        self.transform.RotateAroundLocal(Vector3.up, speed);
    }

    public struct ButtonNames {
        public const string TEXT_INFO = "TextInfo";
        public const string IMAGE_INFO = "ImageHeader";
        public const string VIDEO_INFO = "VideoPanel";
        public const string MODEL_INFO = "ModelInfo";

        public const string INFO_BACK = "InfoBack";
        public const string QUIT = "Quit";
        public const string QuitView = "QuitView";
    
    }

    public struct ButtonNames_onlineMuseum
    {
        public const string NEXT_SLOT = "NextSpot";
        public const string VisualView = "VisualView";
        public const string TimeLineView = "TimeLineView";
        public const string LocationalView = "LocationalView";
        public const string ScaleUp = "ScaleUp";
        public const string ScaleDown = "ScaleDown";
        public const string TimelineInfo = "TimelineInfo(Clone)";
        public const string LocationalInfo = "LocationalItem(Clone)";

    }
    [Serializable]
    public struct ItemInfo {
        public string name;
        //public string ModelUrl;
        public GameObject TempModel;
        public string TextInfo;
        public string VideoUrl;
        //public string ImageUrl;
        public Image Temp;
        public Texture2D TempTexture;

        public int Year;
        public int month;
        public int day;
        public string title;
        public int locationID;
        
    }

}






