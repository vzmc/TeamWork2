//==================================================
// キャラクター系のベースクラス
// 作成者：張ユービン
//==================================================

using Microsoft.Xna.Framework;
using MyLib.Utility;
using MyLib.Device;

namespace ShootingGame.GameObjects
{
    public enum State
    {
        NORMAL,         //一般状態
        START,          //開始状態
        TODEAD,         //死亡の瞬間
        DEATH,          //死亡状態
        INVINCIBLE,     //無敵状態
    }

    abstract class Character : GameObject
    {
        protected BulletFactory bulletFactory;      //弾生成機構
        protected IController controller;           //コントローラー
        protected int hp;                           //HP
        protected int nowHp;                        //NowHp
        protected Circle damegeColider;             //ダメージを受けるColider（相対位置）
        public Circle DamegeColider                 //ダメージを受けるColider（全体位置）
        {
            get
            {
                return new Circle(position + damegeColider.center, damegeColider.radious);
            }
        }

        protected State nowState;                   //現在の状態
        public State NowState
        {
            get
            {
                return nowState;
            }
        }

        //死亡アニメション
        protected Animation deadAnime;
        protected AnimationPlayer animationPlayer;

        public Character(string name, int hp, IController controller, BulletFactory bulletFactory = null) 
            : base(name)
        {
            this.hp = hp;
            nowHp = hp;
            SetDamegeColider(new Circle(Vector2.Zero, ShootingGame.GetGameDevice().GetRenderer().Textures[name].Width / 2));
            SetEquip(controller, bulletFactory);

            //アニメションを設置
            deadAnime = new Animation(ShootingGame.GetGameDevice().GetRenderer(), "deadAnime", 0.1f, false);
            animationPlayer.SetAnimation(deadAnime);
        }

        /// <summary>
        /// コントローラーと弾生成機構両方変更
        /// </summary>
        /// <param name="controller">コントローラー</param>
        /// <param name="bulletFactory">弾生成機構</param>
        public void SetEquip(IController controller, BulletFactory bulletFactory)
        {
            SetBulletFactory(bulletFactory);
            SetController(controller);
        }

        /// <summary>
        /// 弾生成機構を変更
        /// </summary>
        /// <param name="bulletFactory">BulletFactory</param>
        public void SetBulletFactory(BulletFactory bulletFactory)
        {
            if(bulletFactory == null)
            {
                return;
            }
            this.bulletFactory = bulletFactory;
            this.bulletFactory.SetOwner(this);
        }

        /// <summary>
        /// コントローラーを変更
        /// </summary>
        /// <param name="controller">コントローラー</param>
        public void SetController(IController controller)
        {
            if(controller == null)
            {
                return;
            }
            this.controller = controller;
            this.controller.SetTarget(this);
        }

        /// <summary>
        /// ダメージ受けColiderを変更
        /// </summary>
        /// <param name="colider">Circle</param>
        public void SetDamegeColider(Circle colider)
        {
            damegeColider = colider;
        }

        public int NowHp()
        {
            return nowHp;
        }
    }
}
