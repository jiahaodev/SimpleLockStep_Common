/****************************************************
	文件：BaseAction.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/10 22:16   	
	功能：Action基类
*****************************************************/

using System;
using System.Collections;

public class BaseAction
{

    public ActionCallback actionCallbackFunc { get; set; }

    private bool m_bEnable = true;
    public bool enable { get { return m_bEnable; } set { m_bEnable = value; } }

    public string name { get; set; }

    public string label { get; set; }

    public BaseObject unit { get; set; }

    public ActionManager manager { get; set; }

    public void removeSelfFromManager()
    {
        if (manager != null)
        {
            manager.removeAction(this);
        }
    }

    public void setBelongToManager(ActionManager manager)
    {
        this.manager = manager;
    }

    public virtual void updateLogic() { }

}