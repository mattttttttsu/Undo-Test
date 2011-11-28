using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MenuTest.Command;
using MenuTest.Menu;

namespace MenuTest
{
    /// <summary>
    /// ���C���t���[���ƂȂ�E�B���h�E
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// ���j���[�X�g���b�v
        /// </summary>
        private MainMenuStrip _mainMenuStrip;
        /// <summary>
        /// �c�[���X�g���b�v
        /// </summary>
        private MainToolStrip _mainToolStrip;

        /// <summary>
        /// �c�[���p���b�g
        /// </summary>
        private ToolPaletteStrip _toolPaletteStrip;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public Form1()
        {
            //VisualStudio���������������R�[�h�����s
            InitializeComponent();
        }


        /// <summary>
        /// �c�[���X�g���b�v�����������B
        /// </summary>
        public void createToolStrip()
        {
            //���̓E�B���h�E�ɃZ�b�g���郁�j���[�ƃc�[���o�[��
            //_mainMenuStrip�A_mainToolStrip�Ō��ߑł��B
            _mainMenuStrip = new MainMenuStrip();
            _mainMenuStrip.buildMenuStrip();
            _mainToolStrip = new MainToolStrip();
            _mainToolStrip.buildMenuStrip();
            _toolPaletteStrip = new ToolPaletteStrip();
            _toolPaletteStrip.buildMenuStrip();

            //this.Controls.Add(_mainToolStrip);
            this.Controls.Add(_mainMenuStrip);
            this.MainMenuStrip = _mainMenuStrip;
            this.ctrlTopToolStripPanel.Join(_mainToolStrip);
            this.ctrlLeftToolStripPanel.Join(_toolPaletteStrip);
        }


        /// <summary>
        /// �r���[��ǉ�����
        /// </summary>
        /// <param name="child">�q�E�B���h�E</param>
        public void addChildView(Form child)
        {
            child.MdiParent = this;
            child.Show();
        }
    }
}