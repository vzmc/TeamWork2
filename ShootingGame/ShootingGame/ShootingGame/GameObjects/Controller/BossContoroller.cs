//=================================
// Boss用のコントローラー（未完成）
// 作成者：佐瀬
//=================================

using System;
using Microsoft.Xna.Framework;
using MyLib.Utility;
using ShootingGame.Def;

namespace ShootingGame.GameObjects
{
    enum BossState
    {
        START,
        BOSS1,
        BOSS2,

    }
    class BossContoroller : IController
    {
        private Enemy target;
        private Timer timer;
        private Timer fireTimer;
        private Vector2 destination;
        private float fireInterval;
        private BossState state;
        private float angle;
        private Vector2 direction;


        public BossContoroller(float fireInterval)
        {
            this.fireInterval = fireInterval;
            timer = new Timer(fireInterval);
            fireTimer = new Timer(fireInterval);
            destination = new Vector2((Screen.GameWidth/2),50);
            state = BossState.START;
        }

        public BossContoroller(BossContoroller other)
            :this(other.fireInterval)
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

            if (target.NowHp() > target.GetHp() / 2)
            {
                if (position.X < 0 || position.X > Screen.GameWidth)
                {
                    velocity.X = -velocity.X;
                }
            }

            else if(target.NowHp() <= target.GetHp()/2)
            {
                //Vector2 velo2 = Vector2.Subtract(destination, position);
                //fireTimer.Update(gameTime);
                if (position != destination)
                {
                    position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * Parameters.GameSpeed;
                }

                if (position.X < 0 || position.X > Screen.GameWidth)
                {
                    velocity.X = -velocity.X;
                }
            }
            
            position.X = MathHelper.Clamp(position.X, 0, Screen.GameWidth);
            position.Y = MathHelper.Clamp(position.Y, 0, Screen.GameHeight);            

            if (timer.IsTime())
            {
                if (target.NowHp() > target.GetHp() / 2)
                {
                    timer.Initialize();
                    direction = new Vector2(((float)Math.Cos(angle * (Math.PI / 180))) - (float)Math.Sin(angle * (Math.PI / 180)), ((float)Math.Sin(angle * (Math.PI / 180))) + (float)Math.Cos(angle * (Math.PI / 180)));
                    bf.Fire(direction * 70);
                    angle += 12;

                }
                else if (target.NowHp() <= target.GetHp() / 2)
                {                    
                    timer.Initialize();
                    bf.Fire(new Vector2(0, 1) * 150);
                }
                
            }
        }

        public object Clone()
        {
            return new BossContoroller(this);
        }

        public int GetPoint()
        {
            return 10000;
        }

        public void GetDeathControl(GameTime gameTime, ref Vector2 position, ref Vector2 velocity, ref BulletFactory bf)
        {
        }
    }
}
