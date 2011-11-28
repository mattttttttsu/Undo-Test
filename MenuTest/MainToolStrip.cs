using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MenuTest.Command;
using MenuTest.Menu;

namespace MenuTest
{
    /// <summary>
    /// ツールバーのストリップ
    /// </summary>
    class MainToolStrip : ToolStrip
    {
        /// <summary>
        /// 新規作成ボタン
        /// </summary>
        private CommandMenuButton _newButton;
        /// <summary>
        /// ファイルを開くボタン
        /// </summary>
        private CommandMenuButton _openButton;
        /// <summary>
        /// 保存ボタン
        /// </summary>
        private CommandMenuButton _saveButton;

        /// <summary>
        /// Undoボタン
        /// </summary>
        private CommandMenuButton _undoButton;

        /// <summary>
        /// Redoボタン
        /// </summary>
        private CommandMenuButton _redoButton;

        /// <summary>
        /// サイズ変更ボタン
        /// </summary>
        private CommandMenuButtonFolder _penSizeButton;

        /// <summary>
        /// コンストラクタ
        /// 項目の準備
        /// </summary>
        public MainToolStrip()
        {
            _newButton = new CommandMenuButton("basic.file.newfile");
            _openButton = new CommandMenuButton("basic.file.openfile");
            _saveButton = new CommandMenuButton("basic.file.savefile");
            
            _undoButton = new CommandMenuButton("basic.edit.undo");
            _redoButton = new CommandMenuButton("basic.edit.redo");

            //今は手書きでサブメニュー付きボタンを作成する。
            //後々ツールバーのカスタマイズを可能にするためには、
            //pensizeコマンドからツールボタン、メニューを作成した時に
            //自動的にサブメニューも作成されるような設計にする必要がある。
            _penSizeButton = new CommandMenuButtonFolder("ペンサイズ");
            _penSizeButton.DropDownItems.Add(new CommandMenuItem("basic.tool.pensize1"));
            _penSizeButton.DropDownItems.Add(new CommandMenuItem("basic.tool.pensize2"));
            _penSizeButton.DropDownItems.Add(new CommandMenuItem("basic.tool.pensize3"));

        }


        /// <summary>
        /// メインツールストリップを作成する
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
