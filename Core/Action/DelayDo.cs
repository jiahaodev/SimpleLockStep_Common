/****************************************************
	文件：DelayDo.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/10 22:27   	
	功能：延迟执行的事件
*****************************************************/

public class DelayDo : BaseAction {

    Fix64 m_fixPlanTime;
    Fix64 m_fixElapseTime;

    public void init(Fix64 time,ActionCallback cb) {
        name = "DelayDo";
        m_fixPlanTime = time;
        actionCallbackFunc = cb;
    }

    public override void updateLogic() {
        m_fixElapseTime = m_fixElapseTime + GameData.g_fixFrameLen;
        if (m_fixElapseTime >= m_fixPlanTime)
        {
            if (actionCallbackFunc != null)
            {
                actionCallbackFunc();
            }
        }
    }
}
