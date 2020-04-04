using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    //用户交互父类 用于注册信息
    public class UserInput : MonoBehaviour
    {
        
        protected void RegisterEvents()
        {          
            UIEventListener.Get(gameObject).onClick = onPointerClick;                   
            //AssetManager.mInstance.RegisterUIObject(gameObject);           
        }

        protected void RemoveEvents()
        {
            UIEventListener.Get(gameObject).onClick = null;
            //AssetManager.mInstance.RemoveUIObject(gameObject);
        }


        public virtual void onPointerClick(GameObject go)
        {
          

        }
    }


