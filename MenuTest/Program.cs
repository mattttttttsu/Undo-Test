using System;
using System.Collections.Generic;
using System.Text;
using MenuTest.Command;
using MenuTest.Menu;
using System.Windows.Forms;
using System.Drawing;

namespace MenuTest
{
    /// <summary>
    /// アプリケーションのスタートアップオブジェクトとなるクラス
    /// </summary>
    static class Program
    {
        /// <summary>
        /// エントリポイント
        /// </summary>
        [STAThread]
        static void Main()
        {
            init();
            MenuTest.Application app = MenuTest.Application.getInstance();
            
            Form1 mainFrame = (Form1)app.MainFrame;
            mainFrame.createToolStrip();

            System.Windows.Forms.Application.Run(app.MainFrame);
        }


        /// <summary>
        /// 初期化を実行する
        /// </summary>
        public static void init()
        {
            //今はここでコマンド用のアイコンリソースをロード
            ImageList imgList = UiResourceManager.Instance.ImageList;
            Bitmap imgToolBar = new Bitmap("toolbar.png");
            imgList.Images.AddStrip(imgToolBar);
            imgList.Images.SetKeyName(0, "basic.file.newfile");
            imgList.Images.SetKeyName(1, "basic.file.openfile");
            imgList.Images.SetKeyName(2, "basic.file.savefile");
            imgList.Images.SetKeyName(3, "basic.edit.undo");
            imgList.Images.SetKeyName(4, "basic.edit.redo");
            imgList.Images.SetKeyName(10, "basic.tool.pen");
            imgList.Images.SetKeyName(11, "basic.tool.line");

            //今はここで基本コマンドを初期化＆登録
            CommandManager cm = CommandManager.getInstance();

            cm.registerCommand(new NewFileCommand());
            cm.registerCommand(new OpenFileCommand());
            cm.registerCommand(new SaveFileCommand());
            cm.registerCommand(new ExitCommand());

            cm.registerCommand(new UndoCommand());
            cm.registerCommand(new RedoCommand());

            cm.registerCommand(new PenSizeChangeCommand(1));
            cm.registerCommand(new PenSizeChangeCommand(2));
            cm.registerCommand(new PenSizeChangeCommand(3));

            cm.registerCommand(new PenToolCommand());
            cm.registerCommand(new LineToolCommand());
        }
    }
}
