/****************************************************
	文件：GameData.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 17:41   	
	功能：公用数据类
*****************************************************/

using System.Collections;
using System.Collections.Generic;


public class GameData  
{
    //士兵列表
    public static List<BaseSoldier> g_listSoldier = new List<BaseSoldier>();
    //塔列表
    public static List<BaseTower> g_listTower = new List<BaseTower>();
    //子弹列表
    public static List<BaseBullet> g_listBullet = new List<BaseBullet>();
    //操作事件列表
    public static List<BattleInfo> g_listUserControlEvent = new List<BattleInfo>();
    //回放事件列表
    public static List<BattleInfo> g_listPlaybackEvent = new List<BattleInfo>();
    //预定的每帧的时间长度
    public static Fix64 g_fixFrameLen = Fix64.FromRaw(273);
    //游戏的逻辑帧
    public static int g_uGameLogicFrame = 0;
    //是否为回放模式
    public static bool g_bReplayMode = false;

    //士兵工厂
    public static SoldierFactory g_soldierFactory = new SoldierFactory();
    //塔工厂
    public static TowerFactory g_towerFactory = new TowerFactory();
    //子弹工厂
    public static BulletFactory g_bulletManager = new BulletFactory();
    //action主管理器
    //（用于管理个liveobject内部的独立actionManager）
    public static ActionMainManager g_actionMainManager = new ActionMainManager();

    //战斗是否结束
    public static bool g_bBattleEnd = false;
    //全局的随机数对象
    public static SRandom g_srand = new SRandom(1000);

    //战斗信息结构
    public struct BattleInfo {
        public int uGameFrame;
        public string sckeyEvent;
    }

    //释放资源
    public static void release() {
        g_listPlaybackEvent.Clear();

        g_listUserControlEvent.Clear();

        g_actionMainManager.release();
    }

}