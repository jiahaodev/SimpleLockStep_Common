/****************************************************
	�ļ���TowerFactory.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/11 17:38   	
	���ܣ�������
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
