/****************************************************
	文件：TowerAttackState.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 16:02   	
	功能：塔攻击状态
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

    //进入攻击状态，发射子弹射击锁定的目标
    //下一步则进入冷却状态
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
