/****************************************************
	文件：BaseSoldier.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 17:18   	
	功能：士兵 基类
*****************************************************/

using System.Collections;

public class BaseSoldier : LiveObject {

    public BaseSoldier() {
        init();
    }

    private void init() {
        m_scType = "soldier";
        m_statemachine = new StateMachine();
        m_statemachine.setUnit(this);
    }

    public void move() {
        //跳转到移动状态
        changeState("solidermove");
    }

    public virtual void updateLogic() {
        m_statemachine.updateLogic();

        checkIsDead();
        checkEvent();
    }


    //检测状态
    //在冷却状态结束后，检测一下当前的状态，以便根据当前状态刷新逻辑
    public override void checkState()
    {
        
    }

}
