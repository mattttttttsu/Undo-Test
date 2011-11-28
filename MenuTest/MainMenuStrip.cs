using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using MenuTest.Command;
using MenuTest.Menu;

namespace MenuTest
{
    /// <summary>
    /// ���C�����j���[�̍쐬���s���N���X
    /// </summary>
    class MainMenuStrip : MenuStrip
    {
        /// <summary>
        /// �u�t�@�C���v���j���[
        /// </summary>
        private CommandMenuItemFolder _fileMenu;

        /// <summary>
        /// �ҏW���j���[
        /// </summary>
        private CommandMenuItemFolder _editMenu;

        /// <summary>
        /// �c�[�����j���[
        /// </summary>
        private CommandMenuItemFolder _toolMenu;

        /// <summary>
        /// �R���X�g���N�^
        /// ���C�����j���[���ڂ̏���
        /// </summary>
        public MainMenuStrip()
        {
            _fileMenu = new CommandMenuItemFolder("�t�@�C��");
            _fileMenu.DropDownItems.Add(new CommandMenuItem("basic.file.newfile"));
            _fileMenu.DropDownItems.Add(new CommandMenuItem("basic.file.openfile"));
            _fileMenu.DropDownItems.Add(new CommandMenuItem("basic.file.savefile"));
            _fileMenu.DropDownItems.Add(new System.Windows.Forms.ToolStripSeparator());
            _fileMenu.DropDownItems.Add(new CommandMenuItem("basic.file.exit"));

            _editMenu = new CommandMenuItemFolder("�ҏW");
            _editMenu.DropDownItems.Add(new CommandMenuItem("basic.edit.undo"));
            _editMenu.DropDownItems.Add(new CommandMenuItem("basic.edit.redo"));

            _toolMenu = new CommandMenuItemFolder("�c�[��");
            _toolMenu.DropDownItems.Add(new CommandMenuItem("basic.tool.pensize1"));
            _toolMenu.DropDownItems.Add(new CommandMenuItem("basic.tool.pensize2"));
            _toolMenu.DropDownItems.Add(new CommandMenuItem("basic.tool.pensize3"));
        }


        /// <summary>
        /// ���C�����j���[�X�g���b�v���쐬����
        /// </summary>
        public void buildMenuStrip()
        {
            Items.Add(_fileMenu);
            Items.Add(_editMenu);
            Items.Add(_toolMenu);
        }

    }
}
