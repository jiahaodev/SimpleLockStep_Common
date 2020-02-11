/****************************************************
	�ļ���LiveObject.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/11 14:12   	
	���ܣ�����������������
          ����ʿ�����������ӵ����㣩
*****************************************************/

using System.Collections;
using System.Collections.Generic;

public class LiveObject : BaseObject
{

    public Fix64 hp { get; set; }

    public Fix64 orignalHp { get; set; }

    //��ͨ�˺�
    public Fix64 damage { get; set; }

    //�����ҵ��б�
    public List<LiveObject> m_listAttackMe = new List<LiveObject>();
    //�����ҵ��ӵ��б�
    public List<BaseObject> m_listAttackMeBullet = new List<BaseObject>();

    //�����ڹ������б�
    public List<LiveObject> m_listAttackingList = new List<LiveObject>();

    //��ⷶΧ
    public Fix64 attackRange { get; set; }

    //�����ٶ�
    public Fix64 attackSpeed { get; set; }
    //�����Ĺ�������
    public LiveObject lockedAttackUnit { get; set; }
    //�Ƿ�����ȴ״̬
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

    //��������ڹ����Ķ���
    //����������ʱ֪ͨ��Ӧ�Ķ���
    public void addAttackingObj(LiveObject obj)
    {
        if (m_listAttackingList.Contains(obj))
        {
            return;
        }
        m_listAttackingList.Add(obj);
    }

    //�Ƴ������ڹ����Ķ���
    //�Է�����ʱ�����Է��� ��ǰ�� �����������Ƴ���
    public void removeAttackingObj(LiveObject obj)
    {
        m_listAttackingList.Remove(obj);
    }

    //������ڹ����ҵĶ���
    //����������ʱ֪ͨ��Ӧ����
    public void addAttackMeObj(LiveObject obj)
    {
        if (m_listAttackMe.Contains(obj))
        {
            return;
        }
        m_listAttackMe.Add(obj);
    }

    //�Ƴ����ڹ����ҵĶ���
    //�Է�����ʱ��Ҫ�� �Է� ���ڹ����Ķ����� �����Ƴ���
    public void removeAttackMeObj(LiveObject obj)
    {
        m_listAttackMe.Remove(obj);
    }

    //������ڹ����ҵ��ӵ�����
    //����������ʱ֪ͨ��Ӧ���ӵ�����
    public void addAttackMeBulletObj(BaseObject obj)
    {
        if (m_listAttackMeBullet.Contains(obj))
        {
            return;
        }
        m_listAttackMeBullet.Add(obj);
    }

    //�Ƴ����ڹ����ҵ��ӵ�����
    //�Է�����ʱ��Ҫ�����ڹ����ҵ��ӵ��������Ƴ������ӵ�
    public void removeAttackMeBulletObj(BaseObject obj)
    {
        m_listAttackMeBullet.Remove(obj);
    }

    //����������Ϣ����Ӧ����
    public void sendDeadInfoToRelativeObj()
    {
        //�����й����ҵ��ӵ�ʧЧ
        for (int i = m_listAttackMeBullet.Count - 1; i >= 0; i--)
        {
            m_listAttackMeBullet[i].uneffect = true;
            removeAttackMeBulletObj(m_listAttackMeBullet[i]);
        }

        //֪ͨ�����ڹ����Ķ���,���Ѿ�����,�������ڹ����Ķ������ϰ������Ƴ�
        for (int i = m_listAttackingList.Count - 1; i >= 0; i--)
        {
            LiveObject obj = m_listAttackingList[i];
            obj.removeAttackMeObj(this);
            removeAttackingObj(obj);
        }

        //֪ͨ���ڹ����ҵĶ���,���Ѿ�����,�����
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

    //���ù�����
    public void setDamageValue(Fix64 value)
    {
        damage = value;
    }

    //��ȡ������
    public Fix64 getDamageValue()
    {
        return damage;
    }

    //�ܵ��˺�
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

    // ����֮ǰ��״̬������
    // ��¼֮ǰ��״̬,ĳЩ״̬��Ҫ��ִ�к�ָ���֮ǰ��״̬,������Ҫ��¼
    public void setPrevStateName(string stateName)
    {
        m_statemachine.setPrevStateName(stateName);
    }

    //��ȡ֮ǰ��״̬������
    public string getPrevStateName()
    {
        return m_statemachine.getPrevStateName();
    }

}
