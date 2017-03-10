#region 概要
//-----------------------------------------------------------------------------
// アニメーション再生構造体
// 作成者：張ユービン
//-----------------------------------------------------------------------------
#endregion

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyLib.Utility;

namespace MyLib.Device
{
    public struct AnimationPlayer
    {
        //再生するアニメーション
        private Animation animation;
        public Animation Animation
        {
            get { return animation; }
        }

        //フレイム番号
        private int frameIndex;
        public int FrameIndex
        {
            get { return frameIndex; }
        }

        //一時停止
        private bool isPaused;
        public bool IsPaused
        {
            get
            {
                return isPaused;
            }
            set
            {
                isPaused = value;
            }
        }

        //描画位置の原点
        private Vector2 origin;
        public Vector2 Origin
        {
            get
            {
                return origin;
            }
            set
            {
                origin = value;
            }
        }

        private Timer timer;

        /// <summary>
        /// アニメションを再生
        /// </summary>
        /// <param name="animation">指定アニメション</param>
        public void SetAnimation(Animation animation)
        {
            //同じも指定したら、何もしない
            if (Animation == animation)
            {
                return;
            }

            isPaused = false;
            
            //そのアニメションの最初から再生
            this.animation = animation;
            frameIndex = 0;
            timer = new Timer(animation.FrameTime);
        }

        public void Update(GameTime gameTime, float timeScale)
        {
            if (Animation == null)
            {
                throw new NotSupportedException("アニメーション指定していません！");
            }

            if (!isPaused)
            {
                // 経過時間計算
                timer.Update(gameTime, timeScale);
                if (timer.IsTime())
                {
                    timer.Initialize();

                    frameIndex++;
                    if (frameIndex > animation.FrameCount - 1)
                    {
                        if (Animation.IsLooping)
                        {
                            frameIndex %= Animation.FrameCount;
                        }
                        else
                        {
                            frameIndex = Animation.FrameCount - 1;
                            isPaused = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// アニメーションの描画
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="position">描画位置</param>
        /// <param name="spriteEffects">向き</param>
        public void Draw(Renderer renderer, Vector2 position, Vector2 origin, float scale = 1.0f, SpriteEffects spriteEffect = SpriteEffects.None, float rotation = 0.0f, float alpha = 1.0f)
        {
            if (Animation == null)
            {
                throw new NotSupportedException("アニメーション指定していません！");
            }

            //今描画する画像範囲を計算
            Rectangle range = new Rectangle(FrameIndex * Animation.FrameWidth, 0, Animation.FrameWidth, Animation.FrameHeight);

            Vector2 drawOrigin = new Vector2(Animation.FrameWidth * origin.X, Animation.FrameHeight * origin.Y);

            //今のフレイムを描画
            renderer.DrawTexture(Animation.Name, position, range, Color.White * alpha, rotation, drawOrigin, scale, spriteEffect, alpha);
        }

        public void ResetAnimation()
        {
            frameIndex = 0;
            isPaused = false;
        }

        public bool IsEnd()
        {
            return frameIndex >= animation.FrameCount - 1;
        }
    }
}
