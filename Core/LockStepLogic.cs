/****************************************************
	文件：LockStopLogic.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 22:47   	
	功能：帧同步核心逻辑
*****************************************************/

using System.Collections;
using System.Collections.Generic;


public class LockStepLogic
{
    //累积运行的时间
    float m_fAccumilatedTime = 0;

    //下一个逻辑时间
    float m_fNextGameTime = 0;

    //预定的每帧的时间长度
    float m_fFrameLen;

    //两帧之间的时间差
    float m_fInterpolation = 0;

    //挂载的逻辑对象
    BattleLogic m_callUnit = null;

    public LockStepLogic() {
        init();
    }

    public void init() {
        m_fFrameLen = (float)GameData.g_fixFrameLen;
        m_fAccumilatedTime = 0;
        m_fNextGameTime = 0;
        m_fInterpolation = 0;
    }

    public void updateLogic() {
        float deltaTime = 0;
#if _CLIENTLOGIC_
        deltaTime = UnityEngine.Time.deltaTime;
#else
        deltaTime = 0.1f;
#endif
        /**************以下是帧同步的核心逻辑*********************/
        m_fAccumilatedTime += deltaTime;
        //如果真实累计的时间超过游戏帧逻辑原本应有的时间,则循环执行逻辑,确保整个逻辑的运算不会因为帧间隔时间的波动而计算出不同的结果
        while (m_fAccumilatedTime > m_fNextGameTime)
        {
            //运行与游戏相关的具体逻辑 
            m_callUnit.frameLockLogic();

            //计算 下一个逻辑帧应有的时间
            m_fNextGameTime += m_fFrameLen;

            //游戏逻辑帧自增
            GameData.g_uGameLogicFrame++;
        }

        //计算两帧之间的时间差，用于补间动画
        m_fInterpolation = (m_fAccumilatedTime + m_fFrameLen - m_fNextGameTime) / m_fNextGameTime;

        //更新绘制位置
        m_callUnit.updateRenderPosition(m_fInterpolation);

        /**************帧同步的核心逻辑完毕*********************/
    }

    //设置调用的宿主
    public void setCallUnit(BattleLogic unit) {
        m_callUnit = unit;
    }




}