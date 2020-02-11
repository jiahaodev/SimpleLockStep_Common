/****************************************************
	文件：NormalState.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 15:13   	
	功能：普通状态
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