﻿//=====================================================
// 入力処理を行うクラス
// 作成者：張ユービン
//====================================================

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyLib.Device
{
    /// <summary>
    /// キーボード操作を処理するクラス
    /// </summary>
    public class InputState
    {
        //フィールド
        private Vector2 velocity = Vector2.Zero;   // 移動量の宣言

        //Key
        private KeyboardState currentKey;   //現在のキー
        private KeyboardState previousKey;  //前フレームのKey
        private GamePadState currentPad;    //現在のButton
        private GamePadState previousPad;   //前フレームのButton

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InputState()
        {
        }
        
        /// <summary>
        /// 移動量の取得
        /// </summary>
        /// <returns>移動量</returns>
        public Vector2 Velocity()
        {
            return velocity;
        }

        /// <summary>
        /// 移動量の更新
        /// </summary>
        /// <param name="keyState">キーボードの状態</param>
        private void UpdateVelocity()
        {
            velocity = Vector2.Zero;    // 移動量をゼロで初期化
            
            // 十字キーの操作処理
            if (CheckDownKey(Keys.Right, Buttons.DPadRight))
            {
                velocity.X += 1.0f;
            }
            if (CheckDownKey(Keys.Left, Buttons.DPadLeft))
            {
                velocity.X -= 1.0f;
            }
            if (CheckDownKey(Keys.Up, Buttons.DPadUp))
            {
                velocity.Y -= 1.0f;
            }
            if (CheckDownKey(Keys.Down, Buttons.DPadDown))
            {
                velocity.Y += 1.0f;
            }

            // 正規化する（長さ１の単位ベクトルに）
            if (velocity.Length() != 0.0f)
            {
                velocity.Normalize();   // 正規化メソッド
            }
        }

        /// <summary>
        /// キー情報の更新
        /// </summary>
        /// <param name="keyState">KeyboardState</param>
        private void UpdateKey(KeyboardState keyState)
        {
            //現在登録されているキーを１フレーム前のキーに
            previousKey = currentKey;
            //現在のキーを最新のキーに
            currentKey = keyState;
        }

        /// <summary>
        /// button情報更新
        /// </summary>
        /// <param name="buttonState">GamePadState</param>
        private void UpdatePad(GamePadState buttonState)
        {
            //現在登録されているキーを１フレーム前のキーに
            previousPad = currentPad;
            //現在のキーを最新のキーに
            currentPad = buttonState;
        }

        /// <summary>
        /// KeyとButtonが押されているかを両方チェック
        /// </summary>
        /// <param name="key">Keys</param>
        /// <param name="button">Buttons</param>
        /// <returns>どちらが押されていたらTrue</returns>
        public bool CheckDownKey(Keys key, Buttons button)
        {
            //キーボードでチェックしたいキーが押されているか？
            if (currentKey.IsKeyDown(key))
            {
                return true;
            }

            //キーパッドのキーがつながっていないか？
            if (!GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                //接続されなければチェック終了
                return false;
            }

            //キーパッドでチェックしたいボタンが押されているか？
            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(button))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Keyが押されているか？
        /// </summary>
        /// <param name="key">調べたいキー</param>
        /// <returns>現在押されていて、１フレーム前に押されていなければ true</returns>
        public bool IsKeyDown(Keys key)
        {
            //現在チェックしたいキーが押されたか
            bool current = currentKey.IsKeyDown(key);
            //１フレーム前に押されていたか
            bool previous = previousKey.IsKeyDown(key);

            //現在押されていて、１フレーム前に押されていなければ true
            return current && !previous;
        }

        /// <summary>
        /// Buttonが押されているか？
        /// </summary>
        /// <param name="button">Buttons</param>
        /// <returns></returns>
        public bool IsKeyDown(Buttons button)
        {
            //現在チェックしたいキーが押されたか
            bool current = currentPad.IsButtonDown(button);
            //１フレーム前に押されていたか
            bool previous = previousPad.IsButtonDown(button);

            //現在押されていて、１フレーム前に押されていなければ true
            return current && !previous;
        }

        /// <summary>
        /// キー入力のトリガー判定
        /// </summary>
        /// <param name="key">Keys</param>
        /// <returns>１フレーム前に押されていたらfalse</returns>
        public bool GetKeyTrigger(Keys key)
        {
            return IsKeyDown(key);
        }

        /// <summary>
        /// Buttonのトリガー判定
        /// </summary>
        /// <param name="button">Buttons</param>
        /// <returns>１フレーム前に押されていたらfalse</returns>
        public bool GetKeyTrigger(Buttons button)
        {
            return IsKeyDown(button);
        }

        /// <summary>
        /// キー入力の状態判定
        /// </summary>
        /// <param name="key"></param>
        /// <returns>押されていたらtrue</returns>
        public bool GetKeyState(Keys key)
        {
            return currentKey.IsKeyDown(key);
        }

        /// <summary>
        /// Button入力状態判定
        /// </summary>
        /// <param name="button">Buttons</param>
        /// <returns>押されていたらtrue</returns>
        public bool GetKeyState(Buttons button)
        {
            return currentPad.IsButtonDown(button);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            // 現在のキーボードの状態を取得
            var keyState = Keyboard.GetState();
            UpdateKey(keyState);

            var padState = GamePad.GetState(PlayerIndex.One);
            UpdatePad(padState);

            // 移動量の更新
            UpdateVelocity();
        }
    }
}
