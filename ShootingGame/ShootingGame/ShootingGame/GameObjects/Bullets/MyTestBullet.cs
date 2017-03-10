//===============================
// Playerの弾
// 作成者：張ユービン
//==============================

using Microsoft.Xna.Framework;
using MyLib.Device;
using MyLib.Utility;
using ShootingGame.Def;

namespace ShootingGame.GameObjects
{
    class MyTestBullet : Bullet, ICircleColider
    {
        //円形Colider
        private Circle colider;
        public Circle GetColider()
        {
            return new Circle(position + colider.center, colider.radious);
        }

        public MyTestBullet() : base("circlebullet", BulletType.PLAYER, ColiderType.CIRLCE, 10)
        {
            colider = new Circle(Vector2.Zero, ShootingGame.GetGameDevice().GetRenderer().Textures["circlebullet"].Width / 2);
        }

        public MyTestBullet(MyTestBullet other)
            : this()
        {
        }

        public override object Clone()
        {
            return new MyTestBullet(this);
        }

        public override void HitEvent(GameObject gameObject)
        {
            if(gameObject is Enemy)
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
            renderer.DrawTexture(name, position, drawOrigin);
        }
    }
}
