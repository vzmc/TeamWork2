using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyLib.Device
{
    /// <summary>
    /// ゲーム共有クラス
    /// </summary>
    public class GameDevice
    {
        public static readonly Random Rand = new Random();  //ランダム生成オブジェクト
        private Renderer renderer;           //描画オブジェクト
        private InputState input;            //入力オブジェクト
        private Sound sound;                 //音声オブジェクト

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="content">コンテンツ管理者</param>
        /// <param name="graphics">グラフィックデバイス</param>
        public GameDevice(ContentManager content, GraphicsDevice graphics)
        {
            renderer = new Renderer(content, graphics);
            input = new InputState();
            sound = new Sound(content);
        }

        /// <summary>
        /// リソースの読み込み
        /// </summary>
        public void LoadContent()
        {
            //ゲーム開始時に必要な最小限のリソースを読み込む
            renderer.LoadTexture("number", "./Texture/");
            renderer.LoadTexture("load", "./Texture/");
            renderer.LoadTexture("LodingGround", "./Texture/");
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            input.Update();
            sound.Update();
        }

        /// <summary>
        /// 資源を外す
        /// </summary>
        public void UnloadContent()
        {
            renderer.Unload();
            sound.Unload();
        }

        /// <summary>
        /// Soundインスタンスを取得
        /// </summary>
        /// <returns>Soundインスタンス</returns>
        public Sound GetSound()
        {
            return sound;
        }

        /// <summary>
        /// Rendererインスタンスを取得
        /// </summary>
        /// <returns>Rendererインスタンス</returns>
        public Renderer GetRenderer()
        {
            return renderer;
        }

        /// <summary>
        /// InputStateインスタンスを取得
        /// </summary>
        /// <returns>InputStateインスタンス</returns>
        public InputState GetInputState()
        {
            return input;
        }
    }
}
