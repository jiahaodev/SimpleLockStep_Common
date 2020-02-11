/****************************************************
	文件：BulletFactory.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 17:10   	
	功能：子弹工厂
*****************************************************/

using System.Collections;

public class BullectFactory {

    string m_scBulletName = "";

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="src">发射源</param>
    /// <param name="dest">射击目标</param>
    /// <param name="poSrc">发射的起始位置</param>
    /// <param name="poDst">发射的目标位置</param>
    public void createBullet(LiveObject src, LiveObject dest, FixVector3 poSrc, FixVector3 poDst) {
        BaseBullet bullet = null;

        //直射子弹
        bullet = new DirectionShootBullet();

        bullet.initData(src,dest,poSrc,poDst);
        bullet.createBody(m_scBulletName);
        bullet.shoot();

        if (bullet != null)
        {
            //刷新显示位置
            bullet.updateRenderPosition(0);
            //立即记录最后的位置，否则通过Vector3.lerp来进行移动动画是会出现画面抖动的bug
            bullet.recordLastPos();
            //加入子弹列表
            GameData.g_listBullet.Add(bullet);
        }

    }


    public void removeBullet(BaseBullet bullet) {
        GameData.g_listBullet.Remove(bullet);
    }


    public void loadProperties() {

    }

}
