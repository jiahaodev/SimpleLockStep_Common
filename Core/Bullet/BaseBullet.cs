/****************************************************
	文件：BaseBullet.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 16:33   	
	功能：子弹基类
*****************************************************/

using System.Collections;
using System.Collections.Generic;

public class BaseBullet : BaseObject
{

    LiveObject m_src = null;

    protected LiveObject m_dest = null;
    protected FixVector3 m_fixv3SrcPos = new FixVector3();
    protected FixVector3 m_fixv3DestPos = new FixVector3();
    protected Fix64 m_fixDamage = Fix64.Zero;

    public virtual void updateLogic()
    {
        checkEvent();
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="src">发射源</param>
    /// <param name="dest">射击目标</param>
    /// <param name="poSrc">发射的起始位置</param>
    /// <param name="poDst">发射的目标位置</param>
    public virtual void initData(LiveObject src, LiveObject dest, FixVector3 poSrc, FixVector3 poDst)
    {

        m_scType = "bullet";

        loadProperties();

        m_src = src;
        m_dest = dest;
        m_fixv3SrcPos = poSrc;
        m_fixv3DestPos = poDst;

        m_fixDamage = m_src.getDamageValue();
    }

    //射击
    public virtual void shoot()
    {

    }

    //攻击目标
    public virtual void doShootDest()
    {
        if (uneffect == false)
        {
            removeFromDestBulletList();

            m_dest.beDamage(m_fixDamage);
        }
        m_bKilled = true;
    }

    //从攻击者的子弹列表中移除自身
    //避免被攻击者已经死亡，子弹还在攻击的问题
    protected void removeFromDestBulletList()
    {
        m_dest.m_listAttackMeBullet.Remove(this);
    }


    //加载属性
    public virtual void loadProperties()
    {

    }

    //根据名字加载预制体
    public virtual void createBody(string name)
    {

    }


}
