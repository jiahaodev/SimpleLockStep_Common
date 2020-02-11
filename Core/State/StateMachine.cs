/****************************************************
	文件：StateMachine.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 16:10   	
	功能：状态机类
*****************************************************/

using System.Collections;

public class StateMachine
{
    BaseState m_currentState = null;
    string m_scCurrentStateName = "";
    LiveObject m_unit = null;

    //每帧循环
    //由MainLogic的Update来进行调用，自己不会调用
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

        //根据不同的状态参数，创建对应的状态
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

        //为创建新的状态做好准备
        m_currentState.onInit(m_unit);
        //设置之前的状态名
        m_currentState.setPrevStateName(m_scCurrentStateName);
        //记录当前的状态名
        m_scCurrentStateName = state;
        //直接进入该状态
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

    //退出之前的状态
    public void exitOldState() {
        if (m_currentState != null)
        {
            m_currentState.onExit();
        }
    }


    //设置起作用的单元主题
    public void setUnit(LiveObject obj) {
        m_unit = obj;
    }
}
