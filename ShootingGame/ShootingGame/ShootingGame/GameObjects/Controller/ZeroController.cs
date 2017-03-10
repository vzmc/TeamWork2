//===========================================
// Playerに特攻するコントローラー
// 作成者：落合
// 修正者：張ユービン
//==========================================

using System;
using Microsoft.Xna.Framework;
using MyLib.Utility;
using ShootingGame.Def;
using ShootingGame.Scene;

namespace ShootingGame.GameObjects
{
    class ZeroController : IController
    {
        private Enemy target;
        private Timer fireTimer;
        private float fireInterval;
        private Vector2 destination;
        private static Random rand;
        private Vector2 playerPotion;

        private bool deathAttack;
        private bool deathFire;

        private Vector2 fireDirection;
        private Matrix rotation;

        public ZeroController(float fireInterval)
        {
            rand = new Random();
            this.fireInterval = fireInterval;
            fireTimer = new Timer(fireInterval);
            destination = new Vector2(rand.Next(100, Screen.GameWidth - 100), rand.Next(100, 250));
            deathAttack = false;

            fireDirection = new Vector2(0, 1);
            rotation = Matrix.CreateRotationZ(MathHelper.ToRadians(30));
        }
        public ZeroController(ZeroController other) 
            : this(other.fireInterval)
        {

        }

        public object Clone()
        {
            return new ZeroController(this);
        }

        public void SetTarget(GameObject obj)
        {
            target = (Enemy)obj;
        }

        public void GetControl(GameTime gameTime, ref Vector2 position, ref Vector2 velocity, ref BulletFactory bf)
        {
            Vector2 velo2 = Vector2.Subtract(destination, position);
            fireTimer.Update(gameTime);

            Vector2 distance = position - destination;

            if(deathAttack == false)
            {
                position += velo2 * (float)gameTime.ElapsedGameTime.TotalSeconds * Parameters.GameSpeed;
                if (distance.Length() < 50)
                {
                    deathAttack = true;
                    playerPotion = GamePlay.GetObjManager().GetPlayerPosition();
                    velocity = playerPotion - position;
                }
            }
            else
            {
                position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * Parameters.GameSpeed;
            }
            
            position.X = MathHelper.Clamp(position.X, 0, Screen.GameWidth);
        }

        public void GetDeathControl(GameTime gameTime, ref Vector2 position, ref Vector2 velocity, ref BulletFactory bf)
        {
            if (!deathFire)
            {
                for(int i = 0; i < 12; i++)
                {
                    bf.Fire(fireDirection * 200);
                    fireDirection = Vector2.Transform(fireDirection, rotation);
                }
                deathFire = true;
            }
        }

        public int GetPoint()
        {
            return 30;
        }
    }
}
