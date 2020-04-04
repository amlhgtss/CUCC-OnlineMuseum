
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    /// <summary>
    /// UI 按钮 注册按钮点击代理， 如按钮为栏目按钮 直接负责更新按钮文字 ， todo 后续如按钮种类增多最好以此为父节点参照视频按钮处理栏目按钮
    /// </summary>
    public class UIButtons : UserInput {

        
        
        public Text DisplayText;

        #region ui event delegate 
        //public delegate void OnVedioItemClickDelegate(GameObject go);
        //public static  OnVedioItemClickDelegate VedioItemClick;
        public delegate void OnNormalButtonClickDelegate(GameObject go);
        public static OnNormalButtonClickDelegate NormalButtonClick;
        //public delegate void OnChannelButtonClickDelegate(GameObject go);
        //public static OnChannelButtonClickDelegate ChannelButtonClick;

        #endregion


        void Start()
        {
            base.RegisterEvents();
        }
        //点击代理
        public override void onPointerClick(GameObject go)
        {
            base.onPointerClick(go);
            Debug.Log("user clicked gameobject " + go);
            //if (go.name.Contains("VideoItem"))
            //    VedioItemClick(go);
            //else if (go.name.Contains("CHToggle"))
            //    ChannelButtonClick(go);
            //else
                NormalButtonClick(go);

        }


        public void updateButtonImage(Sprite image)
        {
            GetComponent<Image>().sprite = image;
        }
        // 栏目按钮更细文字
        public void updateCatagoryText()
        {
            
        }
    }
