using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MenuTest.Command;
using MenuTest.Menu;

namespace MenuTest
{
    /// <summary>
    /// �c�[���o�[�̃X�g���b�v
    /// </summary>
    class MainToolStrip : ToolStrip
    {
        /// <summary>
        /// �V�K�쐬�{�^��
        /// </summary>
        private CommandMenuButton _newButton;
        /// <summary>
        /// �t�@�C�����J���{�^��
        /// </summary>
        private CommandMenuButton _openButton;
        /// <summary>
        /// �ۑ��{�^��
        /// </summary>
        private CommandMenuButton _saveButton;

        /// <summary>
        /// Undo�{�^��
        /// </summary>
        private CommandMenuButton _undoButton;

        /// <summary>
        /// Redo�{�^��
        /// </summary>
        private CommandMenuButton _redoButton;

        /// <summary>
        /// �T�C�Y�ύX�{�^��
        /// </summary>
        private CommandMenuButtonFolder _penSizeButton;

        /// <summary>
        /// �R���X�g���N�^
        /// ���ڂ̏���
        /// </summary>
        public MainToolStrip()
        {
            _newButton = new CommandMenuButton("basic.file.newfile");
            _openButton = new CommandMenuButton("basic.file.openfile");
            _saveButton = new CommandMenuButton("basic.file.savefile");
            
            _undoButton = new CommandMenuButton("basic.edit.undo");
            _redoButton = new CommandMenuButton("basic.edit.redo");

            //���͎菑���ŃT�u���j���[�t���{�^�����쐬����B
            //��X�c�[���o�[�̃J�X�^�}�C�Y���\�ɂ��邽�߂ɂ́A
            //pensize�R�}���h����c�[���{�^���A���j���[���쐬��������
            //�����I�ɃT�u���j���[���쐬�����悤�Ȑ݌v�ɂ���K�v������B
            _penSizeButton = new CommandMenuButtonFolder("�y���T�C�Y");
            _penSizeButton.DropDownItems.Add(new CommandMenuItem("basic.tool.pensize1"));
            _penSizeButton.DropDownItems.Add(new CommandMenuItem("basic.tool.pensize2"));
            _penSizeButton.DropDownItems.Add(new CommandMenuItem("basic.tool.pensize3"));

        }


        /// <summary>
        /// ���C���c�[���X�g���b�v���쐬����
        /// </summary>
        public void buildMenuStrip()
        {
            Items.Add(_newButton);
            Items.Add(_openButton);
            Items.Add(_saveButton);
            Items.Add( new ToolStripSeparator() );
            Items.Add(_undoButton);
            Items.Add(_redoButton);
            Items.Add( new ToolStripSeparator() );
            Items.Add(_penSizeButton);
        }

    }
}
