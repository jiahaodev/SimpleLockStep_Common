/****************************************************
	文件：UnityObject.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/10 23:16   	
	功能：Unity对象基类
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
    //是否被杀掉了
    public bool m_bKilled = false;
    //最后的位置
    public FixVector3 m_fixv3LastPos;
    //逻辑位置
    public FixVector3 m_fixv3LogicPos;
    //旋转值
    FixVector3 m_fixv3LogicRot;
    //缩放值
    FixVector3 m_fixv3LogicScale;


    //创建预制体
    public void createFromPrefab(string path, UnityObject script)
    {
#if _CLIENTLOGIC_
        PrefabUtil.create(path, script); 
#endif
    }

    //更新渲染位置
    public void updateRenderPosition(float interpolation)
    {
#if _CLIENTLOGIC_
        if (m_bKilled)
        {
            return;
        }

        //只有会移动的对象才需要采用插值算法 来 计算 补间动画
        //不会移动的对象直接设置位置即可
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

    //播放动画
    public void playAnimation(string animationName)
    {

    }

    //停止动画
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

    //设置缩放值
    public void setScale(FixVector3 value)
    {
        m_fixv3LogicScale = value;
#if _CLIENTLOGIC_
        m_gameObject.transform.localScale = value.ToVector3();
#endif
    }

    //获取缩放值
    public FixVector3 getScale()
    {
        return m_fixv3LogicScale;
    }

    //设置旋转值
    public void setRotation(FixVector3 value)
    {
        m_fixv3LogicRot = value;
#if _CLIENTLOGIC_
        m_gameObject.transform.localEulerAngles = value.ToVector3();
#endif
    }

    //获取旋转值
    public FixVector3 getRotation()
    {
        return m_fixv3LogicRot;
    }

    //设置是否可见
    public void setVisible(bool value)
    {
#if _CLIENTLOGIC_
        m_gameObject.SetActive(value);
#endif
    }

    //删除gameObject
    public void destroyGameObject()
    {
#if _CLIENTLOGIC_
        GameObject.Destroy(m_gameObject);
#endif
    }

    //设置gameObject的名字
    public void setGameObjectName(string name)
    {
#if _CLIENTLOGIC_
        m_gameObject.name = name;
#endif
    }

    //获取gameObject的名字
    public string getGameObjectName()
    {
#if _CLIENTLOGIC_
        return m_gameObject.name;
#else
		return "";
#endif
    }

    //设置位置
    public void setGameObjectPosition(FixVector3 position)
    {
#if _CLIENTLOGIC_
        m_gameObject.transform.localPosition = position.ToVector3();
#endif
    }

    //获取位置
    //public FixVector3 getPosition() {
    //     if (!GameData.g_client) { return new FixVector3(Fix64.Zero, Fix64.Zero, Fix64.Zero);}

    //    return gameObject.transform.localPosition;
    // }


    //设置颜色
    public void setColor(float r, float g, float b)
    {
#if _CLIENTLOGIC_
        m_gameObject.GetComponent<Renderer>().material.color = new Color(r, g, b);
#endif
    }


}
