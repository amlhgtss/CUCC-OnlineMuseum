using UnityEngine;

using System.Collections;
using System.IO;
using UnityEngine.UI;
public class CameraController : MonoBehaviour
{
    public Transform target;

    private float distance = 0f;
    private float _camerafov;
    private float xSpeed = 40f;
    private float ySpeed = 30f;

    private float yMinLimit = -90;
    private float yMaxLimit = 80;

    private float x = 0.0f;
    private float y = 0.0f;
    private float _lerpspeed = 5f;

    private Vector2 oldPosition1 = new Vector2(0, 0);
    private Vector2 oldPosition2 = new Vector2(0, 0);

    public bool CanScale;


    //public Text txt;
    void Start()
    {
        // Debug.Log(oldPosition1);  
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        _camerafov = Camera.main.fieldOfView;
    }

    void Update()
    {
        // 判断触摸数量为单点触摸  
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            }
        }

        if (CanScale && Input.touchCount > 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
            {

                //计算出当前两点触摸点的位置
                Vector2 tempPosition1 = Input.GetTouch(0).position;
                Vector2 tempPosition2 = Input.GetTouch(1).position;

                // 改变相机FOV值 实现放大缩小
                if (isEnlarge(oldPosition1, oldPosition2, tempPosition1, tempPosition2))
                {
                    _camerafov -= 80f * Time.deltaTime;
                    if (_camerafov <= 40f)
                    {
                        _camerafov = 40;

                    }
                    Camera.main.fieldOfView = _camerafov;
                }
                else
                {
                    _camerafov += 80f * Time.deltaTime;

                    if (_camerafov >= 95f)
                    {
                        _camerafov = 95f;
                    }
                    Camera.main.fieldOfView = _camerafov;

                }
                oldPosition1 = tempPosition1;
                oldPosition2 = tempPosition2;

            }
        }
    }

    bool isEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2) // 函数判断放大缩小手势
    {
        // 取平方根
        float leng1 = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        float leng2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));

        if (leng1 < leng2)
        {
            // 放大手势  
            return true;
        }
        else
        {
            // 缩小手势  
            return false;
        }
    }
    void LateUpdate()
    {
        if (target)
        {
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            Quaternion rotation = Quaternion.Euler(y, x, 0);

            //transform.rotation = rotation;

            // 插值运算
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _lerpspeed);

        }

    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -90)
            angle = -90;
        if (angle > 80)
            angle = 80;
        return Mathf.Clamp(angle, min, max);

    }
}

