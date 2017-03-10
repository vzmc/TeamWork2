//==============================================================
// エンディングシーンクラス
// 作成者：張ユービン
//==============================================================

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyLib.Device;
using ShootingGame.GameObjects;

namespace ShootingGame.Scene
{
    class Ending : IScene
    {
        private GameDevice gameDevice;
        private Renderer renderer;
        private Sound sound;
        private InputState input;
        private IScene playScene;
        private bool endFlag;

        public Ending(IScene playScene)
        {
            gameDevice = ShootingGame.GetGameDevice();
            this.playScene = playScene;
            renderer = gameDevice.GetRenderer();
            sound = gameDevice.GetSound();
            input = gameDevice.GetInputState();
        }

        public void Draw(GameTime gameTime)
        {
            playScene.Draw(gameTime);
            renderer.DrawTexture("meteorite", Vector2.Zero);
            renderer.DrawTexture("Gameend", new Vector2(0, 200));
            Score.Draw(renderer);
        }

        public void Initialize()
        {
            endFlag = false;
            sound.PlayBGM("bgm_ending");
            Score.SaveHighScore();
        }

        public bool IsEnd()
        {
            return endFlag;
        }

        public SceneType Next()
        {
            return SceneType.Title;
        }

        public void ShutDown()
        {
            sound.StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            if (input.GetKeyTrigger(Keys.Space))
            {
                endFlag = true;
            }
        }

        public void LoadContent()
        {
            
        }

        public void UnloadContent()
        {
        }
    }
}
