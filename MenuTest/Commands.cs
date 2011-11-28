using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MenuTest.Command;

namespace MenuTest
{
    /// <summary>
    /// �V�K�쐬�R�}���h
    /// </summary>
    public class NewFileCommand : BaseCommand
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public NewFileCommand()
        {
            _commandId = "basic.file.newfile";
            _text = "�V�K�쐬";
            _desc = "�h�L�������g��V�K�쐬���܂��B";
        }

        
        /// <summary>
        /// �h�L�������g��V�K�ɍ쐬����B
        /// </summary>
        public override void execute()
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.newDocument();
        }
    }


    /// <summary>
    /// �t�@�C�����J���R�}���h
    /// </summary>
    public class OpenFileCommand : BaseCommand
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public OpenFileCommand()
        {
            _commandId = "basic.file.openfile";
            _text = "�J��";
            _desc = "�t�@�C�����J���܂��B";
        }

        /// <summary>
        /// �K���ȃt�@�C�����J��
        /// </summary>
        public override void execute()
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.openDocument("aaa.txt");
        }

    }


    /// <summary>
    /// �ۑ��R�}���h
    /// </summary>
    public class SaveFileCommand : BaseCommand
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public SaveFileCommand()
        {
            _commandId = "basic.file.savefile";
            _text = "�ۑ�";
            _desc = "�h�L�������g��ۑ����܂��B";

            //�A�N�e�B�u�ȃh�L�������g�̏�Ԃ��Ď�����
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.ActiveDocumentChange += onActiveDocumentChange;
        }


        /// <summary>
        /// �h�A�N�e�B�u�ȃL�������g��dirty�t���O��false�ɐݒ肷��B
        /// </summary>
        public override void execute()
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.ActiveDocument.setDirty(false);
        }


        /// <summary>
        /// �L����Ԃ�Ԃ�
        /// </summary>
        /// <returns>�R�}���h���L�����ǂ���</returns>
        public override Boolean isEnabled()
        {
            //�A�N�e�B�u�ȃh�L�������g�����݂��A
            //dirty�t���O�������Ă��鎖������
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.ActiveDocument != null && app.ActiveDocument.Dirty);
        }


        /// <summary>
        /// �A�N�e�B�u�ȃh�L�������g�ɕω������������ɌĂяo�����
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�C�x���g</param>

        public void onActiveDocumentChange(object sender, EventArgs e)
        {
            notifyStateChange();
        }
    }


    /// <summary>
    /// �I���R�}���h
    /// </summary>
    public class ExitCommand : BaseCommand
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public ExitCommand()
        {
            _commandId = "basic.file.exit";
            _text = "�I��";
            _desc = "�v���O�������I�����܂��B";
        }

        /// <summary>
        /// �A�v���P�[�V�������I������B
        /// </summary>
        public override void execute()
        {
            System.Windows.Forms.Application.Exit();
        }
    }


    /// <summary>
    /// �y���T�C�Y��ύX����R�}���h
    /// </summary>
    public class PenSizeChangeCommand : BaseCommand
    {
        /// <summary>
        /// �ύX��̃y���̃T�C�Y
        /// </summary>
        private Int32 _size;

        /// <summary>
        /// �R���X�g���N�^
        /// �p�����[�^�Ńy���T�C�Y���w�肷��B
        /// </summary>
        /// <param name="size">�y���T�C�Y</param>
        public PenSizeChangeCommand(Int32 size)
        {
            _size = size;
            _commandId = String.Format("basic.tool.pensize{0}", _size);
            _text = String.Format("�y���T�C�Y{0}", _size);
            _desc = String.Format("�y���T�C�Y��{0}�ɕύX", _size);
        }


        /// <summary>
        /// �A�N�e�B�u�ȃh�L�������g�̃y���T�C�Y��ύX����B
        /// </summary>
        public override void execute()
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            Document activeDoc = app.ActiveDocument;

            activeDoc.setPenSize(_size);
        }


        /// <summary>
        /// �L����Ԃ�Ԃ�
        /// </summary>
        /// <returns>�R�}���h�̗L�����</returns>
        public override bool isEnabled()
        {
            //�A�N�e�B�u�ȃh�L�������g�����݂��鎖������
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.ActiveDocument != null);
        }


        /// <summary>
        /// �`�F�b�N����Ă��邩��Ԃ�
        /// </summary>
        /// <returns>�`�F�b�N����Ă��邩�ǂ���</returns>
        public override bool isChecked()
        {
            //�A�N�e�B�u�ȃh�L�������g�̃y���T�C�Y��
            //�`�F�b�N����B�������g�̒l�ƈ�v�����
            //�`�F�b�N����Ă���Ƃ݂Ȃ��B
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.ActiveDocument != null && app.ActiveDocument.PenSize == _size);
        }
    }


    /// <summary>
    /// Undo�R�}���h
    /// </summary>
    public class UndoCommand : BaseCommand
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public UndoCommand()
        {
            _commandId = "basic.edit.undo";
            _text = "Undo";
            _desc = "�A���h�D�����s���܂��B";

            //�A�N�e�B�u�ȃh�L�������g�̏�Ԃ��Ď�����
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.ActiveDocumentChange += onActiveDocumentChange;
        }


        /// <summary>
        /// �h�A�N�e�B�u�ȃL�������g��dirty�t���O��false�ɐݒ肷��B
        /// </summary>
        public override void execute()
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.ActiveDocument.HistoryList.undo();
        }


        /// <summary>
        /// �L����Ԃ�Ԃ�
        /// </summary>
        /// <returns>�R�}���h�̗L�����</returns>
        public override Boolean isEnabled()
        {
            //�A�N�e�B�u�ȃh�L�������g�����݂��A
            //dirty�t���O�������Ă��鎖������
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.ActiveDocument != null && app.ActiveDocument.HistoryList.canUndo());
        }


        /// <summary>
        /// �A�N�e�B�u�ȃh�L�������g�ɕω������������ɌĂяo�����
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�C�x���g</param>
        public void onActiveDocumentChange(object sender, EventArgs e)
        {
            notifyStateChange();
        }
    }



    /// <summary>
    /// Redo�R�}���h
    /// </summary>
    public class RedoCommand : BaseCommand
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public RedoCommand()
        {
            _commandId = "basic.edit.redo";
            _text = "Redo";
            _desc = "���h�D�����s���܂��B";

            //�A�N�e�B�u�ȃh�L�������g�̏�Ԃ��Ď�����
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.ActiveDocumentChange += onActiveDocumentChange;
        }


        /// <summary>
        /// �h�A�N�e�B�u�ȃL�������g��dirty�t���O��false�ɐݒ肷��B
        /// </summary>
        public override void execute()
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.ActiveDocument.HistoryList.redo();
        }


        /// <summary>
        /// �L����Ԃ�Ԃ�
        /// </summary>
        /// <returns>�L�����</returns>
        public override Boolean isEnabled()
        {
            //�A�N�e�B�u�ȃh�L�������g�����݂��A
            //dirty�t���O�������Ă��鎖������
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.ActiveDocument != null && app.ActiveDocument.HistoryList.canRedo());
        }


        /// <summary>
        /// �A�N�e�B�u�ȃh�L�������g�ɕω������������ɌĂяo�����
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�C�x���g</param>
        public void onActiveDocumentChange(object sender, EventArgs e)
        {
            notifyStateChange();
        }
    }


    /// <summary>
    /// �y���c�[���I���R�}���h
    /// </summary>
    public class PenToolCommand : BaseCommand
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public PenToolCommand()
        {
            _commandId = "basic.tool.pen";
            _text = "pen";
            _desc = "�y���c�[��";

            //�A�N�e�B�u�ȃh�L�������g�̏�Ԃ��Ď�����
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.ActiveDocumentChange += onActiveDocumentChange;
        }


        /// <summary>
        /// �y���c�[����I������
        /// </summary>
        public override void execute()
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.setTool(1);
        }


        /// <summary>
        /// �L����Ԃ�Ԃ�
        /// </summary>
        /// <returns>�R�}���h�̗L�����</returns>
        public override Boolean isEnabled()
        {
            //�A�N�e�B�u�ȃh�L�������g�����݂���ΑI���\
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.ActiveDocument != null);
        }


        /// <summary>
        /// �`�F�b�N��Ԃ�Ԃ�
        /// </summary>
        /// <returns>�R�}���h�̃`�F�b�N�̏��</returns>
        public override bool isChecked()
        {
            //�A�N�e�B�u�ȃh�L�������g�����݂���ΑI���\
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.Tool.ToolId == 1);
        }


        /// <summary>
        /// �A�N�e�B�u�ȃh�L�������g�ɕω������������ɌĂяo�����
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�C�x���g</param>
        public void onActiveDocumentChange(object sender, EventArgs e)
        {
            notifyStateChange();
        }
    }


    /// <summary>
    /// �����c�[���I���R�}���h
    /// </summary>
    public class LineToolCommand : BaseCommand
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public LineToolCommand()
        {
            _commandId = "basic.tool.line";
            _text = "line";
            _desc = "�����c�[��";

            //�A�N�e�B�u�ȃh�L�������g�̏�Ԃ��Ď�����
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.ActiveDocumentChange += onActiveDocumentChange;
        }


        /// <summary>
        /// �����c�[����I������
        /// </summary>
        public override void execute()
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.setTool(2);
        }


        /// <summary>
        /// �L����Ԃ�Ԃ�
        /// </summary>
        /// <returns>�R�}���h�̗L�����</returns>
        public override Boolean isEnabled()
        {
            //�A�N�e�B�u�ȃh�L�������g�����݂���ΑI���\
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.ActiveDocument != null);
        }

        
        /// <summary>
        /// �`�F�b�N��Ԃ�Ԃ�
        /// </summary>
        /// <returns>�R�}���h�̃`�F�b�N�̏��</returns>
        public override bool isChecked()
        {
            //�A�N�e�B�u�ȃh�L�������g�����݂���ΑI���\
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.Tool.ToolId == 2);
        }


        /// <summary>
        /// �A�N�e�B�u�ȃh�L�������g�ɕω������������ɌĂяo�����
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�C�x���g</param>
        public void onActiveDocumentChange(object sender, EventArgs e)
        {
            notifyStateChange();
        }
    }

}
