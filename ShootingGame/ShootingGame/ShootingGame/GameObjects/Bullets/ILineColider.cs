//==========================================
// 直線コライダーのインタフェース
// 作成者：張ユービン
//==========================================

using MyLib.Utility;

namespace ShootingGame.GameObjects
{
    interface ILineColider : IColider
    {
        Line GetColider();
    }
}
