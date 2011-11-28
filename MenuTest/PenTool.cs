using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MenuTest
{
    /// <summary>
    /// �y���c�[���̃N���X
    /// </summary>
    public class PenTool : ITool
    {
        /// <summary>
        /// �}�E�X����������
        /// (�����Ă���Ԃ͍�Ɨp�L�����o�X�ɕ`�悷��)
        /// </summary>
        private Boolean _mouseDownFlag;

        /// <summary>
        /// ���O�̃}�E�X�J�[�\���̈ʒu
        /// </summary>
        private Point _lastPoint;
        

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public PenTool()
        {
            _mouseDownFlag = false;
        }


        #region ITool �����o

        /// <summary>
        /// �c�[��ID��Ԃ��v���p�e�B
        /// </summary>
        public int ToolId
        { get { return 1; } }


        /// <summary>
        /// �A�N�e�B�u�ɂȂ������ɌĂяo����鏈��
        /// </summary>
        public void onActivate()
        {
            _mouseDownFlag = false;
        }


        /// <summary>
        /// �A�N�e�B�u�ł͂Ȃ��Ȃ������̏���
        /// </summary>
        public void onDeactivate()
        {
            _mouseDownFlag = false;
        }


        /// <summary>
        /// �}�E�X�{�^�������������̏���
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�}�E�X�J�[�\���̏����܂ރI�u�W�F�N�g</param>
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
        /// �}�E�X�{�^���𗣂������̏���
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�}�E�X�J�[�\���̏����܂ރI�u�W�F�N�g</param>
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

            //historyAction�����
            Surface s  = doc.Surface.Clone() as Surface,
                    bs = doc.BackSurface.Clone() as Surface;
            SurfaceCopyAction sca = new SurfaceCopyAction(doc, s, bs);
            doc.addEdit(sca);

            //�T�[�t�F�C�X�̓��e���o�b�N�T�[�t�F�C�X(�A���h�D�p)�փR�s�[
            doc.endEdit();
        }


        /// <summary>
        /// �}�E�X�J�[�\�����ړ��������̏���
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�}�E�X�J�[�\���̏����܂ރI�u�W�F�N�g</param>
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
        /// �L�����o�X��`�悷�鎞�ɌĂяo����鏈��
        /// </summary>
        /// <param name="sender">�C�x���g�����������I�u�W�F�N�g(=canvas)</param>
        /// <param name="e">�`��C�x���g</param>
        public void onPaint(object sender, PaintEventArgs e)
        {
        }

        #endregion
    }
}
