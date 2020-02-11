/****************************************************
	�ļ���NormalState.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/11 15:13   	
	���ܣ���ͨ״̬
*****************************************************/

using System.Collections;

public class NormalState : BaseState
{

    public NormalState()
    {
        init();
    }

    private void init()
    {
        m_scName = "normal";
    }

    public override void onInit(LiveObject args)
    {
        m_unit = args;
    }

    public override void onEnter(Fix64 args)
    {

    }

    public override void onExit()
    {

    }

    public override void updateLogic()
    {

    }
}