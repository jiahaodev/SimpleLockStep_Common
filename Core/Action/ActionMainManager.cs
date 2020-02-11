/****************************************************
	文件：ActionMainManager.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/10 23:06   	
	功能：事件管理主类（管理所有的ActionManager实例）
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

public class ActionMainManager
{
    List<ActionManager> m_listManager = new List<ActionManager>();

    //更新逻辑
    public void updateLogic()
    {
        for (int i = 0; i < m_listManager.Count; i++)
        {
            if (m_listManager[i].enable)
            {
                m_listManager[i].updateLogic();
            }
        }

        for (int i = m_listManager.Count - 1; i >= 0; i--)
        {
            if (!m_listManager[i].enable)
            {
                m_listManager.RemoveAt(i);
            }
        }
    }

    public void addActionManager(ActionManager actionManager) {
        m_listManager.Add(actionManager);
    }

    public void removeActionManager(ActionManager actionManager) {
        actionManager.enable = false;
    }

    public void release() {
        m_listManager.Clear();
    }

}

