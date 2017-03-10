//========================================
// Player用の弾工場
// 作成者：張ユービン
//========================================

using Microsoft.Xna.Framework;
using ShootingGame.Scene;
using ShootingGame.Def;

namespace ShootingGame.GameObjects
{
    class PlayerBulletFactory : BulletFactory
    {
        private Matrix rotation;

        public PlayerBulletFactory(Vector2 offset) : base(offset)
        {
            bulletList.Add(new MyTestBullet());
        }

        public PlayerBulletFactory(PlayerBulletFactory other)
            : this(other.offset)
        {
            bulletList = other.bulletList;
        }

        public override object Clone()
        {
            return new PlayerBulletFactory(this);
        }

        public override void Fire(Vector2 velocity)
        {
            GameObject newbullet;

            if (Parameters.GameSpeed == 1)
            {
                newbullet = (GameObject)bulletList[0].Clone();
                newbullet.Position = owner.Position + offset;
                newbullet.Velocity = velocity;
                GamePlay.GetObjManager().AddToObjectWaitList(newbullet);
            }
            else
            {
                //強化弾
                for (int i = -1; i < 2; i++)
                {
                    newbullet = (GameObject)bulletList[0].Clone();
                    newbullet.Position = owner.Position + offset;
                    rotation = Matrix.CreateRotationZ(MathHelper.ToRadians(15 * i));
                    newbullet.Velocity = Vector2.Transform(velocity, rotation);
                    GamePlay.GetObjManager().AddToObjectWaitList(newbullet);
                }
            }
        }
    }
}
