/****************************************************
	�ļ���BaseTower.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/11 17:28   	
	���ܣ�������
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

    //- ����λ��
    public override void setPosition(FixVector3 position)
    {
        m_fixv3LogicPos = position;
    }

    //- ���б��Ƿ��߳�������Χ
    private void checkSoldierOutRange()
    {
        if (lockedAttackUnit != null)
        {
            Fix64 distance = FixVector3.Distance(m_fixv3LogicPos, lockedAttackUnit.m_fixv3LogicPos);

            //����߳�������Χ,�������ָ�������״̬
            if (distance > attackRange)
            {
                setPrevStateName("towerstand");
            }
        }
    }

    // ���״̬
    // ����ȴ״̬��������һ�µ�ǰ״̬,�Ա���ݵ�ǰ״̬ˢ���߼�
    public override void checkState()
    {
        checkSoldierOutRange();
    }
}