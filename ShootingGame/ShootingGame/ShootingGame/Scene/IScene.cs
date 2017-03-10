//================================
// シーンクラスのインタフェース
// 作成者：張ユービン
//================================

using Microsoft.Xna.Framework;

namespace ShootingGame.Scene
{
    interface IScene
    {
        void Initialize();              //初期化
        void Update(GameTime gameTime); //更新
        void Draw(GameTime gameTime);   //描画
        void ShutDown();                //終了
        void LoadContent();
        void UnloadContent();

        //シーン管理
        bool IsEnd();                   //終了チェック
        SceneType Next();                   //次のシーン番号
    }
}
