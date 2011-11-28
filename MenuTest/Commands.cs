using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MenuTest.Command;

namespace MenuTest
{
    /// <summary>
    /// 新規作成コマンド
    /// </summary>
    public class NewFileCommand : BaseCommand
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NewFileCommand()
        {
            _commandId = "basic.file.newfile";
            _text = "新規作成";
            _desc = "ドキュメントを新規作成します。";
        }

        
        /// <summary>
        /// ドキュメントを新規に作成する。
        /// </summary>
        public override void execute()
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.newDocument();
        }
    }


    /// <summary>
    /// ファイルを開くコマンド
    /// </summary>
    public class OpenFileCommand : BaseCommand
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OpenFileCommand()
        {
            _commandId = "basic.file.openfile";
            _text = "開く";
            _desc = "ファイルを開きます。";
        }

        /// <summary>
        /// 適当なファイルを開く
        /// </summary>
        public override void execute()
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.openDocument("aaa.txt");
        }

    }


    /// <summary>
    /// 保存コマンド
    /// </summary>
    public class SaveFileCommand : BaseCommand
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SaveFileCommand()
        {
            _commandId = "basic.file.savefile";
            _text = "保存";
            _desc = "ドキュメントを保存します。";

            //アクティブなドキュメントの状態を監視する
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.ActiveDocumentChange += onActiveDocumentChange;
        }


        /// <summary>
        /// ドアクティブなキュメントのdirtyフラグをfalseに設定する。
        /// </summary>
        public override void execute()
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.ActiveDocument.setDirty(false);
        }


        /// <summary>
        /// 有効状態を返す
        /// </summary>
        /// <returns>コマンドが有効かどうか</returns>
        public override Boolean isEnabled()
        {
            //アクティブなドキュメントが存在し、
            //dirtyフラグが立っている事が条件
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.ActiveDocument != null && app.ActiveDocument.Dirty);
        }


        /// <summary>
        /// アクティブなドキュメントに変化があった時に呼び出される
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">イベント</param>

        public void onActiveDocumentChange(object sender, EventArgs e)
        {
            notifyStateChange();
        }
    }


    /// <summary>
    /// 終了コマンド
    /// </summary>
    public class ExitCommand : BaseCommand
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ExitCommand()
        {
            _commandId = "basic.file.exit";
            _text = "終了";
            _desc = "プログラムを終了します。";
        }

        /// <summary>
        /// アプリケーションを終了する。
        /// </summary>
        public override void execute()
        {
            System.Windows.Forms.Application.Exit();
        }
    }


    /// <summary>
    /// ペンサイズを変更するコマンド
    /// </summary>
    public class PenSizeChangeCommand : BaseCommand
    {
        /// <summary>
        /// 変更後のペンのサイズ
        /// </summary>
        private Int32 _size;

        /// <summary>
        /// コンストラクタ
        /// パラメータでペンサイズを指定する。
        /// </summary>
        /// <param name="size">ペンサイズ</param>
        public PenSizeChangeCommand(Int32 size)
        {
            _size = size;
            _commandId = String.Format("basic.tool.pensize{0}", _size);
            _text = String.Format("ペンサイズ{0}", _size);
            _desc = String.Format("ペンサイズを{0}に変更", _size);
        }


        /// <summary>
        /// アクティブなドキュメントのペンサイズを変更する。
        /// </summary>
        public override void execute()
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            Document activeDoc = app.ActiveDocument;

            activeDoc.setPenSize(_size);
        }


        /// <summary>
        /// 有効状態を返す
        /// </summary>
        /// <returns>コマンドの有効状態</returns>
        public override bool isEnabled()
        {
            //アクティブなドキュメントが存在する事が条件
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.ActiveDocument != null);
        }


        /// <summary>
        /// チェックされているかを返す
        /// </summary>
        /// <returns>チェックされているかどうか</returns>
        public override bool isChecked()
        {
            //アクティブなドキュメントのペンサイズを
            //チェックする。自分自身の値と一致すれば
            //チェックされているとみなす。
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.ActiveDocument != null && app.ActiveDocument.PenSize == _size);
        }
    }


    /// <summary>
    /// Undoコマンド
    /// </summary>
    public class UndoCommand : BaseCommand
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UndoCommand()
        {
            _commandId = "basic.edit.undo";
            _text = "Undo";
            _desc = "アンドゥを実行します。";

            //アクティブなドキュメントの状態を監視する
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.ActiveDocumentChange += onActiveDocumentChange;
        }


        /// <summary>
        /// ドアクティブなキュメントのdirtyフラグをfalseに設定する。
        /// </summary>
        public override void execute()
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.ActiveDocument.HistoryList.undo();
        }


        /// <summary>
        /// 有効状態を返す
        /// </summary>
        /// <returns>コマンドの有効状態</returns>
        public override Boolean isEnabled()
        {
            //アクティブなドキュメントが存在し、
            //dirtyフラグが立っている事が条件
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.ActiveDocument != null && app.ActiveDocument.HistoryList.canUndo());
        }


        /// <summary>
        /// アクティブなドキュメントに変化があった時に呼び出される
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">イベント</param>
        public void onActiveDocumentChange(object sender, EventArgs e)
        {
            notifyStateChange();
        }
    }



    /// <summary>
    /// Redoコマンド
    /// </summary>
    public class RedoCommand : BaseCommand
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RedoCommand()
        {
            _commandId = "basic.edit.redo";
            _text = "Redo";
            _desc = "リドゥを実行します。";

            //アクティブなドキュメントの状態を監視する
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.ActiveDocumentChange += onActiveDocumentChange;
        }


        /// <summary>
        /// ドアクティブなキュメントのdirtyフラグをfalseに設定する。
        /// </summary>
        public override void execute()
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.ActiveDocument.HistoryList.redo();
        }


        /// <summary>
        /// 有効状態を返す
        /// </summary>
        /// <returns>有効状態</returns>
        public override Boolean isEnabled()
        {
            //アクティブなドキュメントが存在し、
            //dirtyフラグが立っている事が条件
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.ActiveDocument != null && app.ActiveDocument.HistoryList.canRedo());
        }


        /// <summary>
        /// アクティブなドキュメントに変化があった時に呼び出される
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">イベント</param>
        public void onActiveDocumentChange(object sender, EventArgs e)
        {
            notifyStateChange();
        }
    }


    /// <summary>
    /// ペンツール選択コマンド
    /// </summary>
    public class PenToolCommand : BaseCommand
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PenToolCommand()
        {
            _commandId = "basic.tool.pen";
            _text = "pen";
            _desc = "ペンツール";

            //アクティブなドキュメントの状態を監視する
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.ActiveDocumentChange += onActiveDocumentChange;
        }


        /// <summary>
        /// ペンツールを選択する
        /// </summary>
        public override void execute()
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.setTool(1);
        }


        /// <summary>
        /// 有効状態を返す
        /// </summary>
        /// <returns>コマンドの有効状態</returns>
        public override Boolean isEnabled()
        {
            //アクティブなドキュメントが存在すれば選択可能
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.ActiveDocument != null);
        }


        /// <summary>
        /// チェック状態を返す
        /// </summary>
        /// <returns>コマンドのチェックの状態</returns>
        public override bool isChecked()
        {
            //アクティブなドキュメントが存在すれば選択可能
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.Tool.ToolId == 1);
        }


        /// <summary>
        /// アクティブなドキュメントに変化があった時に呼び出される
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">イベント</param>
        public void onActiveDocumentChange(object sender, EventArgs e)
        {
            notifyStateChange();
        }
    }


    /// <summary>
    /// 直線ツール選択コマンド
    /// </summary>
    public class LineToolCommand : BaseCommand
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LineToolCommand()
        {
            _commandId = "basic.tool.line";
            _text = "line";
            _desc = "直線ツール";

            //アクティブなドキュメントの状態を監視する
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.ActiveDocumentChange += onActiveDocumentChange;
        }


        /// <summary>
        /// 直線ツールを選択する
        /// </summary>
        public override void execute()
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            app.setTool(2);
        }


        /// <summary>
        /// 有効状態を返す
        /// </summary>
        /// <returns>コマンドの有効状態</returns>
        public override Boolean isEnabled()
        {
            //アクティブなドキュメントが存在すれば選択可能
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.ActiveDocument != null);
        }

        
        /// <summary>
        /// チェック状態を返す
        /// </summary>
        /// <returns>コマンドのチェックの状態</returns>
        public override bool isChecked()
        {
            //アクティブなドキュメントが存在すれば選択可能
            MenuTest.Application app = MenuTest.Application.getInstance();
            return (app.Tool.ToolId == 2);
        }


        /// <summary>
        /// アクティブなドキュメントに変化があった時に呼び出される
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">イベント</param>
        public void onActiveDocumentChange(object sender, EventArgs e)
        {
            notifyStateChange();
        }
    }

}
