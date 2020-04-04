using UnityEngine;

/**
 * 鼠标点击事件绑定
 */
public class Click : MonoBehaviour
{
    private Ray _ray;//物理射线相关

    public RaycastHit _hit;//物理射线相关

    private bool _first = true;//新一轮标识（或者也可以叫是否结束的标识）

    private bool _flag = true;//单击或双击的标识（默认单击）

    private void Update()
    {
        monitor();
    }

    /**
     * 鼠标单、双击监听
     */
    private void monitor()
    {
        //触发鼠标左键点击
        if (!Input.GetMouseButtonDown(0)) return;
      
        //射线检测到的对象是当前对象
        if (Camera.main != null) _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(_ray, out _hit) || _hit.collider.gameObject != gameObject)
        {
          
                  return;
        }
          

        _flag = !_flag;

        //上一次的事件是否已经执行完毕，也就是判断是否为新一轮
     
        if (!_first) return;

        _first = false;

        //初始化定时器，300毫秒后执行预定方法
        Invoke("Timer", 0.3f);
    }

    /**
     * 定时调用函数
     */
    private void Timer()
    {
    
        //进行判断
        if (_flag)
        {
            OnDblclick();
        }
        else
        {
            OnClick();
        }

        //定时调用结束，重置标识
        _first = true;
        _flag = true;
    }

    /**
     * 单击事件
     */
    private void OnClick()
    {
        //Debug.Log(gameObject.name + "单击事件被触发");
       
    }

    /**
     * 双击事件
     */
    private void OnDblclick()
    {
        //Debug.Log(gameObject.name + "双击事件被触发");
        OnlineMuseumUI.mInstance.on3DModelClick(gameObject);
       
    }
}