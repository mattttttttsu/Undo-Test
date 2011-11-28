using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using MenuTest.Command;
using MenuTest.Menu;

namespace MenuTest
{
    /// <summary>
    /// メインメニューの作成を行うクラス
    /// </summary>
    class MainMenuStrip : MenuStrip
    {
        /// <summary>
        /// 「ファイル」メニュー
        /// </summary>
        private CommandMenuItemFolder _fileMenu;

        /// <summary>
        /// 編集メニュー
        /// </summary>
        private CommandMenuItemFolder _editMenu;

        /// <summary>
        /// ツールメニュー
        /// </summary>
        private CommandMenuItemFolder _toolMenu;

        /// <summary>
        /// コンストラクタ
        /// メインメニュー項目の準備
        /// </summary>
        public MainMenuStrip()
        {
            _fileMenu = new CommandMenuItemFolder("ファイル");
            _fileMenu.DropDownItems.Add(new CommandMenuItem("basic.file.newfile"));
            _fileMenu.DropDownItems.Add(new CommandMenuItem("basic.file.openfile"));
            _fileMenu.DropDownItems.Add(new CommandMenuItem("basic.file.savefile"));
            _fileMenu.DropDownItems.Add(new System.Windows.Forms.ToolStripSeparator());
            _fileMenu.DropDownItems.Add(new CommandMenuItem("basic.file.exit"));

            _editMenu = new CommandMenuItemFolder("編集");
            _editMenu.DropDownItems.Add(new CommandMenuItem("basic.edit.undo"));
            _editMenu.DropDownItems.Add(new CommandMenuItem("basic.edit.redo"));

            _toolMenu = new CommandMenuItemFolder("ツール");
            _toolMenu.DropDownItems.Add(new CommandMenuItem("basic.tool.pensize1"));
            _toolMenu.DropDownItems.Add(new CommandMenuItem("basic.tool.pensize2"));
            _toolMenu.DropDownItems.Add(new CommandMenuItem("basic.tool.pensize3"));
        }


        /// <summary>
        /// メインメニューストリップを作成する
        /// </summary>
        public void buildMenuStrip()
        {
            Items.Add(_fileMenu);
            Items.Add(_editMenu);
            Items.Add(_toolMenu);
        }

    }
}
