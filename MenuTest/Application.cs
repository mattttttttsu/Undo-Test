using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MenuTest.Command;
using MenuTest.Menu;

namespace MenuTest
{
    /// <summary>
    /// �A�v���P�[�V�����N���X
    /// </summary>
    public class Application
    {
        /// <summary>
        /// �������g�̃C���X�^���X
        /// </summary>
        private static Application _instance;

        /// <summary>
        /// �h�L�������g�̃��X�g
        /// </summary>
        private List<Document> _documents;


        /// <summary>
        /// �A�N�e�B�u�ȃh�L�������g
        /// </summary>
        private Document _activeDocument;

        /// <summary>
        /// �h�L�������g��ID���Ǘ�����V�[�P���X
        /// </summary>
        private Int32 _docSeq;

        /// <summary>
        ///�A�v���P�[�V�����̃��C���E�B���h�E�B
        ///MDI�̐e�E�B���h�E�Ƃ��ė��p����
        /// </summary>
        private Form1 _mainFrame;

        /// <summary>
        ///�A�N�e�B�u�ȃc�[��
        /// </summary>
        private ITool _tool;

        /// <summary>
        ///�A�N�e�B�u�ȃh�L�������g���ω��������ɌĂяo�����
        /// </summary>
        public event EventHandler ActiveDocumentChange;

        /// <summary>
        /// �R���X�g���N�^
        /// ����new�ł��Ȃ��悤��private�Œ�`�B
        /// </summary>
        private Application()
        {
            _documents = new List<Document>();
            _activeDocument = null;

            //Application�N���X�̒��Ń��C���E�B���h�E������������
            _mainFrame = new Form1();

            _tool = new PenTool();

            _docSeq = 1;
        }


        /// <summary>
        /// Application�N���X�̃C���X�^���X��Ԃ�
        /// �A�v���P�[�V�����N���X�͏�ɂ��̃��\�b�h�Ŏ擾����
        /// </summary>
        /// <returns>�A�v���P�[�V�����N���X�̃C���X�^���X</returns>
        public static Application getInstance()
        {
            if(_instance == null)
            {
                _instance = new Application();
            }
            return _instance;
        }


        /// <summary>
        /// �h�L�������g�̐V�K�쐬���s��
        /// </summary>
        public void newDocument()
        {
            //�V����ID�̃h�L�������g���쐬���A
            //�q�t�H�[�����쐬�B
            Document doc = new Document(_docSeq++);
            _documents.Add(doc);

            ChildForm newForm = new ChildForm(doc);
            _mainFrame.addChildView(newForm);
        }


        /// <summary>
        /// �w�肵���t�@�C����ǂݍ���Ńh�L�������g���쐬����
        /// </summary>
        /// <param name="fileName">�J���t�@�C���̖��O</param>
        public void openDocument(string fileName)
        {
            //������
            MessageBox.Show("�t�@�C���J��");
        }


        /// <summary>
        /// �A�N�e�B�u�ȃh�L�������g�̎擾�E�ݒ���s���v���p�e�B
        /// </summary>
        public Document ActiveDocument
        {
            get { return _activeDocument; }
            set
            {
                _activeDocument = value;
                //�A�N�e�B�u�ȃh�L�������g���ω������̂�
                //�C�x���g���N������
                notifyActiveDocumentChange();
            }
        }


        /// <summary>
        /// �A�N�e�B�u�ȃh�L�������g���ǂ�����Ԃ�
        /// </summary>
        /// <param name="doc">���肷��h�L�������g</param>
        /// <returns>
        /// true  => �A�N�e�B�u
        /// false => �A�N�e�B�u�ł͂Ȃ�
        /// </returns>
        public Boolean isActiveDocument(Document doc)
        {
            if(_activeDocument == null) {
                return false;
            }
            return _activeDocument.Equals(doc);
        }


        /// <summary>
        /// ���C���E�B���h�E��Ԃ��v���p�e�B
        /// </summary>
        public System.Windows.Forms.Form MainFrame
        { get { return _mainFrame; } }


        /// <summary>
        /// �A�N�e�B�u�ȃh�L�������g�ɕω������������Ƃ�
        /// �ʒm����C�x���g���N������B
        ///
        /// Application�N���X�ȊO�̃N���X(Document�N���X�Ȃ�)�����
        /// ActiveDocumentChange�C�x���g�������������Ȃ��̂ŁA
        /// ����ɂ��̃��\�b�h���Ăяo���悤�ɂ���B
        /// </summary>
        public void notifyActiveDocumentChange()
        {
            ActiveDocumentChange(this, null);
        }


        /// <summary>
        /// �c�[����ݒ肷��B
        /// ���̓c�[��ID�͂����̐��������A�����
        /// �萔���ɂ���K�v������B�Z�b�g����c�[��������new���Ȃ��B
        /// </summary>
        /// <param name="toolId">�c�[��ID</param>
        public void setTool(Int32 toolId)
        {
            if(_tool != null && _tool.ToolId == toolId) {
                return;
            }

            _tool.onDeactivate();

            switch(toolId)
            {
            case 1: _tool = new PenTool();  break;
            case 2: _tool = new LineTool(); break;
            }

            _tool.onActivate();

            //���͂���Ńc�[���o�[�ɕύX��ʒm���邵���Ȃ��B
            //�ʂȎ�i��p�ӂ���K�v������B
            notifyActiveDocumentChange();
        }


        /// <summary>
        /// ���ݑI�𒆂̃c�[����Ԃ��B
        /// ��œǂݍ��ݐ�p�̃v���p�e�B�ɕύX����
        /// </summary>
        public ITool Tool
        { get { return _tool; } }
    }
}
