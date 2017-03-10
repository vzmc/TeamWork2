//==================================
// 直線運動の敵の弾
// 作成者：張ユービン
//==================================

using Microsoft.Xna.Framework;
using MyLib.Device;
using MyLib.Utility;
using ShootingGame.Def;

namespace ShootingGame.GameObjects
{
    class EnemyBulletTest : Bullet, ICircleColider
    {
        private Circle colider;
        public Circle GetColider()
        {
            return new Circle(position + colider.center, colider.radious);
        }

        public EnemyBulletTest() : base("enemybullet", BulletType.ENEMY, ColiderType.CIRLCE, 50)
        {
            colider = new Circle(Vector2.Zero, ShootingGame.GetGameDevice().GetRenderer().Textures["enemybullet"].Width / 2);
        }

        public EnemyBulletTest(EnemyBulletTest other)
            : this()
        {

        }

        public override object Clone()
        {
            return new EnemyBulletTest(this);
        }

        public override void HitEvent(GameObject gameObject)
        {
            if (gameObject is Player)
            {
                isDead = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (isDead)
            {
                return;
            }

            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * Parameters.GameSpeed;

            CheckIsScreenOut();
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position,  drawOrigin);
        }
    }
}
