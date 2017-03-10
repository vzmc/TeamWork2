//======================================
// 円形コライダーのインタフェース
// 作成者：張ユービン
//======================================

using MyLib.Utility;

namespace ShootingGame.GameObjects
{
    interface ICircleColider : IColider
    {
        Circle GetColider();
    }
}
