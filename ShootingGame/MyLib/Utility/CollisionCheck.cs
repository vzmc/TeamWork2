//================================================
// 衝突判定用クラス
// 作成者：張ユービン
//================================================

using System;
using Microsoft.Xna.Framework;

namespace MyLib.Utility
{
    public static class CollisionCheck
    {
        /// <summary>
        /// 四角同士の衝突判定
        /// </summary>
        /// <param name="rect1">四角１</param>
        /// <param name="rect2">四角２</param>
        /// <returns>判定結果</returns>
        public static bool Check(Rectangle rect1, Rectangle rect2)
        {
            return rect1.Intersects(rect2);
        }

        /// <summary>
        /// 円形同士の衝突判定
        /// </summary>
        /// <param name="c1">円１</param>
        /// <param name="c2">円２</param>
        /// <returns>判定結果</returns>
        public static bool Check(Circle c1, Circle c2)
        {
            Vector2 vDistance = c1.center - c2.center;

            return vDistance.LengthSquared() < (c1.radious + c2.radious) * (c1.radious + c2.radious);
        }

        /// <summary>
        /// 四角と円の衝突判定
        /// </summary>
        /// <param name="rect">四角</param>
        /// <param name="c">円</param>
        /// <returns>判定結果</returns>
        public static bool Check(Rectangle rect, Circle c)
        {
            //未使用
            return false;
        }

        /// <summary>
        /// 直線と円の衝突判定
        /// </summary>
        /// <param name="line">直線</param>
        /// <param name="c">円</param>
        /// <returns>判定結果</returns>
        public static bool Check(Line line, Circle c)
        {
            if(line.direction == Vector2.Zero)
            {
                return false;
            }

            float distance = 0;

            Vector2 lineDirection = line.direction;
            lineDirection.Normalize();

            Vector2 distanceVector = c.center - line.point;

            //内積で位置判断
            float dot = Vector2.Dot(distanceVector, lineDirection);

            if(dot < 0)
            {
                //普通の点と点の距離
                distance = distanceVector.Length();
            }
            else
            {
                //外積を計算する
                distance = Math.Abs(distanceVector.X * lineDirection.Y - distanceVector.Y * lineDirection.X);
            }

            return distance < line.weight / 2 + c.radious;
        }
    }
}
