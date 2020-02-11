/****************************************************
	�ļ���BulletFactory.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/11 17:10   	
	���ܣ��ӵ�����
*****************************************************/

using System.Collections;

public class BullectFactory {

    string m_scBulletName = "";

    /// <summary>
    /// ��ʼ������
    /// </summary>
    /// <param name="src">����Դ</param>
    /// <param name="dest">���Ŀ��</param>
    /// <param name="poSrc">�������ʼλ��</param>
    /// <param name="poDst">�����Ŀ��λ��</param>
    public void createBullet(LiveObject src, LiveObject dest, FixVector3 poSrc, FixVector3 poDst) {
        BaseBullet bullet = null;

        //ֱ���ӵ�
        bullet = new DirectionShootBullet();

        bullet.initData(src,dest,poSrc,poDst);
        bullet.createBody(m_scBulletName);
        bullet.shoot();

        if (bullet != null)
        {
            //ˢ����ʾλ��
            bullet.updateRenderPosition(0);
            //������¼����λ�ã�����ͨ��Vector3.lerp�������ƶ������ǻ���ֻ��涶����bug
            bullet.recordLastPos();
            //�����ӵ��б�
            GameData.g_listBullet.Add(bullet);
        }

    }


    public void removeBullet(BaseBullet bullet) {
        GameData.g_listBullet.Remove(bullet);
    }


    public void loadProperties() {

    }

}
