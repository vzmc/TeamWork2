//==================================================
// Playerの位置に向かって撃つ
// 作成者：葉梨
//==================================================

using Microsoft.Xna.Framework;
using ShootingGame.Scene;

namespace ShootingGame.GameObjects
{
    class ChaseBulletFactory : BulletFactory
    {
        private Vector2 player;
        public ChaseBulletFactory(Vector2 offset)
            : base(offset)
        {
            bulletList.Add(new EnemyBulletTest());
        }

        public ChaseBulletFactory(ChaseBulletFactory other)
            : this(other.offset )
        {
            bulletList = other.bulletList;
        }

        public override object Clone()
        {
            return new ChaseBulletFactory(this);
        }

        public override void Fire(Vector2 velocity)
        {
            GameObject newbullet = (GameObject)bulletList[0].Clone();
            
            newbullet.Position = owner.Position + offset;

            player = GamePlay.GetObjManager().GetPlayerPosition();
            Vector2 velo = player - newbullet.Position;
            velo.Normalize();
            newbullet.Velocity = velo * velocity.Y;

            GamePlay.GetObjManager().AddToObjectWaitList(newbullet);
        }
    }
}
