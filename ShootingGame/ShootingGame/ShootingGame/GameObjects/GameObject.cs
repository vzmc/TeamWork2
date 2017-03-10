//=========================================================
// ゲーム内の物体の基盤クラス
// 作成者：張ユービン
//=========================================================

using System;
using Microsoft.Xna.Framework;
using MyLib.Device;

namespace ShootingGame.GameObjects
{
    public abstract class GameObject : ICloneable  //コピー機能を追加
    {
        protected string name;          //使用する画像のアセット名
        protected Vector2 drawOrigin;   //描画の基準座標

        protected bool isDead;          //死亡判断Flag
        public bool IsDead
        {
            get
            {
                return isDead;
            }
        }

        protected Vector2 velocity;     //速度
        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }

        protected Vector2 position;     //位置座標
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        /// <summary>
        /// コンストラクタ（DrawOriginデフォルトは0.5,0.5）
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置座標</param>
        /// <param name="velocity">速度</param>
        public GameObject(string name)
        {
            this.name = name;
            isDead = false;
            drawOrigin = new Vector2(0.5f, 0.5f);
            SetState(Vector2.Zero, Vector2.Zero);
        }

        public void SetState(Vector2 position, Vector2 velocity)
        {
            this.position = position;
            this.velocity = velocity;
        }

        //抽象メソッド
        public abstract object Clone(); //ICloneableが必ず必要
        public abstract void HitEvent(GameObject gameObject);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(Renderer renderer);
    }
}
