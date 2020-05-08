//================================================================================================================================
//
//  Copyright (c) 2015-2019 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
//  EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
//  and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
//
//================================================================================================================================

using Common;
using easyar;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SurfaceTracking
{
    public class UIController : MonoBehaviour
    {
        public Text Status;
        public ARSession Session;
        public TouchController TouchControl;
        public UnityEngine.UI.Image ScreenShot;
        private SurfaceTrackerFrameFilter tracker;
        private bool takeShot = false;
        private void Awake()
        {
            tracker = Session.GetComponentInChildren<SurfaceTrackerFrameFilter>();
            TouchControl.TurnOn(TouchControl.transform, Camera.main, false, false, true, true);
        }

        private void Update()
        {
            Status.text = "CenterMode: " + Session.CenterMode + Environment.NewLine +
                Environment.NewLine +
                "Gesture Instruction" + Environment.NewLine +
                "\tMove on Surface: One Finger Move" + Environment.NewLine +
                "\tRotate: Two Finger Horizontal Move" + Environment.NewLine +
                "\tScale: Two Finger Pinch";

            if (Input.touchCount == 1 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                var touch = Input.touches[0];
                if (touch.phase == TouchPhase.Moved)
                {
                    var viewPoint = new Vector2(touch.position.x / Screen.width, touch.position.y / Screen.height);
                    if (tracker && tracker.Tracker != null && Session.FrameCameraParameters.OnSome)
                    {
                        var coord = EasyARController.Instance.Display.ImageCoordinatesFromScreenCoordinates(viewPoint, Session.FrameCameraParameters.Value, Session.Assembly.Camera);
                        tracker.Tracker.alignTargetToCameraImagePoint(coord.ToEasyARVector());
                    }
                }
            }
        }

        private IEnumerator screenShot()
        {
            yield return new WaitForEndOfFrame();
            DateTime now = DateTime.Now;
            string times = now.ToString();
            times = times.Trim();
            times = times.Replace("/", "-");
            string fileName = "ARScreenShot" + times + ".png";

            if (Application.platform == RuntimePlatform.Android)
            {
                ScreenShot.gameObject.SetActive(true);

                Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
                texture.ReadPixels(new Rect(0, 0,  Screen.width, Screen.height), 0, 0);
                texture.Apply();
                byte[] bytes = texture.EncodeToPNG();
                
                ScreenShot.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), ScreenShot.rectTransform.pivot);
                string path = Application.persistentDataPath + "/" + "SaveTextures/";//"/sdcard/Pictures/Screenshots";


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filePath = path + "/" + fileName;
                File.WriteAllBytes(filePath, bytes);

            }
        }



        public void OnScreenShotClick()
        {
            StartCoroutine(screenShot());
        }
        public void SwitchCenterMode()
        {
            while (true)
            {
                Session.CenterMode = (ARSession.ARCenterMode)(((int)Session.CenterMode + 1) % Enum.GetValues(typeof(ARSession.ARCenterMode)).Length);
                if (Session.CenterMode == ARSession.ARCenterMode.Camera ||
                    Session.CenterMode == ARSession.ARCenterMode.WorldRoot)
                {
                    break;
                }
            }
        }
    }
}
