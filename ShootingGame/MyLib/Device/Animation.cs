#region 概要
//-----------------------------------------------------------------------------
// アニメションのクラス
// 作成者：張ユービン
//-----------------------------------------------------------------------------
#endregion

using Microsoft.Xna.Framework.Graphics;

namespace MyLib.Device
{
    public class Animation
    {
        private string name;
        public string Name
        {
            get { return name; }
        }

        //フレイムを横に並べる画像
        private Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
        }

        //1フレイムの時間
        private float frameTime;
        public float FrameTime
        {
            get { return frameTime; }
        }

        //Loopするか？
        private bool isLooping;
        public bool IsLooping
        {
            get { return isLooping; }
        }
        
        //フレイム総数
        public int FrameCount
        {
            get { return Texture.Width / FrameWidth; }
        }

        //フレイムの横幅
        public int FrameWidth
        {
            get { return Texture.Height; }
        }

        //フレイムの横幅
        public int FrameHeight
        {
            get { return Texture.Height; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="texture">フレイムを並べる画像</param>
        /// <param name="frameTime">1フレイムの時間</param>
        /// <param name="isLooping">Loopするか？</param>
        public Animation(Renderer renderer, string name, float frameTime, bool isLooping)
        {
            this.name = name;
            texture = renderer.Textures[name];
            this.frameTime = frameTime;
            this.isLooping = isLooping;
        }
    }
}
