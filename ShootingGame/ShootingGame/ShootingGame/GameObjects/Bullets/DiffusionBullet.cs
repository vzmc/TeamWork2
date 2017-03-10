//===========================================
// 拡散弾
// 作成者：葉梨
//===========================================

using System;
using Microsoft.Xna.Framework;
using MyLib.Device;
using MyLib.Utility;
using ShootingGame.Def;
using ShootingGame.Scene;

namespace ShootingGame.GameObjects
{
    class DiffusionBullet : Bullet, ICircleColider
    {
        private Circle colider;
        private Timer fire;
        public Circle GetColider()
        {
            return new Circle(position + colider.center, colider.radious);
        }

        public DiffusionBullet() : base("enemybullet", BulletType.ENEMY, ColiderType.CIRLCE, 50)
        {
            colider = new Circle(Vector2.Zero, ShootingGame.GetGameDevice().GetRenderer().Textures["enemybullet"].Width / 2);
            fire = new Timer(1.0f);
        }

        public DiffusionBullet(DiffusionBullet other)
            : this()
        {

        }

        public override object Clone()
        {
            return new DiffusionBullet(this);
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

            fire.Update(gameTime);
            if (fire.IsTime())
            {
                double angle = 0;
                for (int i = 0; i < 6; i++)
                {
                    GameObject newbullet = new EnemyBulletTest();

                    newbullet.Position = position;

                    Vector2 velo = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    newbullet.Velocity = velo * 150;

                    GamePlay.GetObjManager().AddToObjectWaitList(newbullet);

                    angle += 45;
                    isDead = true;
                }
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
