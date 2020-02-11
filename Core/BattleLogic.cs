/****************************************************
	文件：BattleLogic.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 18:04   	
	功能：战斗主逻辑
*****************************************************/

using System.Collections;
using System.Collections.Generic;

public class BattleLogic
{


    //帧同步核心逻辑对象
    LockStepLogic m_lockStepLogic = null;

    //战斗日志
    string battleRecord = "";

    //游戏逻辑帧
    public static int s_uGameLogicFrame = 0;

    //是否已开战
    private bool m_bFireWar = false;

    //是否暂停（进入结算界面时，战斗逻辑会暂停）
    public bool m_bIsBattlePause = true;

    //主循环
    public void updateLogic()
    {
        if (m_bIsBattlePause)
        {
            return;
        }
        //调用帧同步逻辑
        m_lockStepLogic.updateLogic();
    }

    //战斗逻辑
    public void frameLockLogic()
    {
        //如果是回放模式
        if (GameData.g_bReplayMode)
        {
            //检测回放事件
            checkPlayBackEvent(GameData.g_uGameLogicFrame);
        }

        recordLastPos();

        //动作管理器
        GameData.g_actionMainManager.updateLogic();

        //塔
        for (int i = 0; i < GameData.g_listTower.Count; i++)
        {
            GameData.g_listTower[i].updateLogic();
        }

        //子弹
        for (int i = 0; i < GameData.g_listBullet.Count; i++)
        {
            GameData.g_listBullet[i].updateLogic();
        }

        //士兵
        for (int i = 0; i < GameData.g_listSoldier.Count; i++)
        {
            GameData.g_listSoldier[i].updateLogic();
        }

        //如果士兵全部阵亡，则停止战斗
        if (m_bFireWar && GameData.g_listSoldier.Count == 0)
        {
            stopBattle();
        }
    }

    //记录最后的位置
    //包括怪,子弹等等,因为塔的位置是固定的,所以不需要实时刷新塔的位置,提升效率
    void recordLastPos()
    {
        //子弹
        for (int i = 0; i < GameData.g_listBullet.Count; i++)
        {
            GameData.g_listBullet[i].recordLastPos();
        }

        //士兵
        for (int i = 0; i < GameData.g_listSoldier.Count; i++)
        {
            GameData.g_listSoldier[i].recordLastPos();
        }
    }

    //更新各种对象绘制的位置
    public void updateRenderPosition(float interpolation)
    {
        //子弹
        for (int i = 0; i < GameData.g_listBullet.Count; i++)
        {
            GameData.g_listBullet[i].updateRenderPosition(interpolation);
        }

        //士兵
        for (int i = 0; i < GameData.g_listSoldier.Count; i++)
        {
            GameData.g_listSoldier[i].updateRenderPosition(interpolation);
        }
    }

    //初始化
    public void init()
    {
        UnityTools.Log("BattleLogic init!");
        //初始化游戏帧同步逻辑对象
        m_lockStepLogic = new LockStepLogic();
        m_lockStepLogic.setCallUnit(this);

        //游戏运行速度
        UnityTools.setTimeScale(1);

        //战斗暂停
        m_bIsBattlePause = true;
    }

    //开始战斗
    public void startBattle()
    {
        GameData.g_srand = new SRandom(1000);
        m_bIsBattlePause = false;
        m_lockStepLogic.init();

        //读取玩家操作数据，为回放做准备
        if (GameData.g_bReplayMode)
        {
            loadUserCtrlInfo();
        }

        GameData.g_uGameLogicFrame = 0;
        TowerStandState.s_fixTestCount = (Fix64)0;
        TowerStandState.s_scTestContent = "";

        //创建塔
        createTowers();
    }

    //停止战斗
    public void stopBattle()
    {
        UnityTools.Log("end logic frame: " + GameData.g_uGameLogicFrame);
        UnityTools.Log("s_fixTestCount: " + TowerStandState.s_fixTestCount);
        UnityTools.Log("s_scTestContent: " + TowerStandState.s_scTestContent);

        m_bFireWar = false;
        s_uGameLogicFrame = GameData.g_uGameLogicFrame;

        //记录关键事件
        if (!GameData.g_bReplayMode)
        {
            GameData.BattleInfo info = new GameData.BattleInfo();
            info.uGameFrame = GameData.g_uGameLogicFrame;
            info.sckeyEvent = "stopBattle";
        }

        gameEnd();

    }

    //暂停战斗逻辑
    void pauseBattleLogic()
    {
        m_bIsBattlePause = true;
    }

