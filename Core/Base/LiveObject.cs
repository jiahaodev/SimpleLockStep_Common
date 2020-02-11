/****************************************************
	文件：LiveObject.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 14:12   	
	功能：有生命物体对象基类
          （如士兵、塔。而子弹则不算）
*****************************************************/

using System.Collections;
using System.Collections.Generic;

public class LiveObject : BaseObject
{

    public Fix64 hp { get; set; }

    public Fix64 orignalHp { get; set; }

    //普通伤害
    public Fix64 damage { get; set; }

    //攻击我的列表
    public List<LiveObject> m_listAttackMe = new List<LiveObject>();
    //攻击我的子弹列表
    public List<BaseObject> m_listAttackMeBullet = new List<BaseObject>();

    //我正在攻击的列表
    public List<LiveObject> m_listAttackingList = new List<LiveObject>();

    //侦测范围
    public Fix64 attackRange { get; set; }

    //攻击速度
    public Fix64 attackSpeed { get; set; }
    //锁定的攻击对象
    public LiveObject lockedAttackUnit { get; set; }
    //是否处于冷却状态
    public bool isCooling { get; set; }

    public void setHp(Fix64 value)
    {
        hp = value;
        orignalHp = value;
    }

    public Fix64 getHp()
    {
        return hp;
    }

    //添加我正在攻击的对象
    //用于在死亡时通知对应的对象
    public void addAttackingObj(LiveObject obj)
    {
        if (m_listAttackingList.Contains(obj))
        {
            return;
        }
        m_listAttackingList.Add(obj);
    }

    //移除我正在攻击的对象
    //对方死亡时，将对方从 当前的 攻击队列中移除掉
    public void removeAttackingObj(LiveObject obj)
    {
        m_listAttackingList.Remove(obj);
    }

    //添加正在攻击我的对象
    //用于在死亡时通知对应对象
    public void addAttackMeObj(LiveObject obj)
    {
        if (m_listAttackMe.Contains(obj))
        {
            return;
        }
        m_listAttackMe.Add(obj);
    }

    //移除正在攻击我的对象
    //对方死亡时，要从 对方 正在攻击的队列中 把我移除掉
    public void removeAttackMeObj(LiveObject obj)
    {
        m_listAttackMe.Remove(obj);
    }

    //添加正在攻击我的子弹对象
    //用于在死亡时通知对应的子弹对象
    public void addAttackMeBulletObj(BaseObject obj)
    {
        if (m_listAttackMeBullet.Contains(obj))
        {
            return;
        }
        m_listAttackMeBullet.Add(obj);
    }

    //移除正在攻击我的子弹对象
    //对方死亡时，要从正在攻击我的子弹队列中移除掉该子弹
    public void removeAttackMeBulletObj(BaseObject obj)
    {
        m_listAttackMeBullet.Remove(obj);
    }

    //发送死亡信息给相应对象
    public void sendDeadInfoToRelativeObj()
    {
        //让所有攻击我的子弹失效
        for (int i = m_listAttackMeBullet.Count - 1; i >= 0; i--)
        {
            m_listAttackMeBullet[i].uneffect = true;
            removeAttackMeBulletObj(m_listAttackMeBullet[i]);
        }

        //通知我正在攻击的对象,我已经死了,从我正在攻击的对象身上把自身移除
        for (int i = m_listAttackingList.Count - 1; i >= 0; i--)
        {
            LiveObject obj = m_listAttackingList[i];
            obj.removeAttackMeObj(this);
            removeAttackingObj(obj);
        }

        //通知正在攻击我的对象,我已经死了,别打了
        for (int i = m_listAttackMe.Count - 1; i >= 0; i--)
        {
            LiveObject obj = m_listAttackMe[i];
            obj.removeAttackingObj(this);
            removeAttackMeObj(obj);

            if (obj.m_scType == "tower")
            {
                //print("current state . ", obj.getState())
                if (obj.getState() != "cooling")
                {
                    obj.changeState("towerstand");
                }
                else
                {
                    obj.setPrevStateName("towerstand");
                }
            }
        }
    }

    //设置攻击力
    public void setDamageValue(Fix64 value)
    {
        damage = value;
    }

    //获取攻击力
    public Fix64 getDamageValue()
    {
        return damage;
    }

    //受到伤害
    public void beDamage(Fix64 damage, bool isSrcCrit = false)
    {
        if (m_bKilled)
        {
            return;
        }

        if (m_scType == "tower")
        {
            playAnimation("Hurt");

            delayDo((Fix64)0.5, delegate ()
            {
                playAnimation("Stand");
            }, "delaytostand");
        }

        hp -= damage;

        if (hp <= Fix64.Zero)
        {
            m_bKilled = true;
        }
    }


    public override void killSelf()
    {
        sendDeadInfoToRelativeObj();

        base.killSelf();
    }

    public virtual void loadProperties()
    {


    }

    public Fix64 getAttackRange()
    {
        return attackRange;
    }

    public void changeState(string state)
    {
        m_statemachine.changeState(state, (Fix64)0);
    }

    public void changeState(string state, Fix64 args)
    {
        m_statemachine.changeState(state, args);
    }

    public string getState()
    {
        return m_statemachine.getState();
    }

    // 设置之前的状态的名字
    // 记录之前的状态,某些状态需要在执行后恢复到之前的状态,所以需要记录
    public void setPrevStateName(string stateName)
    {
        m_statemachine.setPrevStateName(stateName);
    }

    //获取之前的状态的名字
    public string getPrevStateName()
    {
        return m_statemachine.getPrevStateName();
    }

}
