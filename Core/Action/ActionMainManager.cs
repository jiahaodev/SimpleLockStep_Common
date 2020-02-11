/****************************************************
	�ļ���ActionMainManager.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/10 23:06   	
	���ܣ��¼��������ࣨ�������е�ActionManagerʵ����
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

public class ActionMainManager
{
    List<ActionManager> m_listManager = new List<ActionManager>();

    //�����߼�
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

