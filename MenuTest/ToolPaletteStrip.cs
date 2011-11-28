using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MenuTest.Command;
using MenuTest.Menu;


namespace MenuTest
{
    /// <summary>
    /// �c�[���p���b�g�̃N���X
    /// </summary>
    class ToolPaletteStrip : ToolStrip
    {
        /// <summary>
        /// �y���c�[��
        /// </summary>
        private CommandMenuButton _penButton;
        /// <summary>
        /// �����c�[��
        /// </summary>
        private CommandMenuButton _lineButton;

        /// <summary>
        /// �R���X�g���N�^
        /// ���ڂ̏���
        /// </summary>
        public ToolPaletteStrip()
        {
            _penButton = new CommandMenuButton("basic.tool.pen");
            _lineButton = new CommandMenuButton("basic.tool.line");
        }


        /// <summary>
        /// ���C���c�[���X�g���b�v���쐬����
        /// </summary>
        public void buildMenuStrip()
        {
            Items.Add(_penButton);
            Items.Add(_lineButton);
        }
    }
}
