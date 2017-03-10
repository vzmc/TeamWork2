//=========================================
// Scene管理用のクラス
// 作成者：張ユービン
//=========================================

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ShootingGame.Scene
{
    class SceneManager
    {
        //複数のシーン
        private Dictionary<SceneType, IScene> scenes = new Dictionary<SceneType, IScene>();

        //現在のシーン
        private IScene currentScene = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SceneManager()
        {
        }

        /// <summary>
        /// シーンの追加
        /// </summary>
        /// <param name="name">シーンの列挙型</param>
        /// <param name="scene"シーンオブジェクト></param>
        public void Add(SceneType name, IScene scene)
        {
            if(scenes.ContainsKey(name))
            {
                //すでに同じ名前でシーンが追加されてれば終了
                return;
            }
            else
            {
                scenes.Add(name, scene);
            }
        }

        /// <summary>
        /// シーンの切り替え
        /// </summary>
        /// <param name="name">シーンの名前</param>
        public void Change(SceneType name)
        {
            if(currentScene != null)
            {
                currentScene.ShutDown();
            }

            currentScene = scenes[name];
            currentScene.Initialize();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public void Update(GameTime gameTime)
        {
            //シーンが全くなければ終了
            if(currentScene == null)
            {
                return;
            }
            else
            {
                //更新
                currentScene.Update(gameTime);
                //シーン終了か？
                if(currentScene.IsEnd())
                {
                    SceneType nextScene = currentScene.Next();
                    //次のシーンへ
                    Change(nextScene);
                }
            }
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(GameTime gameTime)
        {
            if(currentScene == null)
            {
                return;
            }
            else
            {
                currentScene.Draw(gameTime);
            }
        }
    }
}
