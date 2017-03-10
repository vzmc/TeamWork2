//================================================================
// 全方位撃ち
// 作成者：葉梨
// 修正者：張ユービン
//===============================================================

using Microsoft.Xna.Framework;
using ShootingGame.Scene;

namespace ShootingGame.GameObjects
{
    class CircleBulletFactory : BulletFactory
    {
        private Vector2 fireDirection;
        private Matrix rotation;

        public CircleBulletFactory(Vector2 offset)
            : base(offset)
        {
            bulletList.Add(new EnemyBulletTest());
            fireDirection = new Vector2(0, -1);
            rotation = Matrix.CreateRotationZ(MathHelper.ToRadians(30));

        }

        public CircleBulletFactory(CircleBulletFactory other)
            : this(other.offset)
        {
            bulletList = other.bulletList;
        }

        public override object Clone()
        {
            return new CircleBulletFactory(this);
        }

        public override void Fire(Vector2 velocity)
        {
            for(int i = 0;i<12;i++)
            {
                Bullet newbullet = (Bullet)bulletList[0].Clone();
                
                newbullet.Position = owner.Position;
                newbullet.Velocity = fireDirection * 300;
                fireDirection = Vector2.Transform(fireDirection, rotation);

                GamePlay.GetObjManager().AddToObjectWaitList(newbullet);
            }   
        }
    }
}