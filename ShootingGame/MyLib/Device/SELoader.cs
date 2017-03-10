//================================================================================
// SEをロードするクラス
// 作成者：張ユービン
//================================================================================

namespace MyLib.Device
{
    /// <summary>
    /// SE読み込みクラス
    /// </summary>
    public class SELoader : Loader
    {
        private Sound sound;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sound">Soundデバイス</param>
        /// <param name="resources">ロードするパス配列</param>
        public SELoader(Sound sound, string[,] resources):
            base(resources)
        {
            this.sound = sound;
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
                sound.LoadSE(resources[counter, 0], resources[counter, 1]);
                sound.CreateSEInstance(resources[counter, 0]);
                counter++;
                endFlag = false;
            }
        }
    }
}
