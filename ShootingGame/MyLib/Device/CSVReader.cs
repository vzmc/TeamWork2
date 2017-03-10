//===============================================================
// CSVファイルを読み込む用クラス
// 作成者：張ユービン
//===============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MyLib.Device
{
    public static class CSVReader
    {
        private static List<string[]> stringData = new List<string[]>();  //CSVファイルから読み込んだデータの保存ところ
        
        /// <summary>
        /// CSVファイルからデータを読み込む
        /// </summary>
        /// <param name="filename">CSVファイルの名前</param>
        public static void Read(string filename)
        {
            stringData.Clear();
            try
            {
                //CSVファイルを開く
                using (var sr = new StreamReader(@"Content/Map/" + filename))
                {
                    //ストリームの末尾まで繰り返す
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        //読み込んだ１行をカンマごとに分けて配列に格納する
                        var values = line.Split(',');
                        stringData.Add(values);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// List式のStringデータを取得
        /// </summary>
        /// <returns>List式のデータ</returns>
        public static List<string[]> GetData()
        {
            return stringData;
        }

        /// <summary>
        /// Array式のStringデータを取得
        /// </summary>
        /// <returns>Array式のデータ</returns>
        public static string[][] GetArrayData()
        {
            return stringData.ToArray();
        }

        /// <summary>
        /// Array式のIntデータを取得
        /// </summary>
        /// <returns>Array式のIntデータ</returns>
        public static int[][] GetIntData()
        {
            var data = GetArrayData();
            int y = data.Count();
            int x = data[0].Count();

            int[][] intData = new int[y][];
            for(int i = 0; i < y; i++)
            {
                intData[i] = new int[x];
            }

            for(int i = 0; i < y; i++)
            {
                for(int j = 0; j < x; j++)
                {
                    intData[i][j] = int.Parse(data[i][j]);
                }
            }

            return intData;
        }

        /// <summary>
        /// Matrix式Stringデータを取得
        /// </summary>
        /// <returns>Matrix式Stringデータ</returns>
        public static string[,] GetStringMatrix()
        {
            int min = 100000;
            foreach(var s in stringData)
            {
                if(s.Count() < min)
                {
                    min = s.Count();
                }
            }

            string[,] strMatrix = new string[stringData.Count, min];

            for(int y = 0; y < strMatrix.GetLength(0); y++)
            {
                for(int x = 0; x < strMatrix.GetLength(1); x++)
                {
                    strMatrix[y, x] = stringData[y][x];
                }
            }

            return strMatrix;
        }

        /// <summary>
        /// Matrix式intデータを取得
        /// </summary>
        /// <returns>Matrix式intデータ</returns>
        public static int[,] GetIntMatrix()
        {
            var strMatrix = GetStringMatrix();

            int[,] iMatrix = new int[strMatrix.GetLength(0), strMatrix.GetLength(1)];

            for (int y = 0; y < strMatrix.GetLength(0); y++)
            {
                for (int x = 0; x < strMatrix.GetLength(1); x++)
                {
                    iMatrix[y, x] = int.Parse(strMatrix[y, x]);
                }
            }

            return iMatrix;
        }
    }
}
