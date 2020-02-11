
/****************************************************
	�ļ���CoolingState.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/11 15:25   	
	���ܣ���ȴ״̬
*****************************************************/

using System.Collections;

public class CoolingState : BaseState
{

    public CoolingState()
    {
        init();
    }

    private void init()
    {
        m_scName = "cooling";
    }

    public override void onInit(LiveObject args)
    {
        m_unit = args;
    }

    public override void onEnter(Fix64 args)
    {
        m_unit.isCooling = true;

        Fix64 cdtime = args;

        m_unit.delayDo(cdtime, delegate ()
        {
            if (m_scPrevStateName != null)
            {
                m_unit.checkState();
                m_unit.changeState(m_scPrevStateName);
            }
        }, "changePrevState");
    }

    public override void onExit()
    {
        m_unit.isCooling = false;
        m_unit.stopAction("changePrevState");
    }

    public override void updateLogic()
    {

    }
}