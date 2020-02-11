/****************************************************
	�ļ���BaseState.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/11 14:54   	
	���ܣ�״̬ ����
*****************************************************/

using System.Collections;

public class BaseState
{
    //�����ص����嵥Ԫ
    protected LiveObject m_unit = null;
    //֮ǰ��״̬��
    protected string m_scPrevStateName = "";
    //��ǰ��״̬��
    protected string m_scName = "";

    /// <summary>
    /// ����ʱ������ĳ�ʼ������
    /// </summary>
    /// <param name="args">���ӵĴ�����Ϣ</param>
    public virtual void onInit(LiveObject args)
    {

    }

    /// <summary>
    /// �����״̬ʱ���õĺ���
    /// </summary>
    /// <param name="args">���ӵĵ�����Ϣ</param>
    public virtual void onEnter(Fix64 args)
    {

    }

    /// <summary>
    /// �˳���״̬ʱ���õĺ���
    /// </summary>
    public virtual void onExit()
    {

    }

    /// <summary>
    ///���ڸ�״̬ʱÿ֡���õĺ���
    /// </summary>
    public virtual void updateLogic()
    {

    }


    /// <summary>
    /// ����֮ǰ��״̬����
    /// ����¼֮ǰ��״̬��ĳЩ״̬��Ҫ��ִ�к�ָ���֮ǰ��״̬��������Ҫ��¼��
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