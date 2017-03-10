//===================================
// レーザー
// 作成者：葉梨
// 修正者：張ユービン
//===================================

using Microsoft.Xna.Framework;
using MyLib.Device;
using MyLib.Utility;

namespace ShootingGame.GameObjects
{
    class Laser : Bullet, ILineColider
    {
        private Line colider;
        private Timer time;         //レーザーの準備時間
        private Timer time2;        //レーザーの継続時間
        private bool islaser;       //レーザー準備中: false,　継続中: true
        private Sound sound;
        private bool isSound = true;

        public Line GetColider()
        {
            return new Line(position + colider.point, colider.direction, colider.weight);
        }

        public Laser()
            : base("laser", BulletType.ENEMY, ColiderType.LINE, 50)
        {
            colider = new Line(Vector2.Zero, new Vector2(0, 1), 50);
            time = new Timer(1.0f);
            time2 = new Timer(1.0f);
            sound = ShootingGame.GetGameDevice().GetSound();
        }

        public Laser(Laser other)
            : this()
        {

        }

        public override object Clone()
        {
            return new Laser(this);
        }

        public override void HitEvent(GameObject gameObject)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (isDead)
            {
                return;
            }

            time.Update(gameTime);
            if (time.IsTime())
            {
                if (isSound)
                {
                    sound.PlaySE("beam");
                }
                islaser = true;
                isSound = false;
                time2.Update(gameTime);
                if (time2.IsTime())
                {
                    isDead = true;
                }
            }

            if (((Enemy)owner).NowState == State.DEATH)
            {
                isDead = true;
            }

            if (islaser == false)
            {
                colider.direction.Y = 0;
            }
            else
            {
                colider.direction.Y = 1;
            }

            position = owner.Position;
        }

        public override void Draw(Renderer renderer)
        {
            if (islaser == false)
            {
                renderer.DrawTexture("laserpoint", position, new Vector2(0.5f, 0));
            }
            else
            {
                renderer.DrawTexture(name, position, new Vector2(0.5f, 0));
            }
        }
    }
}
