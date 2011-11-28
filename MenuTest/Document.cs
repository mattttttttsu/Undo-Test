using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace MenuTest
{
    /// <summary>
    /// �h�L�������g�̃N���X
    /// </summary>
    public class Document
    {
        /// <summary>
        /// �h�L�������gID
        /// </summary>
        private Int32 _id;

        /// <summary>
        /// �h�L�������g�ɕύX�����������ǂ���
        /// </summary>
        private bool _dirty;

        /// <summary>
        /// �h�L�������g�̃t�@�C����
        /// �V�K�쐬���͋�
        /// </summary>
        private String _fileName;

        /// <summary>
        /// ��Ɨp�T�[�t�F�C�X
        /// </summary>
        private Surface _surface;

        /// <summary>
        /// �o�b�N�A�b�v�p�T�[�t�F�C�X
        /// �A���h�D���\�ɂ��邽�߁A���O�̕ҏW��
        /// �T�[�t�F�C�X�̓��e��ێ�����B
        /// </summary>
        private Surface _backSurface;

        /// <summary>
        /// �A���h�D�\�ȃA�N�V�����̃��X�g
        /// </summary>
        private HistoryActionList _historyList;

        /// <summary>
        /// �y���T�C�Y
        /// </summary>
        private Int32 _penSize;

        /// <summary>
        /// �h�L�������g�̏�ԂɕύX���������ꍇ�ɌĂяo�����C�x���g
        /// </summary>
        public event EventHandler DocumentStateChangeEvent;

        /// <summary>
        /// �T�[�t�F�C�X�̏�ԂɕύX�����������Ƃ�m�邽�߂̃f���Q�[�g
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="rect">�ύX�̂������̈�</param>
        public delegate void SurfaceChangeEventHandler(Object sender, Rectangle rect);

        /// <summary>
        /// �T�[�t�F�C�X�ɕύX���N�������ɔ�������C�x���g
        /// </summary>
        public event SurfaceChangeEventHandler SurfaceChangeEvent;

        

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="id">�h�L�������gID</param>
        public Document(Int32 id)
        {
            _id = id;
            _penSize = 1;
            _fileName = "";
            _dirty = false;

            _surface = new Surface(50, 50);
            _backSurface = _surface.Clone() as Surface;
            _historyList = new HistoryActionList();
        }

        /// <summary>
        /// ID��Ԃ��v���p�e�B
        /// </summary>
        public Int32 ID
        { get { return _id; } }


        /// <summary>
        /// �t�@�C�����̃v���p�e�B
        /// </summary>
        public String FileName
        { get { return _fileName; } }


        /// <summary>
        /// �y���T�C�Y�̃v���p�e�B
        /// </summary>
        public Int32 PenSize
        { get { return _penSize; } }


        /// <summary>
        /// �h�L�������g���ύX���ꂽ���ǂ����̃t���O
        /// </summary>
        public Boolean Dirty
        { get { return _dirty; } }


        /// <summary>
        /// �T�[�t�F�C�X��Ԃ��v���p�e�B
        /// </summary>
        public Surface Surface
        { get { return _surface; } }


        /// <summary>
        /// �o�b�N�T�[�t�F�C�X��Ԃ�
        /// </summary>
        public Surface BackSurface
        { get { return _backSurface; } }


        /// <summary>
        /// �A���h�D�\�ȃA�N�V�����̃��X�g��Ԃ�
        /// </summary>
        public HistoryActionList HistoryList
        { get { return _historyList; } }


        /// <summary>
        /// �h�L�������g�̃t�@�C�������Z�b�g����
        /// </summary>
        /// <param name="fileName">�t�@�C����</param>
        public void setFileName(String fileName)
        {
            _fileName = fileName;
            update();
        }


        /// <summary>
        /// �y���T�C�Y���Z�b�g����
        /// </summary>
        /// <param name="size">�V�����y���T�C�Y</param>
        public void setPenSize(Int32 size)
        {
            _penSize = size;
            update();
        }


        /// <summary>
        /// �h�L�������g�̕ύX�t���O�𒼐ڕύX����
        /// </summary>
        /// <param name="newVal">�V�������</param>
        public void setDirty(Boolean newVal)
        {
            _dirty = newVal;
            update();
        }


        /// <summary>
        /// �T�[�t�F�C�X�ɑB��`��
        /// </summary>
        /// <param name="start">�n�_</param>
        /// <param name="end">�I�_</param>
        public void putLine(Point start, Point end)
        {
            _surface.putLine(start, end, _penSize, 1);
            
            //�T�[�t�F�C�X���Ď����Ă���I�u�W�F�N�g�ɒʒm����
            if(SurfaceChangeEvent != null)
            {
                Int32 x = 0,
                      y = 0,
                      w = _surface.W,
                      h = _surface.H;

                Rectangle rect = new Rectangle(x, y, w, h);
                SurfaceChangeEvent(this, rect);
            }
            
        }


        /// <summary>
        /// �T�[�t�F�C�X�ɑ΂��ĘA�������ҏW���s������
        /// �Ăяo�����\�b�h�B
        /// </summary>
        public void beginEdit()
        {
            //���͉������Ȃ��B
            //��X�AendEdit���Ăяo�����܂ł̊Ԃɍs��ꂽ
            //�ҏW��JoinedHistoryAction�Ƃ������������O�̃I�u�W�F�N�g��
            //���ߍ��ނ悤�Ȏd�l�ɕύX����K�v������B
            return;
        }


        /// <summary>
        /// �A���h�D�\�ȃA�N�V�����𗚗����X�g�ɒǉ�����
        /// </summary>
        /// <param name="action">�A�N�V�����I�u�W�F�N�g</param>
        public void addEdit(IHistoryAction action)
        {
            //��X�AbeginEdit()���Ă΂�Ă���Ԃ�joinedHistoryAction��
            //�������~�ς����悤�ɕύX����B
            _historyList.add(action);
        }


        /// <summary>
        /// �c�[���ł̕ҏW���I��������ɌĂяo�����֐��B
        /// �T�[�t�F�C�X�̓��e���o�b�N�T�[�t�F�C�X�ɃR�s�[����B
        /// ���̎d�l�ł́A�T�[�t�F�C�X�ւ̕ҏW���I������A
        /// beginEdit()���Ăяo���Ă��Ȃ��Ă��K���ĂԕK�v������B
        /// </summary>
        public void endEdit()
        {
            //��X�AjoinedHistoryAction������ꍇ�͂����
            //undoHistoryAction�ɒǉ����鏈�������s����B
            //�������~�ς����悤�ɕύX����B

            //�T�[�t�F�C�X�̓��e���o�b�N�T�[�t�F�C�X(undo�p�T�[�t�F�C�X)�ɃR�s�[����B
            //�������C���[�̃A�v���̏ꍇ�͂ǂ����邩�l����K�v������B
            //�}�b�v�G�f�B�^�̂悤�ɗl�X�Ȏ�ނ̃I�u�W�F�N�g��z�u����ꍇ���܂߁A
            //���̃R�[�h�͂����ɒu���Ă����Ȃ��B
            _backSurface.copy(0, 0, _surface, 0, 0, _surface.W, _surface.H);
            Rectangle rc = new Rectangle(0, 0, _surface.W, _surface.H);
            SurfaceChangeEvent(this, rc);


            update();
        }


        /// <summary>
        /// �h�L�������g�̓��e���ύX���ꂽ�̂ŃC�x���g�𔭐�������B
        /// </summary>
        private void update()
        {
            //�h�L�������g�̏�Ԃ��ύX���ꂽ���Ƃ�ʒm����B
            DocumentStateChangeEvent(this, null);

            //���������g���A�N�e�B�u�Ȃ�A
            //�A�N�e�B�u�h�L�������g�̕ύX�C�x���g�𔭐�������B
            //�c�[���o�[���A�A�N�e�B�u�ȃh�L�������g�̕ω���ǐՂł��Ȃ��̂�
            //�����ł��������������K�v�ɂȂ��Ă��邯�ǁA�{���͕s�v�ȏ����B
            MenuTest.Application app = MenuTest.Application.getInstance();
            if(app.ActiveDocument == this) {
                app.notifyActiveDocumentChange();
            }
        }


        /// <summary>
        /// �T�[�t�F�C�X���X�V�������Ƃ��r���[�ɒʒm����
        /// �c�[������L�����o�X���ĕ`�悷���i���Ȃ�����
        /// ��ނ𓾂��ǉ��B
        /// @param rc �ĕ`�悪�K�v�ȗ̈�(������)
        /// </summary>
        /// <param name="rc"></param>
        public void updateSurface(Rectangle rc)
        {
            SurfaceChangeEvent(this, rc);
        }
    }
}
