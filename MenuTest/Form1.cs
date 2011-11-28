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
    /// メインフレームとなるウィンドウ
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// メニューストリップ
        /// </summary>
        private MainMenuStrip _mainMenuStrip;
        /// <summary>
        /// ツールストリップ
        /// </summary>
        private MainToolStrip _mainToolStrip;

        /// <summary>
        /// ツールパレット
        /// </summary>
        private ToolPaletteStrip _toolPaletteStrip;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form1()
        {
            //VisualStudioが書いた初期化コードを実行
            InitializeComponent();
        }


        /// <summary>
        /// ツールストリップ等を初期化。
        /// </summary>
        public void createToolStrip()
        {
            //今はウィンドウにセットするメニューとツールバーは
            //_mainMenuStrip、_mainToolStripで決め打ち。
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
        /// ビューを追加する
        /// </summary>
        /// <param name="child">子ウィンドウ</param>
        public void addChildView(Form child)
        {
            child.MdiParent = this;
            child.Show();
        }
    }
}