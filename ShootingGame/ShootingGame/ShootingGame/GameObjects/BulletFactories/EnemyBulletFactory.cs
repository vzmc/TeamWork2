//============================================
// 直線撃つのBulletFactory
// 作成者：張ユービン
//===========================================

using Microsoft.Xna.Framework;
using ShootingGame.Scene;

namespace ShootingGame.GameObjects
{
    class EnemyBulletFactory : BulletFactory
    {
        public EnemyBulletFactory(Vector2 offset) : base(offset)
        {
            bulletList.Add(new EnemyBulletTest());
        }

        public EnemyBulletFactory(EnemyBulletFactory other)
            : this(other.offset)
        {
            bulletList = other.bulletList;
        }

        public override object Clone()
        {
            return new EnemyBulletFactory(this);
        }

        public override void Fire(Vector2 velocity)
        {
            GameObject newbullet = (GameObject)bulletList[0].Clone();
            newbullet.Position = owner.Position + offset;
            newbullet.Velocity = velocity;

            GamePlay.GetObjManager().AddToObjectWaitList(newbullet);
        }
    }
}
