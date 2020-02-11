/****************************************************
	文件：Grizzly.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 17:23   	
	功能：豺狼人
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