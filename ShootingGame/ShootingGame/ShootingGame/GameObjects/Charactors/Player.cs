//=========================================
// プレイヤークラス
// 作成者：張ユービン
//========================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;
using MyLib.Utility;
using ShootingGame.Def;

namespace ShootingGame.GameObjects
{
    class Player : Character
    {
        private Timer startTimer;           //開始中Timer
        private Timer invincibleTimer;      //無敵状態Timer
        private Timer flashTimer;           //点滅間隔Timer
        private float alpha;

        private float moveSpeed;
        private Sound sound;
        public float MoveSpeed
        {
            get
            {
                return moveSpeed;
            }
        }

        private Circle safeColider;
        public Circle SafeColider
        {
            get
            {
                return new Circle(safeColider.center + position, safeColider.radious);
            }
        }
        
        public Player(string name, int hp, float moveSpeed, IController controller, BulletFactory bulletFactory) : base(name, hp, controller, bulletFactory)
        {
            safeColider = damegeColider;
            damegeColider = new Circle(Vector2.Zero, 1f);
            this.moveSpeed = moveSpeed;
            nowState = State.START;
            startTimer = new Timer(1.0f);
            flashTimer = new Timer(0.1f);
            invincibleTimer = new Timer(1.5f);
            alpha = 1.0f;
            sound = ShootingGame.GetGameDevice().GetSound();
        }
        
        public Player(Player other)
            : this(other.name, other.hp, other.moveSpeed, (IController)other.controller.Clone(), (BulletFactory)other.bulletFactory.Clone())
        {
        }

        public override object Clone()
        {
            return new Player(this);
        }

        public override void HitEvent(GameObject gameObject)
        {
            nowHp -= 1;
            sound.PlaySE("damage");
            nowState = State.DEATH;
            animationPlayer.ResetAnimation();
        }

        public void Init()
        {
            SetState(new Vector2(Screen.GameWidth / 2, Screen.GameHeight), new Vector2(0, -1) * 150); 
            nowState = State.START;
        }

        public void SafeHitEvent(GameObject obj)
        {
            Parameters.GameSpeed = 0.5f;
        }

        public override void Update(GameTime gameTime)
        {
            switch (nowState)
            {
                case State.START:
                    {
                        position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * Parameters.GameSpeed;
                        startTimer.Update(gameTime, Parameters.GameSpeed);
                        flashTimer.Update(gameTime, Parameters.GameSpeed);

                        if (flashTimer.IsTime())
                        {
                            flashTimer.Initialize();
                            alpha = alpha == 1.0f ? 0.0f : 1.0f;
                        }

                        if (startTimer.IsTime())
                        {
                            startTimer.Initialize();
                            nowState = State.INVINCIBLE;
                            alpha = 1.0f;
                        }
                        break;
                    }
                case State.INVINCIBLE:
                    {
                        controller.GetControl(gameTime, ref position, ref velocity, ref bulletFactory);
                        invincibleTimer.Update(gameTime, Parameters.GameSpeed);
                        flashTimer.Update(gameTime, Parameters.GameSpeed);

                        if (flashTimer.IsTime())
                        {
                            flashTimer.Initialize();
                            alpha = alpha == 1.0f ? 0.0f : 1.0f;
                        }

                        if (invincibleTimer.IsTime())
                        {
                            invincibleTimer.Initialize();
                            nowState = State.NORMAL;
                            alpha = 1.0f;
                        }
                        break;
                    }
                case State.NORMAL:
                    {
                        controller.GetControl(gameTime, ref position, ref velocity, ref bulletFactory);
                        break;
                    }
                case State.DEATH:
                    {
                        animationPlayer.Update(gameTime, Parameters.GameSpeed);
                        if (animationPlayer.IsEnd())
                        {
                            if (nowHp <= 0)
                            {
                                isDead = true;
                            }
                            else
                            {
                                Init();
                            }
                        }
                        break;
                    }
            }
        }

        public override void Draw(Renderer renderer)
        {
            if(nowState == State.NORMAL || nowState == State.START || nowState == State.INVINCIBLE)
            {
                renderer.DrawTexture(name, position, drawOrigin, alpha);
                renderer.DrawTexture("pinpoint", position, drawOrigin, alpha);
            }
            else if(nowState == State.DEATH)
            {
                animationPlayer.Draw(renderer, position, drawOrigin);
            }

            for (int i = 0; i < nowHp; i++)
            {
                renderer.DrawTexture("player", new Vector2(720 + (i * 64), 300));
            }
        }
    }
}
