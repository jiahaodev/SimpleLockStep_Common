/****************************************************
	�ļ���UnityObject.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/10 23:16   	
	���ܣ�Unity�������
*****************************************************/
#if _CLIENTLOGIC_
using UnityEngine;
#endif

using System.Collections;

public class UnityObject
{
    public string m_scBundle = "";
    public string m_scAsset = "";
    public string m_scType = "";

#if _CLIENTLOGIC_
    public GameObject m_gameObject;
#endif
    //�Ƿ�ɱ����
    public bool m_bKilled = false;
    //����λ��
    public FixVector3 m_fixv3LastPos;
    //�߼�λ��
    public FixVector3 m_fixv3LogicPos;
    //��תֵ
    FixVector3 m_fixv3LogicRot;
    //����ֵ
    FixVector3 m_fixv3LogicScale;


    //����Ԥ����
    public void createFromPrefab(string path, UnityObject script)
    {
#if _CLIENTLOGIC_
        PrefabUtil.create(path, script); 
#endif
    }

    //������Ⱦλ��
    public void updateRenderPosition(float interpolation)
    {
#if _CLIENTLOGIC_
        if (m_bKilled)
        {
            return;
        }

        //ֻ�л��ƶ��Ķ������Ҫ���ò�ֵ�㷨 �� ���� ���䶯��
        //�����ƶ��Ķ���ֱ������λ�ü���
        if ((m_scType == "soldier" || m_scType == "bullet" && interpolation != 0))
        {
            m_gameObject.transform.localPosition = Vector3.Lerp(m_fixv3LastPos.ToVector3(), m_fixv3LogicPos.ToVector3(), interpolation);
        }
        else
        {
            m_gameObject.transform.localPosition = m_fixv3LogicPos.ToVector3();
        }
#endif
    }

    //���Ŷ���
    public void playAnimation(string animationName)
    {

    }

    //ֹͣ����
    public void stopAnimation()
    {
#if _CLIENTLOGIC_
        Animation animation = m_gameObject.GetComponent<Animation>();
        if (animation != null)
        {
            animation.Stop();
        }
#endif
    }

    //��������ֵ
    public void setScale(FixVector3 value)
    {
        m_fixv3LogicScale = value;
#if _CLIENTLOGIC_
        m_gameObject.transform.localScale = value.ToVector3();
#endif
    }

    //��ȡ����ֵ
    public FixVector3 getScale()
    {
        return m_fixv3LogicScale;
    }

    //������תֵ
    public void setRotation(FixVector3 value)
    {
        m_fixv3LogicRot = value;
#if _CLIENTLOGIC_
        m_gameObject.transform.localEulerAngles = value.ToVector3();
#endif
    }

    //��ȡ��תֵ
    public FixVector3 getRotation()
    {
        return m_fixv3LogicRot;
    }

    //�����Ƿ�ɼ�
    public void setVisible(bool value)
    {
#if _CLIENTLOGIC_
        m_gameObject.SetActive(value);
#endif
    }

    //ɾ��gameObject
    public void destroyGameObject()
    {
#if _CLIENTLOGIC_
        GameObject.Destroy(m_gameObject);
#endif
    }

    //����gameObject������
    public void setGameObjectName(string name)
    {
#if _CLIENTLOGIC_
        m_gameObject.name = name;
#endif
    }

    //��ȡgameObject������
    public string getGameObjectName()
    {
#if _CLIENTLOGIC_
        return m_gameObject.name;
#else
		return "";
#endif
    }

    //����λ��
    public void setGameObjectPosition(FixVector3 position)
    {
#if _CLIENTLOGIC_
        m_gameObject.transform.localPosition = position.ToVector3();
#endif
    }

    //��ȡλ��
    //public FixVector3 getPosition() {
    //     if (!GameData.g_client) { return new FixVector3(Fix64.Zero, Fix64.Zero, Fix64.Zero);}

    //    return gameObject.transform.localPosition;
    // }


    //������ɫ
    public void setColor(float r, float g, float b)
    {
#if _CLIENTLOGIC_
        m_gameObject.GetComponent<Renderer>().material.color = new Color(r, g, b);
#endif
    }


}
