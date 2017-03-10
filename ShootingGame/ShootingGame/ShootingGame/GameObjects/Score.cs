//==================================
// 点数表示、保存用のクラス
// 作成者：佐瀬
// 修正：張ユービン
//==================================

using System.IO;
using MyLib.Device;
using Microsoft.Xna.Framework;

namespace ShootingGame.GameObjects
{
    public static class Score
    {
        private static int highScore = 0;                   //最高得点
        private static int score = 0;                       //現在得点
        private static string textPath = "HighScore.txt";   //保存先のファイル名

        /// <summary>
        /// 初期化
        /// </summary>
        public static void Initialize()
        {
            score = 0;
        }

        /// <summary>
        /// 点数を描画
        /// </summary>
        /// <param name="renderer"></param>
        public static void Draw(Renderer renderer)
        {
            renderer.DrawTexture("gametext", new Vector2(600, 12), new Rectangle(0, 64, 256, 64));
            renderer.DrawNumber("number", new Vector2(980, 65), highScore, true);
            renderer.DrawTexture("gametext", new Vector2(600, 125), new Rectangle(0, 0, 256, 64));
            renderer.DrawNumber("number", new Vector2(980, 178), score, true);
        }

        /// <summary>
        /// 点数加算
        /// </summary>
        /// <param name="point">点数</param>
        public static void AddPoint(int point)
        {
            score = score + point;
            if (highScore < score)
            {
                highScore = score;
            }
        }

        /// <summary>
        /// 最高記録をファイルに保存
        /// </summary>
        public static void LoadHighScore()
        {
            if (File.Exists(textPath))
            {
                highScore = int.Parse(File.ReadAllText(textPath));
            }
        }

        /// <summary>
        /// 最高記録をファイルから読み込み
        /// </summary>
        public static void SaveHighScore()
        {
            File.WriteAllText(textPath, highScore.ToString());
        }

        /// <summary>
        /// ファイルを削除
        /// </summary>
        public static void DeleteHighScore()
        {
            File.Delete(textPath);
        }
    }
}
