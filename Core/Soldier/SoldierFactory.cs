/****************************************************
	�ļ���SoldierFactory.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/11 17:26   	
	���ܣ�ʿ������
*****************************************************/
public class SoldierFactory
{

    public BaseSoldier createSoldier()
    {
        BaseSoldier soldier;

        soldier = new Grizzly();

        GameData.g_listSoldier.Add(soldier);

        //������¼����λ��,����ͨ��vector3.lerp�������ƶ�����ʱ����ֻ��涶����bug
        soldier.recordLastPos();

        return soldier;
    }
}