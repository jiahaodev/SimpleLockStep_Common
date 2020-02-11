/****************************************************
	�ļ���BaseBullet.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/11 16:33   	
	���ܣ��ӵ�����
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
    /// ��ʼ������
    /// </summary>
    /// <param name="src">����Դ</param>
    /// <param name="dest">���Ŀ��</param>
    /// <param name="poSrc">�������ʼλ��</param>
    /// <param name="poDst">�����Ŀ��λ��</param>
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

    //���
    public virtual void shoot()
    {

    }

    //����Ŀ��
    public virtual void doShootDest()
    {
        if (uneffect == false)
        {
            removeFromDestBulletList();

            m_dest.beDamage(m_fixDamage);
        }
        m_bKilled = true;
    }

    //�ӹ����ߵ��ӵ��б����Ƴ�����
    //���ⱻ�������Ѿ��������ӵ����ڹ���������
    protected void removeFromDestBulletList()
    {
        m_dest.m_listAttackMeBullet.Remove(this);
    }


    //��������
    public virtual void loadProperties()
    {

    }

    //�������ּ���Ԥ����
    public virtual void createBody(string name)
    {

    }


}
