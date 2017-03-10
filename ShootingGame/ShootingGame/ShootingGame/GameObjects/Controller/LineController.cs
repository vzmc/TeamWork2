//====================================
// 直線運動コントローラー
// 作成者：張ユービン
//===================================

using Microsoft.Xna.Framework;
using MyLib.Utility;
using ShootingGame.Def;

namespace ShootingGame.GameObjects
{
    class LineController : IController
    {
        private Enemy target;       //コントロールする対象
        private Timer timer;        
        private float fireInterval; //撃つ間隔


        public LineController(float fireInterval)
        {
            this.fireInterval = fireInterval;
            timer = new Timer(fireInterval);
        }

        public LineController(LineController other) 
            : this(other.fireInterval)
        {
        }

        public void SetTarget(GameObject obj)
        {
            target = (Enemy)obj;
        }

        public void GetControl(GameTime gameTime, ref Vector2 position, ref Vector2 velocity, ref BulletFactory bf)
        {
            timer.Update(gameTime, Parameters.GameSpeed);

            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * Parameters.GameSpeed;

            if (position.X < 0 || position.X > Screen.GameWidth)
            {
                velocity.X = -velocity.X;
            }

            position.X = MathHelper.Clamp(position.X, 0, Screen.GameWidth);

            if (timer.IsTime())
            {
                timer.Initialize();
                bf.Fire(new Vector2(0,1) * 200);
            }
        }

        public object Clone()
        {
            return new LineController(this);
        }

        public int GetPoint()
        {
            return 10;
        }

        public void GetDeathControl(GameTime gameTime, ref Vector2 position, ref Vector2 velocity, ref BulletFactory bf)
        {
        }
    }
}
