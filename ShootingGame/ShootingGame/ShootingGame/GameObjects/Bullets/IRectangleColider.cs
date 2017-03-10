//================================================
// 四角コライダーのインタフェース
// 作成者：張ユービン
//===============================================

using Microsoft.Xna.Framework;

namespace ShootingGame.GameObjects
{
    interface IRectangleColider : IColider
    {
        Rectangle GetColider();
    }
}
