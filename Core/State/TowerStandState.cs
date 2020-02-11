/****************************************************
	文件：TowerStandState.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 15:55   	
	功能：塔待机状态
*****************************************************/
using System.Collections;

public class TowerStandState : BaseState
{
    public static Fix64 s_fixTestCount = (Fix64)0;

    public static string s_scTestContent = "";
    public TowerStandState()
    {
        init();
    }


    void init()
    {
        m_scName = "towerstand";
    }


    public override void onInit(LiveObject args)
    {
        m_unit = args;
    }


    public override void onEnter(Fix64 args)
    {
        //播放待机动画
        m_unit.playAnimation("Stand");
    }


    public override void onExit()
    {

    }

    public override void updateLogic()
    {
        for (int i = 0; i < GameData.g_listSoldier.Count; i++)
        {
            var soldier = GameData.g_listSoldier[i];

            Fix64 distance = FixVector3.Distance(m_unit.m_fixv3LogicPos, soldier.m_fixv3LogicPos);
            s_scTestContent += distance.ToString() + ",";

            if (distance <= (Fix64)m_unit.attackRange)
            {
                s_fixTestCount += distance;
                m_unit.lockedAttackUnit = soldier;

                m_unit.addAttackingObj(soldier);
                soldier.addAttackMeObj(m_unit);

                m_unit.changeState("towerattack");
            }
        }
    }
}