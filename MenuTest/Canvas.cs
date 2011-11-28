using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MenuTest
{

    /// <summary>
    /// サーフェイスの内容を描画し、
    /// ユーザーからの入力を処理するオブジェクト
    /// </summary>
    public class Canvas : PictureBox
    {
        /// <summary>
        /// 表示するサーフェイス
        /// </summary>
        private Surface _surface;

        /// <summary>
        /// テスト用
        /// </summary>
        private SolidBrush _drawBrush;
        private Pen _pen;

        /// <summary>
        /// グリッドの幅
        /// </summary>
        private Int32 _gridW = 16;

        /// <summary>
        /// グリッドの縦幅
        /// </summary>
        private Int32 _gridH = 16;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="doc">ドキュメント</param>
        public Canvas(Document doc)
        {
            _surface = doc.Surface;
            _drawBrush = new SolidBrush(Color.Black);
            _pen = new Pen(_drawBrush);
            this.DoubleBuffered = true;

            doc.SurfaceChangeEvent += new Document.SurfaceChangeEventHandler(onSurfaceChange);
        }


        /// <summary>
        /// サーフェイスの内容に変化があった場合に
        /// 呼び出される。
        /// </summary>
        /// <param name="sender">呼び出しもとのオブジェクト</param>
        /// <param name="rect">変更があった領域の座標。現在は未実装</param>
        public void onSurfaceChange(object sender, Rectangle rect)
        {
            //自分自身を再描画する
            Invalidate();
        }


        /// <summary>
        /// サーフェイスの内容を描画する
        /// onPaintの中でサーフェイスの描画処理を行うのは無駄がある。
        /// あらかじめバックバッファに描画しておいて、ここでは単純に
        /// 一枚の画像として転送するようにする。
        /// </summary>
        /// <param name="pe">ペイントイベント</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            //base.OnPaint(pe);

            if(_surface == null)
            {
                return;
            }

            //Bitmap b = new Bitmap(this.Width, this.Height, pe.Graphics);
            
            Int32 px, py;
            Byte[,] buffer = _surface.getBuffer();
            Rectangle rc = new Rectangle();
            Point l = new Point();
            Size s = new Size();
            for(Int32 y=0; y < _surface.H; y++)
            {
                for(Int32 x=0; x < _surface.W; x++)
                {
                    px = x * _gridW;
                    py = y * _gridH;
                    
                    l.X = px;
                    l.Y = py;
                    s.Width = _gridW;
                    s.Height = _gridH;
                    rc.Location = l;
                    rc.Size = s;
                    switch(buffer[x,y])
                    {
                    case 0:
                        pe.Graphics.DrawRectangle(_pen, rc);
                        break;

                    case 1:
                        pe.Graphics.FillRectangle(_drawBrush, rc);
                        break;
                    }

                }
            }

            Application app = Application.getInstance();
            app.Tool.onPaint(this, pe);

            return;
        }


        /// <summary>
        /// マウスボタンを押した時の処理
        /// </summary>
        /// <param name="e">マウスイベント</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            //現在選択中のツールを実行する
            MenuTest.Application app = MenuTest.Application.getInstance();
            if(app.Tool != null)
            {
                //マウスカーソルの座標をキャンバス座標に直してから渡す
                app.Tool.onMouseDown(this, _getSurfaceMouseEvent(e));
            }
        }

        
        /// <summary>
        /// マウスカーソルを移動した時の処理
        /// </summary>
        /// <param name="e">マウスイベント</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            //現在選択中のツールを実行する
            MenuTest.Application app = MenuTest.Application.getInstance();
            if(app.Tool != null)
            {
                //マウスカーソルの座標をキャンバス座標に直してから渡す
                app.Tool.onMouseMove(this, _getSurfaceMouseEvent(e));
            }
        }


        /// <summary>
        /// マウスボタンを上げた時の処理
        /// </summary>
        /// <param name="e">マウスイベント</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            //現在選択中のツールを実行する
            MenuTest.Application app = MenuTest.Application.getInstance();
            if(app.Tool != null)
            {
                //マウスカーソルの座標をキャンバス座標に直してから渡す
                app.Tool.onMouseUp(this, _getSurfaceMouseEvent(e));
            }
        }


        /// <summary>
        /// スクロールやズームに合わせて変換した新しい
        /// マウスイベントを返す。
        /// </summary>
        /// <param name="e">マウスイベント</param>
        /// <returns>変換後のマウスイベント</returns>
        protected MouseEventArgs _getSurfaceMouseEvent(MouseEventArgs e)
        {
            Point p = canvasToSurface(new Point(e.X, e.Y));
            return new MouseEventArgs(e.Button, e.Clicks, p.X, p.Y, e.Delta);
        }


        /// <summary>
        /// サーフェイス座標からキャンバス上の座標へ座標変換を行う
        /// </summary>
        /// <param name="p">サーフェイス座標</param>
        /// <returns>キャンバス座標</returns>
        public Point surfaceToCanvas(Point p)
        {
            return new Point(p.X * _gridW, p.Y * _gridH);
        }


        /// <summary>
        /// キャンバス座標からサーフェイス上の座標へ座標変換を行う
        /// </summary>
        /// <param name="p">キャンバス座標</param>
        /// <returns>サーフェイス座標</returns>
        public Point canvasToSurface(Point p)
        {
            return new Point(p.X / _gridW, p.Y / _gridH);
        }

    }
}
