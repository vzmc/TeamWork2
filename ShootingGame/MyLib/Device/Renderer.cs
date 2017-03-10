//=================================================
// 画像描画用クラス
// 作成者：張ユービン
//=================================================

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;   //Assert用

namespace MyLib.Device
{
    public class Renderer
    {
        //フィールド
        private ContentManager contentManager;  //コンテンツ管理者
        private GraphicsDevice graphicsDevice;  //グラフィック機器
        private SpriteBatch spriteBatch;        //スプライト一括
        
        //Dictionaryで複数の画像を管理
        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public Dictionary<string, Texture2D> Textures
        {
            get
            {
                return textures;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="content">Game1のコンテンツ管理者</param>
        /// <param name="graphics">Game1のグラフィック機器</param>
        public Renderer(ContentManager content, GraphicsDevice graphics)
        {
            //（Game1クラスの大本のコンテンツ情報とひもづけ）
            contentManager = content;
            //（Game1クラスの大本のGraohicsDeviceとひもづけ）
            graphicsDevice = graphics;
            //現在のグラフィックデバイスでスプライト一括描画オブジェクトの実体を生成
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        /// <summary>
        /// 画像の読み込み
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="filepath">ファイルまでのパス</param>
        public void LoadTexture(string name, string filepath = "./")
        {
            //ガード節
            //Dictionaryへの２重登録を回避
            if(textures.ContainsKey(name))
            {
#if DEBUG       //DEBUGモードの時のみ有効
                Console.WriteLine("この" + name + "はKeyで、すでに登録しています");
#endif
                //処理終了
                return;            
            }

            //画像の読み込みとDictionaryにアセット名と画像を追加
            textures.Add(name, contentManager.Load<Texture2D>(filepath + name));
        }

        /// <summary>
        /// アンロード
        /// </summary>
        public void Unload()
        {
            //Dictionary登録情報をクリア
            textures.Clear();
        }

        /// <summary>
        /// 描画開始
        /// </summary>
        public void Begin()
        {
            spriteBatch.Begin();
        }

        /// <summary>
        /// 描画終了
        /// </summary>
        public void End()
        {
            spriteBatch.End();
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="name">アセット</param>
        /// <param name="position">描画位置</param>
        /// <param name="alpha">透明値（0.0fは完全透明、1.0fは完全不透明）</param>
        public void DrawTexture(string name, Vector2 position, float alpha = 1.0f)
        {
            //登録されているキーがなければエラー表示
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");

            spriteBatch.Draw(textures[name], position, Color.White * alpha);
        }

        /// <summary>
        /// 画像の描画（指定範囲）
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="rect">画像の切り出し範囲</param>
        /// <param name="alpha">透明値</param>
        public void DrawTexture(string name, Vector2 position, Rectangle rect, float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");

            spriteBatch.Draw(
                textures[name], //画像
                position,       //位置
                rect,           //の指定範囲
                Color.White * alpha);
        }

        /// <summary>
        /// 画像の描画（指定描画原点）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="origin"></param>
        /// <param name="alpha"></param>
        public void DrawTexture(string name, Vector2 position, Vector2 origin, float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");

            Vector2 drawOrigin = new Vector2(textures[name].Width * origin.X, textures[name].Height * origin.Y);

            spriteBatch.Draw(textures[name], position, null, Color.White * alpha, 0.0f, drawOrigin, 1.0f, SpriteEffects.None, 0.0f);
        }

        /// <summary>
        /// 画像の描画（描画原点指定, 色指定）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="origin"></param>
        /// <param name="alpha"></param>
        public void DrawTexture(string name, Vector2 position, Color color, Vector2 origin, float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");

            Vector2 drawOrigin = new Vector2(textures[name].Width * origin.X, textures[name].Height * origin.Y);

            spriteBatch.Draw(textures[name], position, null, color * alpha, 0.0f, drawOrigin, 1.0f, SpriteEffects.None, 0.0f);
        }

        /// <summary>
        /// アニメション用の描画
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        /// <param name="rotation"></param>
        /// <param name="drawOrigin"></param>
        /// <param name="scale"></param>
        /// <param name="spriteEffect"></param>
        /// <param name="alpha"></param>
        public void DrawTexture(string name, Vector2 position, Rectangle rect, Color color, float rotation, Vector2 drawOrigin, float scale = 1.0f, SpriteEffects spriteEffect = SpriteEffects.None, float alpha = 1.0f)
        {
            spriteBatch.Draw(textures[name], position, rect, color * alpha, rotation, drawOrigin, scale, spriteEffect, 0.0f);
        }

        /// <summary>
        /// 数字の描画（整数のみ版）
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="number">表示したい数字（整数）</param>
        /// <param name="alpha">透明値</param>
        /// <param name="isAlignRight">右端揃えにするか</param>
        public void DrawNumber(string name, Vector2 position, int number, bool isAlignRight = false, float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");

            //マイナスの数は０
            if(number < 0)
            {
                number = 0;
            }
            string nums = number.ToString();

            if (isAlignRight)
            {
                foreach (var n in nums.Reverse())
                {
                    spriteBatch.Draw(
                        textures[name],
                        position,
                        new Rectangle((n - '0') * 32, 0, 32, 64),
                        Color.White * alpha
                        );
                    position.X -= 32;
                }
            }
            else
            {
                foreach (var n in nums)
                {
                    spriteBatch.Draw(
                        textures[name],
                        position,
                        new Rectangle(int.Parse(n.ToString()) * 32, 0, 32, 64),
                        Color.White * alpha
                        );
                    position.X += 32;
                }
            }
        }

        /// <summary>
        /// 数字の描画（詳細版）
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="number">表示したい数字（文字列でもらう）</param>
        /// <param name="digit">桁数</param>
        /// <param name="alpha">透明値</param>
        public void DrawNumber(string name, Vector2 position, string number, int digit, float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");

            for(int i = 0; i < digit; i++)
            {
                if(number[i] == '.')
                {
                    //幅をかけて座標を求め、１文字を絵から切り出す
                    spriteBatch.Draw(
                        textures[name],
                        position,
                        new Rectangle(10 * 32, 0, 32, 64),
                        Color.White * alpha
                        );
                }
                else
                {
                    //１文字分の数値を数値文字で取得
                    char n = number[i];

                    //幅をかけて座標を求め、１文字を絵から切り出す
                    spriteBatch.Draw(
                        textures[name],
                        position,
                        new Rectangle((n - '0') * 32, 0, 32, 64),
                        Color.White * alpha
                        );

                }
                position.X += 32;
            }
        }

        public void DrawNumber(string name, Vector2 position, int number, int digit, float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");

            foreach(var n in number.ToString().PadLeft(digit, '0'))
            {
                spriteBatch.Draw(
                    textures[name],
                    position,
                    new Rectangle((n - '0') * 32, 0, 32, 64),
                    Color.White * alpha
                    );
                position.X += 32;
            }
        }
    }
}
