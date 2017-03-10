//===========================================
// 弾生成工場基盤クラス
// 作成者：張ユービン
//==========================================

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ShootingGame.GameObjects
{
    abstract class BulletFactory : ICloneable
    {
        protected List<Bullet> bulletList;      //弾を格納するList
        protected GameObject owner;             //持ち主
        protected Vector2 offset;               //持ち主位置座標からの偏差値
        protected int level;                    //未使用

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="offset">物体との相対位置</param>
        public BulletFactory(Vector2 offset)
        {
            bulletList = new List<Bullet>();
            this.offset = offset;
            level = 1;
        }

        /// <summary>
        /// オーナーを設置
        /// </summary>
        /// <param name="owner">オーナー</param>
        public void SetOwner(GameObject owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// 撃つ処理
        /// </summary>
        /// <param name="direction">撃つ方向</param>
        abstract public void Fire(Vector2 dirction);
        abstract public object Clone();
    }
}
