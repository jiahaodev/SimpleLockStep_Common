/****************************************************
	�ļ���TowerAttackState.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/11 16:02   	
	���ܣ�������״̬
*****************************************************/

using System.Collections;

public class TowerAttackState : BaseState
{
    public TowerAttackState()
    {
        init();
    }

    void init()
    {
        m_scName = "towerattack";
    }


    public override void onInit(LiveObject args)
    {
        m_unit = args;
    }

    //���빥��״̬�������ӵ����������Ŀ��
    //��һ���������ȴ״̬
    public override void onEnter(Fix64 args)
    {
        BaseSoldier soldier = (BaseSoldier)m_unit.lockedAttackUnit;

        GameData.g_bulletManager.createBullet(m_unit, soldier, m_unit.m_fixv3LogicPos, soldier.m_fixv3LogicPos);
        m_unit.changeState("cooling", m_unit.attackSpeed);
    }


    public override void onExit()
    {

    }


    public override void updateLogic()
    {

    }
}
