using System;
using System.Collections.Generic;
using System.Text;

namespace MenuTest
{
    /// <summary>
    /// サーフェイスの内容を丸ごと置き換えるアクション
    /// </summary>
    public class SurfaceCopyAction : IHistoryAction
    {
        /// <summary>
        /// 編集対象のドキュメント
        /// </summary>
        private Document _doc;

        /// <summary>
        /// 編集後のサーフェイス
        /// </summary>
        private Surface _surface;

        /// <summary>
        /// 編集前のサーフェイス
        /// </summary>
        private Surface _backSurface;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="doc">編集対象のドキュメント</param>
        /// <param name="surface">編集後のサーフェイス</param>
        /// <param name="backSurface">編集前のサーフェイス</param>
        public SurfaceCopyAction(Document doc, Surface surface, Surface backSurface)
        {
            _doc = doc;
            _surface = surface;
            _backSurface = backSurface;
        }


        #region IHistoryAction メンバ

        /// <summary>
        /// アクションの実行、redo。
        /// </summary>
        public void execute()
        {
            _doc.Surface.copy(0, 0, _surface, 0, 0, _surface.W, _surface.H);
            _doc.endEdit();
        }


        /// <summary>
        /// アクションのundo。
        /// </summary>
        public void unexecute()
        {
            _doc.Surface.copy(0, 0, _backSurface, 0, 0, _surface.W, _surface.H);
            _doc.endEdit();
        }

        #endregion
    }
}
