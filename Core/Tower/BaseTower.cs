/****************************************************
	文件：BaseTower.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 17:28   	
	功能：塔基类
*****************************************************/
using System.Collections;

public class BaseTower : LiveObject
{
    public BaseTower()
    {
        init();
    }

    private void init()
    {
        m_scType = "tower";

        m_statemachine = new StateMachine();

        m_statemachine.setUnit(this);
    }


    public virtual void updateLogic()
    {
        m_statemachine.updateLogic();

        checkIsDead();

        checkEvent();
    }

    //- 设置位置
    public override void setPosition(FixVector3 position)
    {
        m_fixv3LogicPos = position;
    }

    //- 检测敌兵是否走出攻击范围
    private void checkSoldierOutRange()
    {
        if (lockedAttackUnit != null)
        {
            Fix64 distance = FixVector3.Distance(m_fixv3LogicPos, lockedAttackUnit.m_fixv3LogicPos);

            //如果走出攻击范围,则让塔恢复到待机状态
            if (distance > attackRange)
            {
                setPrevStateName("towerstand");
            }
        }
    }

    // 检查状态
    // 在冷却状态结束后检测一下当前状态,以便根据当前状态刷新逻辑
    public override void checkState()
    {
        checkSoldierOutRange();
    }
}