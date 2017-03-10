//======================================
// 指定位置到達後に撃って戻る
// 作成者：佐瀬
// 修正者：張ユービン
//======================================

using System;
using Microsoft.Xna.Framework;
using MyLib.Utility;
using ShootingGame.Def;

namespace ShootingGame.GameObjects
{
    class ReturnController : IController
    {
        private Enemy target;
        private Timer fireTimer;
        private float fireInterval;
        private Vector2 destination;
        private static Random rand;
        private bool isGetDestination;
        private bool isFire;
        private Timer waitFire;

        public ReturnController(float fireInterval)
        {
            rand = new Random();
            this.fireInterval = fireInterval;
            fireTimer = new Timer(fireInterval);
            destination = new Vector2(rand.Next(100, Screen.GameWidth - 100), rand.Next(100, 300));
            isGetDestination = false;
            isFire = false;
            waitFire = new Timer(2.0f);
        }

        public ReturnController(ReturnController other)
            : this(other.fireInterval)
        {

        }

        public object Clone()
        {
            return new ReturnController(this);
        }

        public void SetTarget(GameObject obj)
        {
            target = (Enemy)obj;
        }

        public void GetControl(GameTime gameTime, ref Vector2 position, ref Vector2 velocity, ref BulletFactory bf)
        {
            Vector2 velo2 = Vector2.Subtract(destination, position);

            fireTimer.Update(gameTime, Parameters.GameSpeed);
            if (isFire)
            {
                waitFire.Update(gameTime);
            }
            if (isGetDestination && waitFire.IsTime())
            {
                position += new Vector2(0, -5) * Parameters.GameSpeed;
            }
            if (position != destination && isGetDestination == false)
            {
                position += velo2 * (float)gameTime.ElapsedGameTime.TotalSeconds * Parameters.GameSpeed;
            }
            if (Vector2.Distance(position, destination) <= 50)
            {
                isGetDestination = true;
            }

            if (position.X < 0 || position.X > Screen.GameWidth)
            {
                velocity.X = -velocity.X;
            }

            position.X = MathHelper.Clamp(position.X, 0, Screen.GameWidth);

            if (isGetDestination && fireTimer.IsTime() && isFire == false)
            {
                bf.Fire(new Vector2(0, 1) * 180);
                fireTimer.Initialize();
                isFire = true;
            }
        }

        public int GetPoint()
        {
            return 100;
        }

        public void GetDeathControl(GameTime gameTime, ref Vector2 position, ref Vector2 velocity, ref BulletFactory bf)
        {
        }
    }
}
