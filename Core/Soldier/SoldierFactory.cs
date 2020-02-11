/****************************************************
	文件：SoldierFactory.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 17:26   	
	功能：士兵工厂
*****************************************************/
public class SoldierFactory
{

    public BaseSoldier createSoldier()
    {
        BaseSoldier soldier;

        soldier = new Grizzly();

        GameData.g_listSoldier.Add(soldier);

        //立即记录最后的位置,否则通过vector3.lerp来进行移动动画时会出现画面抖动的bug
        soldier.recordLastPos();

        return soldier;
    }
}