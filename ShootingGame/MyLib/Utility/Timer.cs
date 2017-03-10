//============================================
// 時間計算用のクラス
// 作成者：張ユービン
//============================================

using System;
using Microsoft.Xna.Framework;

namespace MyLib.Utility
{
    public class Timer
    {
        //フィールド
        private float timer;        //現在時間
        private float limitTime;    //制限時間

        /// <summary>
        /// コンストラクタ（デフォルト制限時間は1秒）
        /// </summary>
        public Timer()
        {
            limitTime = 1f;
            Initialize();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="setTime">制限時間を設定</param>
        public Timer(float setTime)
        {
            limitTime = setTime;
            Initialize();
        }

        /// <summary>
        /// 初期化（Timerを0にする）
        /// </summary>
        public void Initialize()
        {
            timer = 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        /// <summary>
        /// TimeScale付きの更新
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        /// <param name="timeScale">TimeScale</param>
        public void Update(GameTime gameTime, float timeScale)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds * timeScale;
        }

        /// <summary>
        /// 時間を判定
        /// </summary>
        /// <returns>制限時間になったらTrue</returns>
        public bool IsTime()
        {
            return timer >= limitTime;
        }

        /// <summary>
        /// 現在時間と制限時間の割合を計算（最大は1）
        /// </summary>
        /// <returns>現在時間と制限時間の割合</returns>
        public float Rate()
        {
            return Math.Min(1.0f, timer / limitTime);
        }

        /// <summary>
        /// 制限時間を変更
        /// </summary>
        /// <param name="limitTime">limitTime</param>
        public void ChangeLimitTime(float limitTime)
        {
            this.limitTime = limitTime;
            Initialize();
        }

        /// <summary>
        /// 現在時間を取得
        /// </summary>
        /// <returns>現在時間</returns>
        public float GetNowTime()
        {
            return timer;
        }

        /// <summary>
        /// 制限時間を取得
        /// </summary>
        /// <returns>制限時間</returns>
        public float GetLimitTime()
        {
            return limitTime;
        }
    }
}
