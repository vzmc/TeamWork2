//================================
// ゲームプレイシーンクラス
// 作成者：張ユービン
//================================

using Microsoft.Xna.Framework;
using MyLib.Device;
using MyLib.Utility;
using ShootingGame.Maps;
using ShootingGame.GameObjects;

namespace ShootingGame.Scene
{
    class GamePlay : IScene
    {
        static private ObjectManager objmanager = new ObjectManager();  //ゲームオブジェクト管理クラスのインスタンスを生成
        private GameDevice gameDevice;
        private Renderer renderer;
        private Sound sound;
        private InputState input;
        private bool endFlag;
        private Map map;
        private Player player;
        private Timer endTime;

        public GamePlay()
        {
            gameDevice = ShootingGame.GetGameDevice();
            renderer = gameDevice.GetRenderer();
            sound = gameDevice.GetSound();
            input = gameDevice.GetInputState();
            map = new Map();
            endTime = new Timer(180.0f);
        }

        public void Initialize()
        {
            LoadContent();
            endFlag = false;
            sound.PlayBGM("bgm_play");

            //各の初期化
            map.Init();
            objmanager.Init();
            Score.Initialize();
            endTime.Initialize();

            //play生成
            player = new Player("player", 3, 300, new PlayerController(1), new PlayerBulletFactory(Vector2.Zero));
            player.Init();
            objmanager.AddToObjectWaitList(player);
        }

        public bool IsEnd()
        {
            return endFlag;
        }

        public SceneType Next()
        {
            return SceneType.Ending;
        }

        public void ShutDown()
        {
            sound.StopBGM();
            UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (player.IsDead)
            {
                endFlag = true;
            }
            endTime.Update(gameTime);
            map.Update(gameTime);
            objmanager.Update(gameTime);

            if (endTime.IsTime())
            {
                endFlag = true;
                endTime.Initialize();
            }
        }

        public void Draw(GameTime gameTime)
        {
            renderer.DrawTexture("gameplay", Vector2.Zero);
            map.Draw(renderer);
            objmanager.Draw(renderer);
            Score.Draw(renderer);
            renderer.DrawNumber("number", new Vector2(780, 400), (int)endTime.GetLimitTime() - (int)endTime.GetNowTime());
        }

        public void LoadContent()
        {
        }

        public void UnloadContent()
        {
        }

        /// <summary>
        /// ObjectManagerのインスタンスを返す
        /// </summary>
        /// <returns>ObjectManagerのインスタンス</returns>
        public static ObjectManager GetObjManager()
        {
            return objmanager;
        }
    }
}
