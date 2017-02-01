using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyLib.Device
{
    /// <summary>
    /// ゲーム共有クラス
    /// </summary>
    public class GameDevice
    {
        public static Random Rand = new Random();

        private Renderer renderer;  //描画オブジェクト
        private InputState input;   //入力オブジェクト
        private Sound sound;
        private Vector2 displayModify;
        
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
            displayModify = Vector2.Zero;
        }

        public void SetDisplayModify(Vector2 position)
        {
            displayModify = position;
        }

        public Vector2 GetDisplayModify()
        {
            return displayModify;
        }

        /// <summary>
        /// リソースの読み込み
        /// </summary>
        public void LoadContent()
        {
            //ゲーム開始時に必要な最小限のリソースを読み込む
            renderer.LoadTexture("number", "./Texture/");
            renderer.LoadTexture("load", "./Texture/");
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            input.Update();
            sound.Update();
        }

        public void UnloadContent()
        {
            renderer.Unload();
            sound.Unload();
        }

        public Sound GetSound()
        {
            return sound;
        }

        public Renderer GetRenderer()
        {
            return renderer;
        }

        public InputState GetInputState()
        {
            return input;
        }
    }
}
