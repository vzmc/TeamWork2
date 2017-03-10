//=======================================
// Title Scene
// 作成者：張ユービン
//=======================================

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyLib.Device;
using ShootingGame.Def;

namespace ShootingGame.Scene
{
    class Title : IScene
    {
        private GameDevice gameDevice;
        private Renderer renderer;
        private Sound sound;
        private InputState input;
        private StartText startText;

        private bool endFlag;

        public Title()
        {
            gameDevice = ShootingGame.GetGameDevice();
            renderer = gameDevice.GetRenderer();
            sound = gameDevice.GetSound();
            input = gameDevice.GetInputState();
            startText = new StartText();
            endFlag = false;
        }

        public void Draw(GameTime gameTime)
        {
            renderer.DrawTexture("titleBackGround", Vector2.Zero);
            renderer.DrawTexture("TitleText", new Vector2(Screen.Width / 2, 200), new Vector2(0.5f, 0.5f));
            renderer.DrawTexture("ControlText", new Vector2(Screen.Width / 2, 500), new Vector2(0.5f, 0.5f));

            startText.Draw(renderer);
        }

        public void Initialize()
        {
            endFlag = false;
            sound.PlayBGM("bgm_title");
        }

        public bool IsEnd()
        {
            return endFlag;
        }

        public SceneType Next()
        {
            return SceneType.GamePlay;
        }

        public void ShutDown()
        {
            sound.StopBGM();
        }

        public void LoadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            if (input.GetKeyTrigger(Keys.Space))
            {
                endFlag = true;
                sound.PlaySEInstance("direction", false);
            }
            startText.Update(gameTime);
        }

        public void UnloadContent()
        {
        }
    }
}
