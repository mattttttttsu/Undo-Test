using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MenuTest
{
    /// <summary>
    /// 直線を引くツール
    /// </summary>
    public class LineTool : ITool
    {
        /// <summary>
        /// マウスを押したかどうかのフラグ
        /// </summary>
        private Boolean _mouseDownFlag;

        /// <summary>
        /// 始点の座標
        /// </summary>
        private Point _startPoint;

        /// <summary>
        /// 終点の座標
        /// </summary>
        private Point _endPoint;

        /// <summary>
        /// プレビュー用のペンオブジェクト
        /// </summary>
        private Pen _pen = null;

        #region ITool メンバ

        /// <summary>
        /// ツールID
        /// </summary>
        public int ToolId
        { get { return 2; } }


        /// <summary>
        /// ツールがアクティブになった時の処理
        /// </summary>
        public void onActivate()
        {
            _mouseDownFlag = false;

            if(_pen != null) {
                _pen.Dispose();
            }
            _pen = new Pen(Color.Black, 1);
        }


        /// <summary>
        /// ツールがアクティブでなくなった時の処理
        /// </summary>
        public void onDeactivate()
        {
            _pen.Dispose();
            _pen = null;
        }


        /// <summary>
        /// マウスボタンを押した時の処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">マウスイベント</param>
        public void onMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(_mouseDownFlag) {
                return;
            }

            _mouseDownFlag = true;
            _startPoint = e.Location;
        }


        /// <summary>
        /// マウスボタンを離した時の処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">マウスイベント</param>
        public void onMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(!_mouseDownFlag) {
                return;
            }

            _mouseDownFlag = false;
            MenuTest.Application app = MenuTest.Application.getInstance();
            Document doc = app.ActiveDocument;

            if(doc == null)
            {
                return;
            }

            //直線をサーフェイスに描画する
            doc.putLine(_startPoint, e.Location);

            //historyActionを作る
            Surface s  = doc.Surface.Clone() as Surface,
                    bs = doc.BackSurface.Clone() as Surface;
            SurfaceCopyAction sca = new SurfaceCopyAction(doc, s, bs);
            doc.addEdit(sca);

            //サーフェイスの内容をバックサーフェイス(アンドゥ用)へコピー
            doc.endEdit();
        }


        /// <summary>
        /// マウスを移動した時の処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">マウスイベント</param>
        public void onMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(!_mouseDownFlag) {
                return;
            }

            _endPoint = e.Location;
            
            MenuTest.Application app = MenuTest.Application.getInstance();
            Document doc = app.ActiveDocument;

            if(doc == null)
            {
                return;
            }
            doc.updateSurface(new Rectangle(0, 0, 1, 1));
        }


        /// <summary>
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト(=canvas)</param>
        /// <param name="e">描画イベント</param>
        public void onPaint(object sender, PaintEventArgs e)
        {
            if(!_mouseDownFlag) {
                return;
            }

            Canvas c = sender as Canvas;
            if(c == null) {
                //キャンバス以外のオブジェクトから発生するはずはない
                return;
            }

            Point start = c.surfaceToCanvas(_startPoint),
                  end   = c.surfaceToCanvas(_endPoint);

            e.Graphics.DrawLine(_pen, start, end);
        }


        #endregion
    }
}
