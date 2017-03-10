//==========================================
// 敵クラス
// 作成者：張ユービン
//==========================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;
using MyLib.Utility;
using ShootingGame.Def;
using ShootingGame.Scene;

namespace ShootingGame.GameObjects
{
    class Enemy : Character
    {
        private Sound sound;
        protected bool isAnim;

        public Enemy(string name, int hp, IController controller, BulletFactory bulletFactroy) : base(name, hp, controller, bulletFactroy)
        {
            nowState = State.NORMAL;
            sound = ShootingGame.GetGameDevice().GetSound();
            isAnim = false;
        }

        public Enemy(Enemy other)
            : this(other.name, other.hp, (IController)other.controller.Clone(), (BulletFactory)other.bulletFactory.Clone())
        {
        }

        public override object Clone()
        {
            return new Enemy(this);
        }

        public override void HitEvent(GameObject gameObject)
        {
            if (gameObject is Bullet)
            {
                nowHp -= ((Bullet)gameObject).Power;
            }

            if (nowHp <= 0)
            {
                nowState = State.DEATH;
                sound.PlaySE("damage");
                Score.AddPoint(controller.GetPoint());
            }
        }

        public override void Update(GameTime gameTime)
        {
            if(controller == null)
            {
                return;
            }

            if (nowState == State.NORMAL)
            {
                //通常コントロール
                controller.GetControl(gameTime, ref position, ref velocity, ref bulletFactory);
            }
            else if(nowState == State.DEATH)
            {
                //死亡後のコントロール
                controller.GetDeathControl(gameTime, ref position, ref velocity, ref bulletFactory);

                //アニメション処理
                if (animationPlayer.IsEnd())
                {
                    isDead = true;
                    return;
                }
                animationPlayer.Update(gameTime, Parameters.GameSpeed);
            }

            if(position.Y > Screen.GameHeight + 50)
            {
                isDead = true;
            }
            if (position.Y < -50)
            {
                isDead = true;
            }
        }

        public override void Draw(Renderer renderer)
        {
            if (nowState == State.NORMAL)
            {
                renderer.DrawTexture(name, position, drawOrigin);
            }
            else if (nowState == State.DEATH)
            {
                animationPlayer.Draw(renderer, position, drawOrigin);
            }
        }

        public int GetHp()
        {
            return hp;
        }
    }
}
