/****************************************************
	�ļ���Grizzly.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/11 17:23   	
	���ܣ�������
*****************************************************/

public class Grizzly : BaseSoldier
{

    public Grizzly()
    {
        loadProperties();

        base.createFromPrefab("Prefabs/Soldier", this);

        name = "grizzly";
    }

    public override void updateLogic()
    {
        base.updateLogic();
    }

    public override void loadProperties()
    {
        hp = (Fix64)800;
    }

}