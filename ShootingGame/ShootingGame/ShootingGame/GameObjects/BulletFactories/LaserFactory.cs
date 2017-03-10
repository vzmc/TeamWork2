//===============================================
// レーザー撃ち用の弾工場
// 作成者：葉梨
//==============================================

using Microsoft.Xna.Framework;
using ShootingGame.Scene;

namespace ShootingGame.GameObjects
{
    class LaserFactory : BulletFactory
    {
        public LaserFactory(Vector2 offset) : base(offset)
        {
            bulletList.Add(new Laser());
        }

        public LaserFactory(LaserFactory other)
            : this(other.offset)
        {
            bulletList = other.bulletList;
        }

        public override object Clone()
        {
            return new LaserFactory(this);
        }

        public override void Fire(Vector2 velocity)
        {
            Bullet newbullet = (Bullet)bulletList[0].Clone();
            newbullet.Position = owner.Position + offset;
            newbullet.Velocity = velocity;
            //撃っているやつを設定
            newbullet.SetOwner(owner);

            GamePlay.GetObjManager().AddToObjectWaitList(newbullet);
        }
    }
}

