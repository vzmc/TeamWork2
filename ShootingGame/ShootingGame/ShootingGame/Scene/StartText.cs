//==========================================
// 点滅Text
// 作成者：張ユービン
//==========================================

using Microsoft.Xna.Framework;
using MyLib.Device;
using MyLib.Utility;
using ShootingGame.Def;

namespace ShootingGame.Scene
{
    /// <summary>
    /// スタート押下、点滅表示テキスト
    /// </summary>
    class StartText
    {
        private Timer timer;
        private bool alphaFlag;
        private float alpha;

        public void Initialize()
        {
            //透明値切り替え時間は0.5sに設定
            timer = new Timer(0.5f);

            //true: 不透明
            alphaFlag = true;
            alpha = 1.0f;
        }

        public StartText()
        {
            Initialize();
        }

        public void Update(GameTime gameTime)
        {
            timer.Update(gameTime);

            if (timer.IsTime())
            {
                timer.Initialize();
                alphaFlag = !alphaFlag;

                alpha = alphaFlag ? 1.0f : 0.3f;
            }
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("GameStart", new Vector2(Screen.Width / 2-357, 660),  alpha);
        }
    }
}
