using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MenuTest
{
    /// <summary>
    /// ペンツールのクラス
    /// </summary>
    public class PenTool : ITool
    {
        /// <summary>
        /// マウスを押したか
        /// (押している間は作業用キャンバスに描画する)
        /// </summary>
        private Boolean _mouseDownFlag;

        /// <summary>
        /// 直前のマウスカーソルの位置
        /// </summary>
        private Point _lastPoint;
        

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PenTool()
        {
            _mouseDownFlag = false;
        }


        #region ITool メンバ

        /// <summary>
        /// ツールIDを返すプロパティ
        /// </summary>
        public int ToolId
        { get { return 1; } }


        /// <summary>
        /// アクティブになった時に呼び出される処理
        /// </summary>
        public void onActivate()
        {
            _mouseDownFlag = false;
        }


        /// <summary>
        /// アクティブではなくなった時の処理
        /// </summary>
        public void onDeactivate()
        {
            _mouseDownFlag = false;
        }


        /// <summary>
        /// マウスボタンを押した時の処理
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">マウスカーソルの情報を含むオブジェクト</param>
        public void onMouseDown(object sender, MouseEventArgs e)
        {
            if(_mouseDownFlag)
            {
                return;
            }
            
            MenuTest.Application app = MenuTest.Application.getInstance();
            Document doc = app.ActiveDocument;

            if(doc == null)
            {
                return;
            }
            //doc.putLine(new Point(0, 5), new Point(20, 0));
            //doc.putLine(new Point(8, 0), new Point(0, 5));
            //doc.putLine(new Point(15, 8), new Point(0, 0));
            //doc.putLine(new Point(0, 0), new Point(15, 8));
            //doc.putLine(new Point(2, 5), new Point(2, 18));
            //doc.putLine(new Point(2, 18), new Point(2, 5));
            //doc.putLine(new Point(18, 2), new Point(5, 2));
            
            _mouseDownFlag = true;
            _lastPoint.X = e.X;
            _lastPoint.Y = e.Y;
            //doc.putLine(new Point(20, 20), _lastPoint);
            onMouseMove(sender, e);

            doc.beginEdit();
        }


        /// <summary>
        /// マウスボタンを離した時の処理
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">マウスカーソルの情報を含むオブジェクト</param>
        public void onMouseUp(object sender, MouseEventArgs e)
        {
            if(!_mouseDownFlag)
            {
                return;
            }

            _mouseDownFlag = false;
            MenuTest.Application app = MenuTest.Application.getInstance();
            Document doc = app.ActiveDocument;

            if(doc == null)
            {
                return;
            }

            //historyActionを作る
            Surface s  = doc.Surface.Clone() as Surface,
                    bs = doc.BackSurface.Clone() as Surface;
            SurfaceCopyAction sca = new SurfaceCopyAction(doc, s, bs);
            doc.addEdit(sca);

            //サーフェイスの内容をバックサーフェイス(アンドゥ用)へコピー
            doc.endEdit();
        }


        /// <summary>
        /// マウスカーソルを移動した時の処理
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">マウスカーソルの情報を含むオブジェクト</param>
        public void onMouseMove(object sender, MouseEventArgs e)
        {
            if(!_mouseDownFlag)
            {
                return;
            }

            Point curPos = new Point(e.X, e.Y);
            MenuTest.Application app = MenuTest.Application.getInstance();
            Document doc = app.ActiveDocument;
            if(doc == null)
            {
                return;
            }

            
            doc.putLine(curPos, _lastPoint);
            _lastPoint.X = curPos.X;
            _lastPoint.Y = curPos.Y;
        }


        /// <summary>
        /// キャンバスを描画する時に呼び出される処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト(=canvas)</param>
        /// <param name="e">描画イベント</param>
        public void onPaint(object sender, PaintEventArgs e)
        {
        }

        #endregion
    }
}
