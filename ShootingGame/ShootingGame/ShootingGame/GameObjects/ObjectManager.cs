//=====================================================
// Scene内の物を管理するクラス
// 作成者：張ユービン
//=====================================================

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyLib.Device;
using MyLib.Utility;
using ShootingGame.Def;


namespace ShootingGame.GameObjects
{
    public class ObjectManager
    {
        private List<Player> playerList;        //Playerをこちに入れる
        private List<Enemy> enemyList;          //Enemyをこちに入れる
        private List<Bullet> bulletList;        //弾をこちに入れる

        private List<GameObject> addList;       //Add待ちのList

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ObjectManager()
        {
            playerList = new List<Player>();
            enemyList = new List<Enemy>();
            bulletList = new List<Bullet>();
            addList = new List<GameObject>();
        }

        /// <summary>
        /// 初期化（全Listをクリア）
        /// </summary>
        public void Init()
        {
            playerList.Clear();
            enemyList.Clear();
            bulletList.Clear();
            addList.Clear();
        }

        /// <summary>
        /// 更新（物体の追加、全物体の更新、衝突判定、死亡物体の削除）
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public void Update(GameTime gameTime)
        {
            AddToObjectList();

            foreach (var p in playerList)
            {
                p.Update(gameTime);
            }
            foreach(var e in enemyList)
            {
                e.Update(gameTime);
            }
            foreach(var b  in bulletList)
            {
                b.Update(gameTime);
            }

            Collision();

            RemoveDeath();
        }
        
        /// <summary>
        /// 死亡物体を削除
        /// </summary>
        public void RemoveDeath()
        {
            playerList.RemoveAll(x => x.IsDead);
            enemyList.RemoveAll(x => x.IsDead);
            bulletList.RemoveAll(x => x.IsDead);
        }

        /// <summary>
        /// 全物体の描画
        /// </summary>
        /// <param name="renderer">Renderer</param>
        public void Draw(Renderer renderer)
        {
            foreach (var p in playerList)
            {
                p.Draw(renderer);
            }

            foreach (var e in enemyList)
            {
                e.Draw(renderer);
            }

            foreach(var b in bulletList)
            {
                b.Draw(renderer);
            }
        }

        /// <summary>
        /// 物体を追加待ちListに入れる
        /// </summary>
        /// <param name="obj"></param>
        public void AddToObjectWaitList(GameObject obj)
        {
             addList.Add(obj);
        }

        /// <summary>
        /// 衝突判定
        /// </summary>
        public void Collision()
        {
            Parameters.GameSpeed = 1.0f;

            //弾と各物の判定
            foreach (var b in bulletList)
            {
                if (b.BulletType == BulletType.ENEMY)
                {
                    foreach (var p in playerList)
                    {
                        if(p.NowState == State.START || p.NowState == State.DEATH || p.NowState == State.INVINCIBLE)
                        {
                            continue;
                        }

                        bool hitFlag = false;
                        bool safeFlag = false;

                        //弾の判定形状によって判定関数を呼び出す
                        switch (b.ColiderType)
                        {
                            case ColiderType.CIRLCE:
                                {
                                    hitFlag = CollisionCheck.Check(((ICircleColider)b).GetColider(), p.DamegeColider);

                                    if(Parameters.GameSpeed == 1.0f)
                                    {
                                        safeFlag = CollisionCheck.Check(((ICircleColider)b).GetColider(), p.SafeColider);
                                    }
                                    break;
                                }
                            case ColiderType.LINE:
                                {
                                    hitFlag = CollisionCheck.Check(((ILineColider)b).GetColider(), p.DamegeColider);

                                    if (Parameters.GameSpeed == 1.0f)
                                    {
                                        safeFlag = CollisionCheck.Check(((ILineColider)b).GetColider(), p.SafeColider);
                                    }
                                    break;
                                }
                            case ColiderType.RECTANGLE:
                                {
                                    hitFlag = CollisionCheck.Check(((IRectangleColider)b).GetColider(), p.DamegeColider);

                                    if (Parameters.GameSpeed == 1.0f)
                                    {
                                        safeFlag = CollisionCheck.Check(((IRectangleColider)b).GetColider(), p.SafeColider);
                                    }
                                    break;
                                }
                        }
                        if (hitFlag)
                        {
                            b.HitEvent(p);
                            p.HitEvent(b);
                        }
                        if (safeFlag)
                        {
                            //b.HitEvent(p);
                            p.SafeHitEvent(b);
                        }
                    }
                }
                else if(b.BulletType == BulletType.PLAYER)
                {
                    foreach (var e in enemyList)
                    {
                        if(e.NowState == State.DEATH)
                        {
                            continue;
                        }

                        bool hitFlag = false;

                        switch (b.ColiderType)
                        {
                            case ColiderType.CIRLCE:
                                {
                                    hitFlag = CollisionCheck.Check(((ICircleColider)b).GetColider(), e.DamegeColider);
                                    break;
                                }
                            case ColiderType.LINE:
                                {
                                    hitFlag = CollisionCheck.Check(((ILineColider)b).GetColider(), e.DamegeColider);
                                    break;
                                }
                            case ColiderType.RECTANGLE:
                                {
                                    hitFlag = CollisionCheck.Check(((IRectangleColider)b).GetColider(), e.DamegeColider);
                                    break;
                                }
                        }
                        if (hitFlag)
                        {
                            b.HitEvent(e);
                            e.HitEvent(b);
                        }
                    }
                }
            }

            //敵とPlayerの判定
            foreach(var e in enemyList)
            {
                if(e.NowState == State.DEATH)
                {
                    continue;
                }
                foreach(var p in playerList)
                {
                    if (p.NowState == State.START || p.NowState == State.DEATH || p.NowState == State.INVINCIBLE)
                    {
                        continue;
                    }

                    bool hitFlag = false;
                    bool safeFlag = false;

                    hitFlag = CollisionCheck.Check(e.DamegeColider, p.DamegeColider);

                    if (Parameters.GameSpeed == 1.0f)
                    {
                        safeFlag = CollisionCheck.Check(e.DamegeColider, p.SafeColider);
                    }

                    if (hitFlag)
                    {
                        //e.HitEvent(p);
                        p.HitEvent(e);
                    }
                    if (safeFlag)
                    {
                        //e.HitEvent(p);
                        p.SafeHitEvent(e);
                    }
                }
                
            }
        }

        /// <summary>
        /// 追加待ちList内の物体を分類し、対応のListに入れる
        /// </summary>
        private void AddToObjectList()
        {
            foreach(var obj in addList)
            {
                if(obj is Player)
                {
                    playerList.Add((Player)obj);
                }
                else if(obj is Enemy)
                {
                    enemyList.Add((Enemy)obj);
                }
                else if (obj is Bullet)
                {
                    bulletList.Add((Bullet)obj);
                }
            }

            addList.Clear();
        }

        public Vector2 GetPlayerPosition()
        {
            return playerList[0].Position;
        }

        //画面内の敵数を制限するため
        public bool IsEnemyLimit()
        {
            return enemyList.Count >= 10;
        }
        
    }
}
