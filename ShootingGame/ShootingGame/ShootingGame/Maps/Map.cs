//==============================
// Map情報のクラス（敵の生成など）
// 作成者：張ユービン
//==============================

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyLib.Device;
using MyLib.Utility;
using ShootingGame.Def;
using ShootingGame.GameObjects;
using ShootingGame.Scene;

namespace ShootingGame.Maps
{
    public class Map
    {
        private BackGround backGround;                          //背景移動処理のクラス
        private Dictionary<string, Enemy> enemyDictionary;      //敵を格納するDictionary
        private List<string> enemyNameList;                     //敵の名前を格納するList
        private Timer makeEnemyTimer;                           //敵生産の時間間隔
        private ObjectManager objManager;

        public Map()
        {
            enemyDictionary = new Dictionary<string, Enemy>();
            enemyNameList = new List<string>();
            makeEnemyTimer = new Timer(2.0f);
            objManager = GamePlay.GetObjManager();
            //bosstimer = new Timer(3f);
        }

        public void Init()
        {
            //敵の格納
            enemyDictionary.Clear();
            enemyDictionary.Add("testEnemy", new Enemy("enemy", 80, new LineController(1.0f), new EnemyBulletFactory(Vector2.Zero)));
            enemyDictionary.Add("RepeatEnemy", new Enemy("enemy2-2", 50, new RepeatMoveController(1.4f), new EnemyBulletFactory(Vector2.Zero)));
            enemyDictionary.Add("SitInEnemy", new Enemy("enemy2-1", 40, new ShitInController(0.5f), new EnemyBulletFactory(Vector2.Zero)));
            enemyDictionary.Add("ReturnEnemy", new Enemy("enemy3-3", 30, new ReturnController(0.5f), new LaserFactory(Vector2.Zero)));
            enemyDictionary.Add("BackEnemy", new Enemy("backenemy", 50, new LineController(2.0f), new ChaseBulletFactory(Vector2.Zero)));
            enemyDictionary.Add("StraightEnemy", new Enemy("enemy3-2", 80, new LineController(3.0f), new ChaseBulletFactory(Vector2.Zero)));
            enemyDictionary.Add("ZeroEnemy", new Enemy("enemy3-5", 20, new ZeroController(100), new EnemyBulletFactory(Vector2.Zero)));
            //enemyDictionary.Add("BossEnemy", new Enemy("enemy", 100, new BossContoroller(0.2f), new EnemyBulletFactory(Vector2.Zero)));

            //敵の名前を格納
            enemyNameList.Clear();
            enemyNameList.Add("testEnemy");
            enemyNameList.Add("RepeatEnemy");
            enemyNameList.Add("SitInEnemy");
            enemyNameList.Add("ReturnEnemy");
            enemyNameList.Add("BackEnemy");
            enemyNameList.Add("StraightEnemy");
            enemyNameList.Add("ZeroEnemy");

            backGround = new BackGround("main");
        }

        /// <summary>
        /// 敵の生産
        /// </summary>
        private void MakeEnemy()
        {
            int num = GameDevice.Rand.Next(0, enemyNameList.Count);
            string name = enemyNameList[num];

            if (num == 4)
            {
                Enemy e = (Enemy)enemyDictionary[name].Clone();
                e.Position = new Vector2(GameDevice.Rand.Next(0, Screen.GameWidth), Screen.Height);
                e.Velocity = new Vector2(0, GameDevice.Rand.Next(-80, -50));
                objManager.AddToObjectWaitList(e);
            }
            else
            {
                Enemy e = (Enemy)enemyDictionary[name].Clone();
                e.Position = new Vector2(GameDevice.Rand.Next(0, Screen.GameWidth), 0);
                e.Velocity = new Vector2(GameDevice.Rand.Next(-100, 100), GameDevice.Rand.Next(50, 100));
                objManager.AddToObjectWaitList(e);
            }
        }

        public void Update(GameTime gameTime)
        {
            makeEnemyTimer.Update(gameTime, Parameters.GameSpeed);
            backGround.Update(gameTime);

            if (makeEnemyTimer.IsTime() && !objManager.IsEnemyLimit())
            {
                makeEnemyTimer.Initialize();
                MakeEnemy();
            }
        }

        public void Draw(Renderer renderer)
        {
            backGround.Draw(renderer);
        }
    }
}
