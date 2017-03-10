using Microsoft.Xna.Framework;
using ShootingGame.Scene;

namespace ShootingGame.GameObjects
{
    class EnemyDiffusionBulletFactory : BulletFactory
    {
        public EnemyDiffusionBulletFactory(Vector2 offset) : base(offset)
        {
            bulletList.Add(new DiffusionBullet());
        }

        public EnemyDiffusionBulletFactory(EnemyDiffusionBulletFactory other)
            : this(other.offset)
        {
            bulletList = other.bulletList;
        }

        public override object Clone()
        {
            return new EnemyDiffusionBulletFactory(this);
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
