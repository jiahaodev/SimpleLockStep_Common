/****************************************************
	�ļ���MagicStand.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/11 17:33   	
	���ܣ�ħ����
*****************************************************/

using System.Collections;

public class MagicTower : BaseTower
{

    public MagicTower()
    {
        init(); 
    }

    private void init()
    {
        loadProperties();

        createFromPrefab("Prefabs/Tower", this);

        name = "magictower";
    }

    public override void updateLogic()
    {
        base.updateLogic();
    }

    public override void loadProperties()
    {
        damage = (Fix64)50;
        attackRange = (Fix64)6 + GameData.g_srand.Range(1, 3);
        attackSpeed = (Fix64)1;
    }
}
