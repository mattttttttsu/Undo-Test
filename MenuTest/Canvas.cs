using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MenuTest
{

    /// <summary>
    /// �T�[�t�F�C�X�̓��e��`�悵�A
    /// ���[�U�[����̓��͂���������I�u�W�F�N�g
    /// </summary>
    public class Canvas : PictureBox
    {
        /// <summary>
        /// �\������T�[�t�F�C�X
        /// </summary>
        private Surface _surface;

        /// <summary>
        /// �e�X�g�p
        /// </summary>
        private SolidBrush _drawBrush;
        private Pen _pen;

        /// <summary>
        /// �O���b�h�̕�
        /// </summary>
        private Int32 _gridW = 16;

        /// <summary>
        /// �O���b�h�̏c��
        /// </summary>
        private Int32 _gridH = 16;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="doc">�h�L�������g</param>
        public Canvas(Document doc)
        {
            _surface = doc.Surface;
            _drawBrush = new SolidBrush(Color.Black);
            _pen = new Pen(_drawBrush);
            this.DoubleBuffered = true;

            doc.SurfaceChangeEvent += new Document.SurfaceChangeEventHandler(onSurfaceChange);
        }


        /// <summary>
        /// �T�[�t�F�C�X�̓��e�ɕω����������ꍇ��
        /// �Ăяo�����B
        /// </summary>
        /// <param name="sender">�Ăяo�����Ƃ̃I�u�W�F�N�g</param>
        /// <param name="rect">�ύX���������̈�̍��W�B���݂͖�����</param>
        public void onSurfaceChange(object sender, Rectangle rect)
        {
            //�������g���ĕ`�悷��
            Invalidate();
        }


        /// <summary>
        /// �T�[�t�F�C�X�̓��e��`�悷��
        /// onPaint�̒��ŃT�[�t�F�C�X�̕`�揈�����s���͖̂��ʂ�����B
        /// ���炩���߃o�b�N�o�b�t�@�ɕ`�悵�Ă����āA�����ł͒P����
        /// �ꖇ�̉摜�Ƃ��ē]������悤�ɂ���B
        /// </summary>
        /// <param name="pe">�y�C���g�C�x���g</param>
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
        /// �}�E�X�{�^�������������̏���
        /// </summary>
        /// <param name="e">�}�E�X�C�x���g</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            //���ݑI�𒆂̃c�[�������s����
            MenuTest.Application app = MenuTest.Application.getInstance();
            if(app.Tool != null)
            {
                //�}�E�X�J�[�\���̍��W���L�����o�X���W�ɒ����Ă���n��
                app.Tool.onMouseDown(this, _getSurfaceMouseEvent(e));
            }
        }

        
        /// <summary>
        /// �}�E�X�J�[�\�����ړ��������̏���
        /// </summary>
        /// <param name="e">�}�E�X�C�x���g</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            //���ݑI�𒆂̃c�[�������s����
            MenuTest.Application app = MenuTest.Application.getInstance();
            if(app.Tool != null)
            {
                //�}�E�X�J�[�\���̍��W���L�����o�X���W�ɒ����Ă���n��
                app.Tool.onMouseMove(this, _getSurfaceMouseEvent(e));
            }
        }


        /// <summary>
        /// �}�E�X�{�^�����グ�����̏���
        /// </summary>
        /// <param name="e">�}�E�X�C�x���g</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            //���ݑI�𒆂̃c�[�������s����
            MenuTest.Application app = MenuTest.Application.getInstance();
            if(app.Tool != null)
            {
                //�}�E�X�J�[�\���̍��W���L�����o�X���W�ɒ����Ă���n��
                app.Tool.onMouseUp(this, _getSurfaceMouseEvent(e));
            }
        }


        /// <summary>
        /// �X�N���[����Y�[���ɍ��킹�ĕϊ������V����
        /// �}�E�X�C�x���g��Ԃ��B
        /// </summary>
        /// <param name="e">�}�E�X�C�x���g</param>
        /// <returns>�ϊ���̃}�E�X�C�x���g</returns>
        protected MouseEventArgs _getSurfaceMouseEvent(MouseEventArgs e)
        {
            Point p = canvasToSurface(new Point(e.X, e.Y));
            return new MouseEventArgs(e.Button, e.Clicks, p.X, p.Y, e.Delta);
        }


        /// <summary>
        /// �T�[�t�F�C�X���W����L�����o�X��̍��W�֍��W�ϊ����s��
        /// </summary>
        /// <param name="p">�T�[�t�F�C�X���W</param>
        /// <returns>�L�����o�X���W</returns>
        public Point surfaceToCanvas(Point p)
        {
            return new Point(p.X * _gridW, p.Y * _gridH);
        }


        /// <summary>
        /// �L�����o�X���W����T�[�t�F�C�X��̍��W�֍��W�ϊ����s��
        /// </summary>
        /// <param name="p">�L�����o�X���W</param>
        /// <returns>�T�[�t�F�C�X���W</returns>
        public Point canvasToSurface(Point p)
        {
            return new Point(p.X / _gridW, p.Y / _gridH);
        }

    }
}
