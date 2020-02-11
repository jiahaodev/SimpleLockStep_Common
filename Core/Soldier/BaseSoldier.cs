/****************************************************
	�ļ���BaseSoldier.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/11 17:18   	
	���ܣ�ʿ�� ����
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
        //��ת���ƶ�״̬
        changeState("solidermove");
    }

    public virtual void updateLogic() {
        m_statemachine.updateLogic();

        checkIsDead();
        checkEvent();
    }


    //���״̬
    //����ȴ״̬�����󣬼��һ�µ�ǰ��״̬���Ա���ݵ�ǰ״̬ˢ���߼�
    public override void checkState()
    {
        
    }

}
