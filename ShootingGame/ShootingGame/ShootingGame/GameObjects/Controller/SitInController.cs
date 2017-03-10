//=============================================
// 回転撃ちのコントローラー
// 作成者：佐瀬
//============================================

using System;
using Microsoft.Xna.Framework;
using MyLib.Utility;
using ShootingGame.Def;

namespace ShootingGame.GameObjects
{
    class ShitInController : IController
    {
        private Enemy target;
        private Timer fireTimer;
        private float fireInterval;
        private Vector2 destination;
        private static Random rand;
        private float angle;
        private Vector2 direction;

        public ShitInController(float fireInterval)
        {
            rand = new Random();
            this.fireInterval = fireInterval;
            fireTimer = new Timer(fireInterval);
            destination = new Vector2(rand.Next(100, Screen.GameWidth - 100), rand.Next(100, 250));
            angle = 0;
        }
        public ShitInController(ShitInController other) 
            : this(other.fireInterval)
        {

        }

        public object Clone()
        {
            return new ShitInController(this);
        }

        public void SetTarget(GameObject obj)
        {
            target = (Enemy)obj;
        }

        public void GetControl(GameTime gameTime, ref Vector2 position, ref Vector2 velocity, ref BulletFactory bf)
        {
            Vector2 velo2 = Vector2.Subtract(destination, position);

            fireTimer.Update(gameTime, Parameters.GameSpeed);
            if (position != destination)
            {
                position += velo2 * (float)gameTime.ElapsedGameTime.TotalSeconds * Parameters.GameSpeed;
            }

            if (position.X < 0 || position.X > Screen.GameWidth)
            {
                velocity.X = -velocity.X;
            }

            position.X = MathHelper.Clamp(position.X, 0, Screen.GameWidth);

            if (fireTimer.IsTime())
            {
                fireTimer.Initialize();
                direction = new Vector2(((float)Math.Cos(angle * (Math.PI / 180))) - (float)Math.Sin(angle * (Math.PI / 180)), ((float)Math.Sin(angle * (Math.PI / 180))) + (float)Math.Cos(angle * (Math.PI / 180)));
                bf.Fire(direction * 70);
                angle += 12;
            }
        }

        public int GetPoint()
        {
            return 50;
        }

        public void GetDeathControl(GameTime gameTime, ref Vector2 position, ref Vector2 velocity, ref BulletFactory bf)
        {
        }
    }
}
