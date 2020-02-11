/****************************************************
	文件：BaseObject..cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/10 23:16   	
	功能：物体对象基类
*****************************************************/

using System;
using System.Collections;

public class BaseObject : UnityObject
{
    protected MoveTo moveToAction { get; set; }

    public string name { get; set; }

    public ActionManager actionManager { get; set; }

    protected StateMachine m_statemachine = null;
    public StateMachine priorAttackTarget { get { return m_statemachine; } set { m_statemachine = value; } }

    public bool uneffect { get; set; }

    public BaseObject()
    {
        init();
    }

    private void init()
    {
        actionManager = new ActionManager();
        GameData.g_actionMainManager.addActionManager(actionManager);
    }

    public void moveTo(FixVector3 startPos, FixVector3 endPos, Fix64 time, ActionCallback cb = null)
    {
        if (moveToAction == null)
        {
            moveToAction = new MoveTo();
            moveToAction.init(this, startPos, endPos, time, cb);
            actionManager.addAction(moveToAction);
        }
    }

    public void delayDo(Fix64 time, ActionCallback cb, string label = null)
    {
        DelayDo delayDoAction = new DelayDo();
        delayDoAction.init(time, cb);

        if (label != null)
        {
            delayDoAction.label = label;
        }
        actionManager.addAction(delayDoAction);
    }

    public void stopMove()
    {
        if (moveToAction != null)
        {
            actionManager.removeAction(moveToAction);
            moveToAction = null;
        }
    }

    public void stopAction(string label)
    {
        actionManager.stopAction(label);
    }

    public void stopActionByName(string name)
    {
        actionManager.stopActionByName(name);
    }

    public void stopAllAction()
    {
        actionManager.stopAllAction();
    }

    public void killActionManager()
    {
        actionManager.enable = false;
    }

    //检测事件并执行
    //由于事件都是一次性的，所以执行完毕后立即清空
    public void checkEvent()
    {
        if (m_bKilled)
        {
            //停止所有DelayDo实例
            stopActionByName("DelayDo");

            //塔
            if (m_scType == "tower")
            {

                for (int i = GameData.g_listTower.Count - 1; i >= 0; i--)
                {
                    if (this == GameData.g_listTower[i])
                    {
                        GameData.g_listTower.Remove(GameData.g_listTower[i]);
                        break;
                    }
                }

            }
            //士兵
            else if (m_scType == "soldier")
            {
                for (int i = GameData.g_listSoldier.Count - 1; i >= 0; i--)
                {
                    if (this == GameData.g_listSoldier[i])
                    {
                        GameData.g_listSoldier.Remove(GameData.g_listSoldier[i]);
                        break;
                    }
                }
            }
            //子弹
            else if (m_scType == "bullet")
            {
                for (int i = GameData.g_listBullet.Count - 1; i >= 0; i--)
                {
                    if (this == GameData.g_listBullet[i])
                    {
                        GameData.g_listBullet.Remove(GameData.g_listBullet[i]);
                        break;
                    }
                }
            }
            //其它
            else
            {
                UnityTools.LogError("wrong type : " + m_scType);
            }

        }
    }

    public virtual void setPosition(FixVector3 position)
    {
        m_fixv3LogicPos = position;
    }

    public FixVector3 getPosition()
    {
        return m_fixv3LogicPos;
    }

    public void checkIsDead()
    {
        if (m_bKilled)
        {
            killSelf();
        }
    }

    public virtual void killSelf()
    {
        stopAllAction();
        killActionManager();

        if (m_statemachine != null)
        {
            m_statemachine.exitOldState();
        }

        m_bKilled = true;

        checkEvent();
    }

    
    public virtual void checkState()
    {

    }

    //记录最后的位置
    public void recordLastPos() {
        m_fixv3LastPos = m_fixv3LogicPos;
    }
}