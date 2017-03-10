//======================================
// 各Loadクラスの親クラス
// 作成者：張ユービン
//======================================

namespace MyLib.Device
{
    /// <summary>
    /// 読み込み抽象クラス
    /// </summary>
    public abstract class Loader
    {
        protected string[,] resources;  //リソースアセット名群
        protected int counter;          //カウンタ
        protected int maxNum;           //リソース最大数
        protected bool endFlag;         //終了フラグ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="resources">リソースアセット名群</param>
        public Loader(string[,] resources)
        {
            this.resources = resources;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            counter = 0;
            endFlag = false;
            maxNum = 0;
            if (resources != null)
            {
                maxNum = resources.GetLength(0);
            }
        }

        /// <summary>
        /// リソース最大数を取得
        /// </summary>
        /// <returns>リソース最大数</returns>
        public int Count()
        {
            return maxNum;
        }

        /// <summary>
        /// 読み込みのカウンタを取得
        /// </summary>
        /// <returns>読み込みのカウンタ</returns>
        public int CurrentCount()
        {
            return counter;
        }

        /// <summary>
        /// 読み込み終了Flagを取得
        /// </summary>
        /// <returns>読み込み終了Flag</returns>
        public bool IsEnd()
        {
            return endFlag;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public abstract void Update();
    }
}
