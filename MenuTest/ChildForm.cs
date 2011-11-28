using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MenuTest
{
    /// <summary>
    /// MDI�̎q�E�B���h�E�B
    /// �h�L�������g�̃r���[�B
    /// </summary>
    public partial class ChildForm : Form
    {
        /// <summary>
        /// ���̃r���[���Q�Ƃ���h�L�������g
        /// </summary>
        private Document _doc;

        /// <summary>
        /// �L�����o�X�i�y�����ŕ`�悷��̈�j
        /// </summary>
        private Canvas _ctrlCanvas;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="doc">�h�L�������g</param>
        public ChildForm(Document doc)
        {
            _doc = doc;

            //Visual Studio�̃t�H�[���f�U�C�i��������
            //UI�������R�[�h�̎��s
            InitializeComponent();

            //�E�B���h�E�^�C�g���̐ݒ�B
            //�t�@�C���������肵�Ă���΃t�@�C�����B
            //���肵�Ă��Ȃ���΃h�L�������g+ID
            if(_doc.FileName != "") {
                this.Text = _doc.FileName;
            } else {
                this.Text = String.Format("�h�L�������g{0}", _doc.ID);
            }

            //�h�L�������g�̕ύX���Ď�����B
            _doc.DocumentStateChangeEvent += onDocumentChange;

            //�L�����o�X�̔z�u
            _ctrlCanvas = new Canvas(_doc);
            ((System.ComponentModel.ISupportInitialize)_ctrlCanvas).BeginInit();
            this.Controls.Add(_ctrlCanvas);

            _ctrlCanvas.Dock = DockStyle.Fill;
            _ctrlCanvas.Width = 100;
            _ctrlCanvas.Height = 200;
            _ctrlCanvas.BorderStyle = BorderStyle.Fixed3D;
            ((System.ComponentModel.ISupportInitialize)_ctrlCanvas).EndInit();
            _ctrlCanvas.Show();
        }


        /// <summary>
        /// �t�H�[�����A�N�e�B�u�ɂȂ����ꍇ�B
        /// �������g�̃h�L�������g���A�N�e�B�u�ɂ���B
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�C�x���g</param>
        private void MapForm_Activated(object sender, EventArgs e)
        {
            MenuTest.Application app = MenuTest.Application.getInstance();

            //Application����onActiveDocumentChange�C�x���g����������
            app.ActiveDocument = _doc;
        }
        

        /// <summary>
        /// �t�H�[�����A�N�e�B�u�ł͂Ȃ��Ȃ����ꍇ
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�C�x���g</param>
        private void MapForm_Deactivate(object sender, EventArgs e)
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            if(!app.isActiveDocument(_doc)) {
                return;
            }
            
            //Application����onActiveDocumentChange�C�x���g����������
            //�����̎q�E�B���h�E�����݂���ꍇ�͑��̃E�B���h�E��
            //�����ɃA�N�e�B�u�ɂȂ�̂�null�ɐݒ肷��B
            app.ActiveDocument = null;
        }


        /// <summary>
        /// �h�L�������g�̏�Ԃ��ω�����ƌĂяo�����
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�C�x���g</param>
        private void onDocumentChange(object sender, EventArgs e)
        {
            //���̓h�L�������g�ɂǂ�ȏ����ȕύX�������Ă��ʒm�����B
            //�����Ă���f�[�^�̐���ʂ����Ȃ��ꍇ�͂���ő��v������
            //�f�[�^�ʂ���������A�K�͂��傫���Ȃ��Ă����ꍇ��
            //�C�x���g�𕪂��邩�AEventArgs�Ŕ��f���邩�A
            //�I���W�i���̃p�����[�^���������V�����C�x���g���`����B

            ctrlPenSizeLabel.Text = "�y���T�C�Y�F" + _doc.PenSize.ToString();
        }
    }
}