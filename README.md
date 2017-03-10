# TeamWork2
こちらは2Dのシューティングゲームです。  
「Release」フォルダー内はビルド済みの実行ファイルです。  
  
ゲーム目的：180秒内、高得点を目指します。  
操作：  
　移動：方向キー  
　撃つ：スペース  
　GameOverシーンでスペースを押すとTitleに戻ります。  
内容説明：  
　敵はランダムで生成しています。画面内の敵数は10体以内に制限されています。  
　Playerのダメージ判定範囲は中心の赤ポイントのみ、機体の部分を敵や敵の弾と掠ると、時間が減速し、自機弾が強化されます。  
  
制作チーム：  
　プランナー：　　　　　1名  
　メインプログラマー：　1名（私）  
　プログラマー：　　　　2名  
 
開発環境：  
　Visual Stadio2015 + XNA 4.05 + C#  
   
制作期間：  
　30時間  
  
説明：  
　このプログラムは、違う敵やPlayerを実現するには、直接クラス継承ではなく、パーツを交換する方法を使いました。敵もPlayerも二つのパーツがあります。  
　Controller：行動を制御する  
　BulletFactory：弾を生成する  
そしてBulletFactoryが色んなBulletを装填できます。  
  
担当部分：  
　共用可能な部分を抽出し、ライブラリを作成：MyLib部分のすべて  

　基盤とするプログラムの設計：  
　　Game基盤：ShootingGame  
　　Scene管理：SceneManager、SceneType  
　　GameObject管理：ObjectManager  
　　Map管理：Map  

ベースクラス、構造体の設計：  
　　IScene、GameObject、BulletFactory、Bullet、Character、IController、ICircleColider、IColider、ILineColider、IRectangleColider   
  
　Sceneの実装：  
　　Title、GamePlay、Ending、StageIn、Load  
　  
　GameObjectの実装：  
　　Enemy、Player  
　  
　一部パーツの実装：  
　　PlayerBulletFactory、EnemyBulletFactory、EnemyBulletTest、MyTestBullet、LineController、PlayerController  
　  
　コード全体の管理、リファクタリング、Debug  
