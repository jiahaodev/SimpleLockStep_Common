/****************************************************
	文件：MagicStand.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 17:33   	
	功能：魔法塔
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
