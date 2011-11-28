using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MenuTest
{
    /// <summary>
    /// メニューバー、ツールバー等で使うリソースの
    /// 管理をする簡単で良い方法が思いつかないので
    /// Managerクラスを用意。
    /// </summary>
    public class UiResourceManager
    {
        /// <summary>
        /// クラスのインスタンス
        /// </summary>
        private static UiResourceManager _instance = new UiResourceManager();
        /// <summary>
        /// 画像リスト
        /// </summary>
        private ImageList _imageList;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private UiResourceManager()
        {
            _imageList = new ImageList();
        }


        /// <summary>
        /// 指定したIDの画像を返す
        /// </summary>
        public ImageList ImageList
        { get{ return _imageList; } }


        /// <summary>
        /// クラスのインスタンスを返す
        /// </summary>
        public static UiResourceManager Instance
        { get { return _instance; } }
    }
}
