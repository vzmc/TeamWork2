//======================================================
// 基礎形状を定義クラス
// 作成者：張ユービン
//======================================================

using Microsoft.Xna.Framework;

namespace MyLib.Utility
{
    //円形
    public struct Circle
    {
        public Vector2 center;  //円形の中心座標
        public float radious;   //円形の半径

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="center">中心座標</param>
        /// <param name="radious">半径</param>
        public Circle(Vector2 center, float radious)
        {
            this.center = center;
            this.radious = radious;
        }
    }

    //直線
    public struct Line
    {
        public Vector2 point;       //直線上の一点
        public Vector2 direction;   //直線の方向を表すvector
        public float weight;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="point">直線上の一点</param>
        /// <param name="direction">直線の方向を表すvector</param>
        /// <param name="weight">直線の太さ（デフォルトは0）</param>
        public Line(Vector2 point, Vector2 direction, float weight = 0.0f)
        {
            this.point = point;
            this.direction = direction;
            this.weight = weight;
        }
    }
}
