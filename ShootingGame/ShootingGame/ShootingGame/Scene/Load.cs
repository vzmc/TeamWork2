//===================================
// リソースロードシーンクラス
// 作成者：張ユービン
//===================================

using Microsoft.Xna.Framework;
using MyLib.Device;

namespace ShootingGame.Scene
{
    class Load : IScene
    {
        private Renderer renderer;
        private Sound sound;
        private bool endFlag;

        private TextureLoader textureLoader;    // 画像Loadクラスインスタンス
        private BGMLoader bgmLoader;            // BGMLoadクラスインスタンス
        private SELoader seLoader;              // SELodeクラスインスタンス

        private int totalResouceNum;            //リソースの総数


        /// <summary>
        /// 画像の読み込みリスト作成
        /// </summary>
        /// <returns>画像の読み込みリスト</returns>
        private string[,] TextureList()
        {
            string path = "./Texture/";

            string[,] list = new string[,]
            {
                {"ending", path},
                {"player", path },
                {"enemy", path },
                {"circlebullet", path },
                {"titleBackGround",path },
                {"gameplay",path },
                {"main",path },
                {"meteorite",path },
                {"gametext", path },
                {"meteorite",path },
                {"ControlText",path },
                {"GameStart",path },
                {"TitleText",path },
                {"pinpoint",path },
                {"TitleText",path },
                {"fadein",path },
                {"fadein",path },
                {"laser",path },
                {"laserpoint",path },
                {"deadAnime", path },
                {"laserpoint",path },
                {"enemy",path },
                {"enemy2-1",path },
                {"enemy2-2",path },
                {"enemy3-2",path },
                {"enemy3-3",path },
                {"enemy3-5",path },
                {"backenemy",path },
                {"enemybullet",path },
                {"Gameend",path },
            };

            return list;
        }

        /// <summary>
        /// BGMの読み込みリスト作成
        /// </summary>
        /// <returns>BGMの読み込みリスト</returns>
        private string[,] BGMList()
        {
            string path = "./Sound/bgm/";
            string[,] list = new string[,]
            {
                {"bgm_title", path},
                {"bgm_play", path},
                {"bgm_ending",path},
            };

            return list;
        }

        /// <summary>
        /// SEの読み込みリスト作成
        /// </summary>
        /// <returns>SEの読み込みリスト</returns>
        private string[,] SEList()
        {
            string path = "./Sound/se/";
            string[,] list = new string[,]
            {
                {"se_effect1", path},
                {"se_effect2", path},
                {"se_effect3",path},
                {"se_over",path},
                {"se_start",path},
                {"beam",path},
                {"cursor",path},
                {"damage",path},
                {"direction",path},
                {"shot",path },
            };

            return list;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Load()
        {
            renderer = ShootingGame.GetGameDevice().GetRenderer();
            sound = ShootingGame.GetGameDevice().GetSound();

            textureLoader = new TextureLoader(renderer, TextureList());
            bgmLoader = new BGMLoader(sound, BGMList());
            seLoader = new SELoader(sound, SEList());
        }

        /// <summary>
        /// 描画（読み込み進行状態を表す）
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public void Draw(GameTime gameTime)
        {
            renderer.DrawTexture("LodingGround", Vector2.Zero);

            renderer.DrawTexture("load", new Vector2(20, 20));

            int currentCount = textureLoader.CurrentCount() + bgmLoader.CurrentCount() + seLoader.CurrentCount();
            if (totalResouceNum != 0)
            {
                renderer.DrawNumber(
                    "number",
                    new Vector2(20, 100),
                    (int)(currentCount / (float)totalResouceNum * 100.0f));
            }

            if (textureLoader.IsEnd() && bgmLoader.IsEnd() && seLoader.IsEnd())
            {
                endFlag = true;
            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            endFlag = false;
            textureLoader.Initialize();
            bgmLoader.Initialize();
            seLoader.Initialize();
            totalResouceNum = textureLoader.Count() + bgmLoader.Count() + seLoader.Count();
        }

        public void Update(GameTime gameTime)
        {
            if (!textureLoader.IsEnd())
            {
                textureLoader.Update();
            }
            else if (!bgmLoader.IsEnd())
            {
                bgmLoader.Update();
            }
            else if (!seLoader.IsEnd())
            {
                seLoader.Update();
            }
        }

        public bool IsEnd()
        {
            return endFlag;
        }

        public void LoadContent()
        {
        }

        public SceneType Next()
        {
            return SceneType.Title;
        }

        public void ShutDown()
        {
        }

        public void UnloadContent()
        {
        }
    }
}
