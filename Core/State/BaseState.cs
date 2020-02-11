/****************************************************
	文件：BaseState.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 14:54   	
	功能：状态 基类
*****************************************************/

using System.Collections;

public class BaseState
{
    //所挂载的主体单元
    protected LiveObject m_unit = null;
    //之前的状态名
    protected string m_scPrevStateName = "";
    //当前的状态名
    protected string m_scName = "";

    /// <summary>
    /// 创建时，进入的初始化函数
    /// </summary>
    /// <param name="args">附加的创建信息</param>
    public virtual void onInit(LiveObject args)
    {

    }

    /// <summary>
    /// 进入该状态时调用的函数
    /// </summary>
    /// <param name="args">附加的调用信息</param>
    public virtual void onEnter(Fix64 args)
    {

    }

    /// <summary>
    /// 退出该状态时调用的函数
    /// </summary>
    public virtual void onExit()
    {

    }

    /// <summary>
    ///处于改状态时每帧调用的函数
    /// </summary>
    public virtual void updateLogic()
    {

    }


    /// <summary>
    /// 设置之前的状态名字
    /// （记录之前的状态，某些状态需要在执行后恢复到之前的状态，所以需要记录）
    /// </summary>
    /// <param name="stateName"></param>
    public void setPrevStateName(string stateName)
    {
        m_scPrevStateName = stateName;
    }

    public string getPrevStateName()
    {
        return m_scPrevStateName;
    }


}