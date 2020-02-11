/****************************************************
	文件：ActionManager.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/10 22:58   	
	功能：事件管理类
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

public class ActionManager {

    List<BaseAction> m_listAction = new List<BaseAction>();
    public bool m_bEnable = true;
    public bool enable { get { return m_bEnable; } set { m_bEnable = value; } }

    //更新逻辑
    public void updateLogic() {

        for (int i = 0; i < m_listAction.Count; i++)
        {
            if (m_listAction[i].enable)
            {
                m_listAction[i].updateLogic();
            }
        }

        for (int i = m_listAction.Count - 1; i >= 0; i--)
        {
            if (!m_listAction[i].enable)
            {
                m_listAction.RemoveAt(i);
            }
        }
    }


    public void addAction(BaseAction action) {
        m_listAction.Add(action);
        action.manager = this;
    }

    public void removeAction(BaseAction action) {
        action.enable = false;
    }


    public void stopAction(string label) {
        for (int i = m_listAction.Count - 1; i >= 0; i--)
        {
            if (m_listAction[i].label == label)
            {
                m_listAction.RemoveAt(i);
            }
        }
    }

    public void stopActionByName(string name)
    {
        for (int i = m_listAction.Count - 1; i >= 0; i--)
        {
            if (m_listAction[i].name == name)
            {
                m_listAction.RemoveAt(i);
            }
        }
    }

    public void stopAllAction()
    {
        m_listAction.Clear();
    }


}