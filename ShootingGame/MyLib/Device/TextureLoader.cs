//==========================================================
// 画像を読み込み用のクラス
// 作成者：張ユービン
//==========================================================

namespace MyLib.Device
{
    public class TextureLoader : Loader
    {
        private Renderer renderer;  //Rendererデバイス

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="renderer">Renderer</param>
        /// <param name="resources">リソースパス群</param>
        public TextureLoader(Renderer renderer, string[,] resources) :
            base(resources)
        {
            this.renderer = renderer;
            Initialize();
        }

        /// <summary>
        /// 更新
        /// </summary>
        public override void Update()
        {
            endFlag = true;

            if(counter < maxNum)
            {
                renderer.LoadTexture(resources[counter, 0], resources[counter, 1]);
                counter++;
                endFlag = false;
            }
        }
    }
}
