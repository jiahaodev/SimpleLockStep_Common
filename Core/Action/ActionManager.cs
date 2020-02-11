/****************************************************
	�ļ���ActionManager.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/10 22:58   	
	���ܣ��¼�������
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

public class ActionManager {

    List<BaseAction> m_listAction = new List<BaseAction>();

    public bool enable { get; set; }

    //�����߼�
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
        for (int i = m_listAction.Count; i >= 0; i--)
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