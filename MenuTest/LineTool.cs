using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MenuTest
{
    /// <summary>
    /// �����������c�[��
    /// </summary>
    public class LineTool : ITool
    {
        /// <summary>
        /// �}�E�X�����������ǂ����̃t���O
        /// </summary>
        private Boolean _mouseDownFlag;

        /// <summary>
        /// �n�_�̍��W
        /// </summary>
        private Point _startPoint;

        /// <summary>
        /// �I�_�̍��W
        /// </summary>
        private Point _endPoint;

        /// <summary>
        /// �v���r���[�p�̃y���I�u�W�F�N�g
        /// </summary>
        private Pen _pen = null;

        #region ITool �����o

        /// <summary>
        /// �c�[��ID
        /// </summary>
        public int ToolId
        { get { return 2; } }


        /// <summary>
        /// �c�[�����A�N�e�B�u�ɂȂ������̏���
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
        /// �c�[�����A�N�e�B�u�łȂ��Ȃ������̏���
        /// </summary>
        public void onDeactivate()
        {
            _pen.Dispose();
            _pen = null;
        }


        /// <summary>
        /// �}�E�X�{�^�������������̏���
        /// </summary>
        /// <param name="sender">�C�x���g�����������I�u�W�F�N�g</param>
        /// <param name="e">�}�E�X�C�x���g</param>
        public void onMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(_mouseDownFlag) {
                return;
            }

            _mouseDownFlag = true;
            _startPoint = e.Location;
        }


        /// <summary>
        /// �}�E�X�{�^���𗣂������̏���
        /// </summary>
        /// <param name="sender">�C�x���g�����������I�u�W�F�N�g</param>
        /// <param name="e">�}�E�X�C�x���g</param>
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

            //�������T�[�t�F�C�X�ɕ`�悷��
            doc.putLine(_startPoint, e.Location);

            //historyAction�����
            Surface s  = doc.Surface.Clone() as Surface,
                    bs = doc.BackSurface.Clone() as Surface;
            SurfaceCopyAction sca = new SurfaceCopyAction(doc, s, bs);
            doc.addEdit(sca);

            //�T�[�t�F�C�X�̓��e���o�b�N�T�[�t�F�C�X(�A���h�D�p)�փR�s�[
            doc.endEdit();
        }


        /// <summary>
        /// �}�E�X���ړ��������̏���
        /// </summary>
        /// <param name="sender">�C�x���g�����������I�u�W�F�N�g</param>
        /// <param name="e">�}�E�X�C�x���g</param>
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
        /// <param name="sender">�C�x���g�����������I�u�W�F�N�g(=canvas)</param>
        /// <param name="e">�`��C�x���g</param>
        public void onPaint(object sender, PaintEventArgs e)
        {
            if(!_mouseDownFlag) {
                return;
            }

            Canvas c = sender as Canvas;
            if(c == null) {
                //�L�����o�X�ȊO�̃I�u�W�F�N�g���甭������͂��͂Ȃ�
                return;
            }

            Point start = c.surfaceToCanvas(_startPoint),
                  end   = c.surfaceToCanvas(_endPoint);

            e.Graphics.DrawLine(_pen, start, end);
        }


        #endregion
    }
}
