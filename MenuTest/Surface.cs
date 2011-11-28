using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MenuTest
{
    /// <summary>
    /// サーフェイスのクラス
    /// </summary>
    public class Surface : ICloneable
    {
        /// <summary>
        /// 横幅
        /// </summary>
        private Int32 _w;

        /// <summary>
        /// 縦幅
        /// </summary>
        private Int32 _h;

        /// <summary>
        /// バッファ
        /// </summary>
        private Byte[,] _buffer;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="w">横幅</param>
        /// <param name="h">縦幅</param>
        public Surface(Int32 w, Int32 h)
        {
            _w = w;
            _h = h;

            _buffer = new Byte[w,h];
        }


        /// <summary>
        /// 横幅を返すプロパティ
        /// </summary>
        public Int32 W
        { get { return _w; } }


        /// <summary>
        /// 縦幅を返すプロパティ
        /// </summary>
        public Int32 H
        { get { return _h; } }


        /// <summary>
        /// サーフェイスに直線を引くメソッド
        /// Bresenhamアルゴリズム(http://www2.starcat.ne.jp/~fussy/algo/algo1-1.htm)のつもり。
        /// </summary>
        /// <param name="pt1">開始点</param>
        /// <param name="pt2">終了点</param>
        /// <param name="size">ペンサイズ</param>
        /// <param name="color">色</param>
        public void putLine(Point pt1, Point pt2, Int32 size, Byte color)
        {
            Point start, end;
            if(Math.Abs(pt1.Y-pt2.Y) <= Math.Abs(pt1.X-pt2.X)) {
                //横長の場合
                if(pt1.X < pt2.X) {
                    start = new Point(pt1.X, pt1.Y);
                    end = new Point(pt2.X, pt2.Y);
                } else {
                    start = new Point(pt2.X, pt2.Y);
                    end = new Point(pt1.X, pt1.Y);
                }
            } else {
                //縦長の場合
                if(pt1.Y < pt2.Y) {
                    start = new Point(pt1.X, pt1.Y);
                    end = new Point(pt2.X, pt2.Y);
                } else {
                    start = new Point(pt2.X, pt2.Y);
                    end = new Point(pt1.X, pt1.Y);
                }
            }

            Int32 w = (end.X - start.X),
                  h = (end.Y - start.Y);

            if(w == 0 && h == 0) {
                putPoint(pt1, size, color);
                return;
            }else if(w == 0) {
                putVLine(pt1, pt2.Y-pt1.Y, size, color);
                return;
            }else if(h == 0) {
                putHLine(pt1, pt2.X-pt1.X, size, color);
                return;
            }

            Point pt = new Point();
            float r = (float)h / (float)w;
            float e;
            if(Math.Abs(r) <= 1.0f) {
                //横長の場合
                e = (float)-w;
                for(Int32 x=0,y=0; x<=w; x++)
                {
                    pt.X = start.X + x;
                    pt.Y = start.Y + y;

                    putPoint(pt, size, color);

                    e += 2.0f * (float)Math.Abs(h);
                    if(e >= w) {
                        y += (h > 0) ? 1 : -1;
                        e -= 2.0f * w;
                    }
                }
            } else {
                //縦長の場合
                e = (float)-h;
                for(Int32 x=0,y=0; y<=h; y++)
                {
                    pt.X = start.X + x;
                    pt.Y = start.Y + y;

                    putPoint(pt, size, color);

                    e += 2.0f * Math.Abs(w);
                    if(e >= h) {
                        x += (w > 0) ? 1 : -1;
                        e -= 2.0f * h;
                    }
                }
            }
        }


        /// <summary>
        /// サーフェイスに水平線を置くメソッド
        /// </summary>
        /// <param name="pos">配置する位置</param>
        /// <param name="l">長さ</param>
        /// <param name="size">ペンのサイズ</param>
        /// <param name="color">色</param>
        public void putHLine(Point pos, Int32 l, Int32 size, Byte color)
        {
            if(Math.Abs(l) <= 1) {
                putPoint(pos, size, color);
                return;
            }

            Point position = new Point();
            Int32 dim = (l > 0) ? 1 : -1;
            for(Int32 p=0; p<Math.Abs(l); p++)
            {
                position.X = pos.X + (p * dim);
                position.Y = pos.Y;
                putPoint(position, size, color);
            }
        }


        /// <summary>
        /// サーフェイスに垂直線を置くメソッド
        /// </summary>
        /// <param name="pos">配置する位置</param>
        /// <param name="l">長さ</param>
        /// <param name="size">ペンのサイズ</param>
        /// <param name="color">色</param>
        public void putVLine(Point pos, Int32 l, Int32 size, Byte color)
        {
            if(Math.Abs(l) <= 1) {
                putPoint(pos, size, color);
                return;
            }

            Point position = new Point();
            Int32 dim = (l > 0) ? 1 : -1;
            for(Int32 p=0; p<Math.Abs(l); p++)
            {
                position.X = pos.X;
                position.Y = pos.Y + (p * dim);
                putPoint(position, size, color);
            }
        }


        /// <summary>
        /// サーフェースに点を置くメソッド
        /// </summary>
        /// <param name="p">点を置く座標</param>
        /// <param name="size">ペンのサイズ</param>
        /// <param name="color">色</param>
        public void putPoint(Point p, Int32 size, Byte color)
        {
            Int32 ptx,pty;
            for(Int32 y=-(size-1); y<size; y++)
            {
                pty = p.Y + y;
                if(pty < 0 || pty >= _h)
                {
                    continue;
                }
                for(Int32 x=-(size-1); x<size; x++)
                {
                    ptx = p.X + x;
                    if(ptx < 0 || ptx >= _w)
                    {
                        continue;
                    }
                    _buffer[ptx,pty] = color;
                }
            }
            return;
        }

        
        /// <summary>
        /// 他のサーフェイスのデータを自分自身にコピーする
        /// </summary>
        /// <param name="dstX">コピー先のX座標</param>
        /// <param name="dstY">コピー先のY座標</param>
        /// <param name="src">コピー元のサーフェイス</param>
        /// <param name="srcX">コピー元のX座標</param>
        /// <param name="srcY">コピー元のY座標</param>
        /// <param name="w">横幅</param>
        /// <param name="h">縦幅</param>
        public void copy(Int32 dstX, Int32 dstY, Surface src, Int32 srcX, Int32 srcY, Int32 w, Int32 h)
        {
            Byte[,] srcBuffer = src.getBuffer();
            
            //コピー領域のクリッピング
            //画像が転送元、転送先の領域からはみ出していればそれを矯正する。
            Rectangle dstRect = getRect(),
                      srcRect = src.getRect();

            dstRect.Intersect(new Rectangle(dstX, dstY, w, h));
            srcRect.Intersect(new Rectangle(srcX, srcY, w, h));

            //実際にコピーする領域の幅・高さはw,hで良いとは限らない。
            //コピー先、コピー元の領域の幅のうち、小さい方の値を使用する
            Int32 copyW = Math.Min(dstRect.Width, srcRect.Width),
                  copyH = Math.Min(dstRect.Height, srcRect.Height);

            if(copyW < 1 || copyH < 1)
            {
                return;
            }

            Int32 ptDstX,ptDstY,ptSrcX,ptSrcY;
            for(Int32 y=0; y < copyH; y++)
            {
                for(Int32 x=0; x < copyW; x++)
                {
                    ptDstX = dstRect.Left + x;
                    ptDstY = dstRect.Top  + y;
                    ptSrcX = srcRect.Left + x;
                    ptSrcY = srcRect.Top  + y;

                    _buffer[ptDstX,ptDstY] = srcBuffer[ptSrcX,ptSrcY];
                }
            }
        }


        /// <summary>
        /// バッファを返す
        /// </summary>
        /// <returns>サーフェイスのバッファ</returns>
        public byte[,] getBuffer()
        { return _buffer; }


        /// <summary>
        /// サーフェイスのサイズをRectangleで返す
        /// </summary>
        /// <returns>サーフェイスのサイズを表すRectangleオブジェクト</returns>
        public Rectangle getRect()
        {
            return new Rectangle(0, 0, _w, _h);
        }

        
        #region ICloneable メンバ

        /// <summary>
        /// 自分自身の複製を返す
        /// </summary>
        /// <returns>自分自身の複製</returns>
        public object Clone()
        {
            Surface clone = new Surface(_w, _h);
            clone.copy(0,0, this, 0, 0, _w, _h);
            return clone;
        }

        #endregion
    }
}
