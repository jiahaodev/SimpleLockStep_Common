/****************************************************
	文件：DirectionShootBullet.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/11 17:00   	
	功能：直射子弹
*****************************************************/

using System.Collections;

public class DirectionShootBullet : BaseBullet
{
    Fix64 m_fixMoveTiem = Fix64.Zero;
    Fix64 m_fixSpeed = Fix64.Zero;

    public override void updateLogic()
    {
        base.updateLogic();
    }

    public override void initData(LiveObject src, LiveObject dest, FixVector3 poSrc, FixVector3 poDst)
    {
        base.initData(src, dest, poSrc, poDst);

        Fix64 distance = FixVector3.Distance(poSrc, poDst);

        m_fixMoveTiem = distance / m_fixSpeed;
    }

    public override void shoot()
    {
        m_fixv3LogicPos = m_fixv3SrcPos;

        moveTo(m_fixv3SrcPos, m_fixv3DestPos, m_fixMoveTiem, delegate ()
        {
            doShootDest();
        });
    }

    public override void createBody(string name)
    {
        createFromPrefab("Prefabs/Bullet", this);

        this.name = name;
    }

    public override void loadProperties()
    {
        m_fixSpeed = (Fix64)10;
    }

}