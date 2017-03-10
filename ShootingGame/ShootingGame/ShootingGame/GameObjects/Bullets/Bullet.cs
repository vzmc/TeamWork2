//===============================================
// 弾の基盤クラス
// 作成者：張ユービン
//===============================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;
using ShootingGame.Def;

namespace ShootingGame.GameObjects
{
    // 弾の種類（自機、敵、道具）
    public enum BulletType
    {
        PLAYER,
        ENEMY,
        ITEM,
    }

    // 弾のColiderの形（円形、四角形、直線）
    public enum ColiderType
    {
        CIRLCE,
        RECTANGLE,
        LINE,
    }

    abstract class Bullet : GameObject, IColider
    {
        //弾の種類
        protected BulletType bulletType;
        public BulletType BulletType
        {
            get
            {
                return bulletType;
            }
        }

        //弾の形
        protected ColiderType coliderType;
        public ColiderType ColiderType
        {
            get
            {
                return coliderType;
            }
        }

        //弾の威力
        protected int power;
        public int Power
        {
            get
            {
                return power;
            }
        }

        //撃っているやつ
        protected GameObject owner;
        public GameObject Owner
        {
            get
            {
                return owner;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="bulletType">弾の種類</param>
        /// <param name="coliderType">弾の形</param>
        /// <param name="power">威力</param>
        public Bullet(string name, BulletType bulletType, ColiderType coliderType, int power) : base(name)
        {
            this.bulletType = bulletType;
            this.coliderType = coliderType;
            this.power = power;
        }

        /// <summary>
        /// 画面外に出たら消す
        /// </summary>
        protected virtual void CheckIsScreenOut()
        {
            if(position.X < 0 || position.X > Screen.GameWidth || position.Y < 0 || position.Y > Screen.GameHeight)
            {
                isDead = true;
            }
        }

        /// <summary>
        /// 弾の状態設定
        /// </summary>
        /// <param name="position"></param>
        /// <param name="velocity"></param>
        protected virtual void SetState(Vector2 position, Vector2 velocity)
        {
            this.position = position;
            this.velocity = velocity;
        }
        /// <summary>
        /// 誰が撃っているか
        /// </summary>
        /// <param name="owner"></param>
        public virtual void SetOwner(GameObject owner)
        {
            this.owner = owner;
        }
    }
}
