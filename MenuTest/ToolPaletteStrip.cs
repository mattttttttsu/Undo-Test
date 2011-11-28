using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MenuTest.Command;
using MenuTest.Menu;


namespace MenuTest
{
    /// <summary>
    /// ツールパレットのクラス
    /// </summary>
    class ToolPaletteStrip : ToolStrip
    {
        /// <summary>
        /// ペンツール
        /// </summary>
        private CommandMenuButton _penButton;
        /// <summary>
        /// 直線ツール
        /// </summary>
        private CommandMenuButton _lineButton;

        /// <summary>
        /// コンストラクタ
        /// 項目の準備
        /// </summary>
        public ToolPaletteStrip()
        {
            _penButton = new CommandMenuButton("basic.tool.pen");
            _lineButton = new CommandMenuButton("basic.tool.line");
        }


        /// <summary>
        /// メインツールストリップを作成する
        /// </summary>
        public void buildMenuStrip()
        {
            Items.Add(_penButton);
            Items.Add(_lineButton);
        }
    }
}
