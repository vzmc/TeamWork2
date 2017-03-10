//===================================
// 左右横移動コントローラー
// 作成者：佐瀬
//===================================

using Microsoft.Xna.Framework;
using MyLib.Utility;
using ShootingGame.Def;

namespace ShootingGame.GameObjects
{
    class RepeatMoveController : IController
    {
        private Enemy target;
        private Timer moveTimer;
        private Timer fireTimer;
        private float fireInterval;

        public RepeatMoveController(float fireInterval)
        {
            this.fireInterval = fireInterval;
            fireTimer = new Timer(fireInterval);
            moveTimer = new Timer(0.5f);
        }

        public RepeatMoveController(RepeatMoveController other) 
            : this(other.fireInterval)
        {
        }

        public void SetTarget(GameObject obj)
        {
            target = (Enemy)obj;
        }

        public void GetControl(GameTime gameTime, ref Vector2 position, ref Vector2 velocity, ref BulletFactory bf)
        {
            fireTimer.Update(gameTime, Parameters.GameSpeed);
            moveTimer.Update(gameTime, Parameters.GameSpeed);

            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * new Vector2(2, 1) * Parameters.GameSpeed;

            if (moveTimer.IsTime())
            {
                velocity.X = -velocity.X;
                moveTimer.Initialize();
            }
            if (position.X < 0 || position.X > Screen.GameWidth)
            {
                velocity.X = -velocity.X;
                moveTimer.Initialize();
            }
            position.X = MathHelper.Clamp(position.X, 0, Screen.GameWidth);

            if (fireTimer.IsTime())
            {
                fireTimer.Initialize();
                bf.Fire(new Vector2(0, 1) * 180);
            }
        }

        public object Clone()
        {
            return new RepeatMoveController(this);
        }

        public int GetPoint()
        {
            return 20;
        }

        public void GetDeathControl(GameTime gameTime, ref Vector2 position, ref Vector2 velocity, ref BulletFactory bf)
        {
        }
    }
}
