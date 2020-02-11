/****************************************************
	文件：TowerFactory.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 17:38   	
	功能：塔工厂
*****************************************************/
using System.Collections;

public class TowerFactory {

    public BaseTower createTower() {
        BaseTower tower = new MagicTower();

        tower.changeState("towerstand");

        GameData.g_listTower.Add(tower);

        return tower;
    }

}
