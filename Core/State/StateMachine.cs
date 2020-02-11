/****************************************************
	�ļ���StateMachine.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/11 16:10   	
	���ܣ�״̬����
*****************************************************/

using System.Collections;

public class StateMachine
{
    BaseState m_currentState = null;
    string m_scCurrentStateName = "";
    LiveObject m_unit = null;

    //ÿ֡ѭ��
    //��MainLogic��Update�����е��ã��Լ��������
    public void updateLogic()
    {
        if (m_currentState != null)
        {
            m_currentState.updateLogic();
        }
    }

    public void changeState(string state,Fix64 args) {

        exitOldState();

        m_currentState = null;

        //���ݲ�ͬ��״̬������������Ӧ��״̬
        if (state == "towerattack")
        {
            m_currentState = new TowerAttackState();
        }
        else if (state == "towerstand")
        {
            m_currentState = new TowerStandState();
        }
        else if (state == "cooling")
        {
            m_currentState = new CoolingState();
        }
        else if (state == "normal")
        {
            m_currentState = new NormalState();
        }

        //Ϊ�����µ�״̬����׼��
        m_currentState.onInit(m_unit);
        //����֮ǰ��״̬��
        m_currentState.setPrevStateName(m_scCurrentStateName);
        //��¼��ǰ��״̬��
        m_scCurrentStateName = state;
        //ֱ�ӽ����״̬
        m_currentState.onEnter(args);
    }


    public void setPrevStateName(string stateName) {
        m_currentState.setPrevStateName(stateName);
    }

    public string getPrevStateName() {
        return m_currentState.getPrevStateName();
    }

    public string getState() {
        return m_scCurrentStateName;
    }

    //�˳�֮ǰ��״̬
    public void exitOldState() {
        if (m_currentState != null)
        {
            m_currentState.onExit();
        }
    }


    //���������õĵ�Ԫ����
    public void setUnit(LiveObject obj) {
        m_unit = obj;
    }
}
