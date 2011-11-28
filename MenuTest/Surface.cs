using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MenuTest
{
    /// <summary>
    /// �T�[�t�F�C�X�̃N���X
    /// </summary>
    public class Surface : ICloneable
    {
        /// <summary>
        /// ����
        /// </summary>
        private Int32 _w;

        /// <summary>
        /// �c��
        /// </summary>
        private Int32 _h;

        /// <summary>
        /// �o�b�t�@
        /// </summary>
        private Byte[,] _buffer;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="w">����</param>
        /// <param name="h">�c��</param>
        public Surface(Int32 w, Int32 h)
        {
            _w = w;
            _h = h;

            _buffer = new Byte[w,h];
        }


        /// <summary>
        /// ������Ԃ��v���p�e�B
        /// </summary>
        public Int32 W
        { get { return _w; } }


        /// <summary>
        /// �c����Ԃ��v���p�e�B
        /// </summary>
        public Int32 H
        { get { return _h; } }


        /// <summary>
        /// �T�[�t�F�C�X�ɒ������������\�b�h
        /// Bresenham�A���S���Y��(http://www2.starcat.ne.jp/~fussy/algo/algo1-1.htm)�̂���B
        /// </summary>
        /// <param name="pt1">�J�n�_</param>
        /// <param name="pt2">�I���_</param>
        /// <param name="size">�y���T�C�Y</param>
        /// <param name="color">�F</param>
        public void putLine(Point pt1, Point pt2, Int32 size, Byte color)
        {
            Point start, end;
            if(Math.Abs(pt1.Y-pt2.Y) <= Math.Abs(pt1.X-pt2.X)) {
                //�����̏ꍇ
                if(pt1.X < pt2.X) {
                    start = new Point(pt1.X, pt1.Y);
                    end = new Point(pt2.X, pt2.Y);
                } else {
                    start = new Point(pt2.X, pt2.Y);
                    end = new Point(pt1.X, pt1.Y);
                }
            } else {
                //�c���̏ꍇ
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
                //�����̏ꍇ
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
                //�c���̏ꍇ
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
        /// �T�[�t�F�C�X�ɐ�������u�����\�b�h
        /// </summary>
        /// <param name="pos">�z�u����ʒu</param>
        /// <param name="l">����</param>
        /// <param name="size">�y���̃T�C�Y</param>
        /// <param name="color">�F</param>
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
        /// �T�[�t�F�C�X�ɐ�������u�����\�b�h
        /// </summary>
        /// <param name="pos">�z�u����ʒu</param>
        /// <param name="l">����</param>
        /// <param name="size">�y���̃T�C�Y</param>
        /// <param name="color">�F</param>
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
        /// �T�[�t�F�[�X�ɓ_��u�����\�b�h
        /// </summary>
        /// <param name="p">�_��u�����W</param>
        /// <param name="size">�y���̃T�C�Y</param>
        /// <param name="color">�F</param>
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
        /// ���̃T�[�t�F�C�X�̃f�[�^���������g�ɃR�s�[����
        /// </summary>
        /// <param name="dstX">�R�s�[���X���W</param>
        /// <param name="dstY">�R�s�[���Y���W</param>
        /// <param name="src">�R�s�[���̃T�[�t�F�C�X</param>
        /// <param name="srcX">�R�s�[����X���W</param>
        /// <param name="srcY">�R�s�[����Y���W</param>
        /// <param name="w">����</param>
        /// <param name="h">�c��</param>
        public void copy(Int32 dstX, Int32 dstY, Surface src, Int32 srcX, Int32 srcY, Int32 w, Int32 h)
        {
            Byte[,] srcBuffer = src.getBuffer();
            
            //�R�s�[�̈�̃N���b�s���O
            //�摜���]�����A�]����̗̈悩��͂ݏo���Ă���΂������������B
            Rectangle dstRect = getRect(),
                      srcRect = src.getRect();

            dstRect.Intersect(new Rectangle(dstX, dstY, w, h));
            srcRect.Intersect(new Rectangle(srcX, srcY, w, h));

            //���ۂɃR�s�[����̈�̕��E������w,h�ŗǂ��Ƃ͌���Ȃ��B
            //�R�s�[��A�R�s�[���̗̈�̕��̂����A���������̒l���g�p����
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
        /// �o�b�t�@��Ԃ�
        /// </summary>
        /// <returns>�T�[�t�F�C�X�̃o�b�t�@</returns>
        public byte[,] getBuffer()
        { return _buffer; }


        /// <summary>
        /// �T�[�t�F�C�X�̃T�C�Y��Rectangle�ŕԂ�
        /// </summary>
        /// <returns>�T�[�t�F�C�X�̃T�C�Y��\��Rectangle�I�u�W�F�N�g</returns>
        public Rectangle getRect()
        {
            return new Rectangle(0, 0, _w, _h);
        }

        
        #region ICloneable �����o

        /// <summary>
        /// �������g�̕�����Ԃ�
        /// </summary>
        /// <returns>�������g�̕���</returns>
        public object Clone()
        {
            Surface clone = new Surface(_w, _h);
            clone.copy(0,0, this, 0, 0, _w, _h);
            return clone;
        }

        #endregion
    }
}