    //游戏结束
    public void gameEnd()
    {
        if (!m_bIsBattlePause)
        {
            //销毁战场上所有对象
            //塔
            for (int i = GameData.g_listTower.Count-1; i >= 0; i--)
            {
                GameData.g_listTower[i].killSelf();
            }
            //子弹
            for (int i = GameData.g_listBullet.Count - 1; i >= 0; i--)
            {
                GameData.g_listBullet[i].killSelf();
            }
            //士兵
            for (int i = GameData.g_listSoldier.Count - 1; i >= 0; i--)
            {
                GameData.g_listSoldier[i].killSelf();
            }

            if (!GameData.g_bReplayMode)
            {
                recordBattleInfo();
#if _CLIENTLOGIC_
                //如果是客户端，将本地的战斗消息发送到服务端进行校验
                //SimpleSocket socket = new SimpleSocket();
                //socket.Init();
                //socket.sendBattleRecordToServer(UnityTools.playerPrefsGetString("battleRecord"));
#endif
            }

            pauseBattleLogic();

            GameData.g_bReplayMode = false;
            GameData.release();
        }
    }

    //记录战斗消息（回放时使用）
    void recordBattleInfo()
    {
        if (!GameData.g_bReplayMode)
        {
            //记录战斗数据
            string content = "";
            for (int i = 0; i < GameData.g_listUserControlEvent.Count; i++)
            {
                GameData.BattleInfo v = GameData.g_listUserControlEvent[i];
                //出兵
                if (v.sckeyEvent == "createSoldier")
                {
                    content += v.uGameFrame + "," + v.sckeyEvent + "$";
                }
            }

            UnityTools.playerPrefsSetString("battleRecord", content);
            GameData.g_listUserControlEvent.Clear();
        }
    }

    //设置战斗消息
    public void setBattleRecord(string record)
    {
        battleRecord = record;
    }

    //回放战斗录像
    public void replayVideo()
    {
        GameData.g_bReplayMode = true;
        GameData.g_uGameLogicFrame = 0;
        startBattle();
    }


    //检测回放事件(如果有回放事件则进行回放)
    void checkPlayBackEvent(int gameFrame)
    {
        if (GameData.g_listPlaybackEvent.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < GameData.g_listPlaybackEvent.Count; i++)
        {
            GameData.BattleInfo v = GameData.g_listPlaybackEvent[i];

            if (gameFrame == v.uGameFrame)
            {
                if (v.sckeyEvent == "createSoldier")
                {
                    createSoldier();
                }
            }
        }
    }


    //创建塔
    void createTowers()
    {
        for (int i = 0; i < 5; i++)
        {
            var tower = GameData.g_towerFactory.createTower();
            tower.m_fixv3LogicPos = new FixVector3((Fix64)5, (Fix64)1.3f, (Fix64)(-3.0f) + (Fix64)2.5f * i);
            tower.updateRenderPosition(0);
        }
    }

    //创建士兵
    public void createSoldier()
    {
        m_bFireWar = true;

        var soldier = GameData.g_soldierFactory.createSoldier();
        soldier.m_fixv3LogicPos = new FixVector3((Fix64)0, (Fix64)1, (Fix64)(-4.0f));
        soldier.updateRenderPosition(0);

        float moveSpeed = 3 + GameData.g_srand.Range(0,3);
        soldier.moveTo(soldier.m_fixv3LogicPos, new FixVector3(soldier.m_fixv3LogicPos.x, soldier.m_fixv3LogicPos.y, (Fix64)8),(Fix64)moveSpeed);

        //记录关键事件
        if (!GameData.g_bReplayMode)
        {
            GameData.BattleInfo info = new GameData.BattleInfo();
            info.uGameFrame = GameData.g_uGameLogicFrame;
            info.sckeyEvent = "createSoldier";
            GameData.g_listUserControlEvent.Add(info);
        }
    }

    //读取玩家操作信息
    void loadUserCtrlInfo() {
        GameData.g_listUserControlEvent.Clear();

        string content = battleRecord;

        string[] contents = content.Split('$');

        for (int i = 0; i < contents.Length-1; i++)
        {
            string[] battleInfo = contents[i].Split(',');

            GameData.BattleInfo info = new GameData.BattleInfo();
            info.uGameFrame = int.Parse(battleInfo[0]);
            info.sckeyEvent = battleInfo[1];
            GameData.g_listPlaybackEvent.Add(info);
        }
    }

    //释放资源
    void release()
    {

    }

}