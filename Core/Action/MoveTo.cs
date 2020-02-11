/****************************************************
	文件：MoveTo.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/10 22:32   	
	功能：移动到指定位置
*****************************************************/


public class MoveTo : BaseAction
{

    Fix64 m_fixMoveTime;
    Fix64 m_fixMoveElapseTime;
    FixVector3 m_fixMoveStartPosition;
    FixVector3 m_fixMoveEndPosition;
    FixVector3 m_fixv3MoveDistance;


    public void init(BaseObject unitbody, FixVector3 startPos, FixVector3 endPos, Fix64 time, ActionCallback cb)
    {
        name = "MoveTo";

        unit = unitbody;
        unit.m_fixv3LogicPos = startPos;
        m_fixMoveStartPosition = startPos;
        m_fixMoveEndPosition = endPos;
        m_fixv3MoveDistance = endPos - startPos;
        m_fixMoveTime = time;

        if (m_fixMoveTime == Fix64.Zero)
        {
            m_fixMoveTime = (Fix64)0.1f;
        }

        actionCallbackFunc = cb;
    }

    public override void updateLogic()
    {
        bool actionOver = false;

        m_fixMoveElapseTime += GameData.g_fixFrameLen;

        Fix64 timeScale = m_fixMoveElapseTime / m_fixMoveTime;

        if (timeScale >= (Fix64)1)
        {
            timeScale = (Fix64)1;
            actionOver = true;
        }

        FixVector3 elapseDistance = m_fixv3MoveDistance * timeScale;
        FixVector3 newPosition = m_fixMoveStartPosition + elapseDistance;

        unit.m_fixv3LogicPos = newPosition;

        if (actionOver)
        {
            removeSelfFromManager();
            if (actionCallbackFunc != null)
            {
                actionCallbackFunc();
            }
        }

    }




}