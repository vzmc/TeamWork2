//================================================================================
// BGMをロードするクラス
// 作成者：張ユービン
//================================================================================

namespace MyLib.Device
{
    /// <summary>
    /// BGM読み込みクラス
    /// </summary>
    public class BGMLoader : Loader
    {
        private Sound sound;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sound">Soundデバイス</param>
        /// <param name="resources">ロードするパス配列</param>
        public BGMLoader(Sound sound, string[,] resources) :
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
                sound.LoadBGM(resources[counter, 0], resources[counter, 1]);
                counter++;
                endFlag = false;
            }
        }
    }
}
