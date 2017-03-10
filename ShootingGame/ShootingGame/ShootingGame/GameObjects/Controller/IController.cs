//========================================================
// コントローラー　インタフェース
// 作成者：張ユービン
//=======================================================
using System;
using Microsoft.Xna.Framework;

namespace ShootingGame.GameObjects
{
    interface IController : ICloneable
    {
        /// <summary>
        /// コントロールする対象を設定する
        /// </summary>
        /// <param name="obj">対象</param>
        void SetTarget(GameObject obj);

        /// <summary>
        /// コントロールする
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        /// <param name="position">位置変数の参照</param>
        /// <param name="velocity">速度変数の参照</param>
        /// <param name="bf">弾生成工場の参照</param>
        void GetControl(GameTime gameTime, ref Vector2 position, ref Vector2 velocity, ref BulletFactory bf);

        /// <summary>
        /// 死亡後のコントロール処理
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="position"></param>
        /// <param name="velocity"></param>
        /// <param name="bf"></param>
        void GetDeathControl(GameTime gameTime, ref Vector2 position, ref Vector2 velocity, ref BulletFactory bf);

        /// <summary>
        /// 得られる得点を渡す
        /// </summary>
        /// <returns>得点</returns>
        int GetPoint();
    }
}
