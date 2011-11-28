using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MenuTest.Command;
using MenuTest.Menu;

namespace MenuTest
{
    /// <summary>
    /// アプリケーションクラス
    /// </summary>
    public class Application
    {
        /// <summary>
        /// 自分自身のインスタンス
        /// </summary>
        private static Application _instance;

        /// <summary>
        /// ドキュメントのリスト
        /// </summary>
        private List<Document> _documents;


        /// <summary>
        /// アクティブなドキュメント
        /// </summary>
        private Document _activeDocument;

        /// <summary>
        /// ドキュメントのIDを管理するシーケンス
        /// </summary>
        private Int32 _docSeq;

        /// <summary>
        ///アプリケーションのメインウィンドウ。
        ///MDIの親ウィンドウとして利用する
        /// </summary>
        private Form1 _mainFrame;

        /// <summary>
        ///アクティブなツール
        /// </summary>
        private ITool _tool;

        /// <summary>
        ///アクティブなドキュメントが変化した時に呼び出される
        /// </summary>
        public event EventHandler ActiveDocumentChange;

        /// <summary>
        /// コンストラクタ
        /// 直接newできないようにprivateで定義。
        /// </summary>
        private Application()
        {
            _documents = new List<Document>();
            _activeDocument = null;

            //Applicationクラスの中でメインウィンドウを初期化する
            _mainFrame = new Form1();

            _tool = new PenTool();

            _docSeq = 1;
        }


        /// <summary>
        /// Applicationクラスのインスタンスを返す
        /// アプリケーションクラスは常にこのメソッドで取得する
        /// </summary>
        /// <returns>アプリケーションクラスのインスタンス</returns>
        public static Application getInstance()
        {
            if(_instance == null)
            {
                _instance = new Application();
            }
            return _instance;
        }


        /// <summary>
        /// ドキュメントの新規作成を行う
        /// </summary>
        public void newDocument()
        {
            //新しいIDのドキュメントを作成し、
            //子フォームを作成。
            Document doc = new Document(_docSeq++);
            _documents.Add(doc);

            ChildForm newForm = new ChildForm(doc);
            _mainFrame.addChildView(newForm);
        }


        /// <summary>
        /// 指定したファイルを読み込んでドキュメントを作成する
        /// </summary>
        /// <param name="fileName">開くファイルの名前</param>
        public void openDocument(string fileName)
        {
            //未実装
            MessageBox.Show("ファイル開く");
        }


        /// <summary>
        /// アクティブなドキュメントの取得・設定を行うプロパティ
        /// </summary>
        public Document ActiveDocument
        {
            get { return _activeDocument; }
            set
            {
                _activeDocument = value;
                //アクティブなドキュメントが変化したので
                //イベントを起動する
                notifyActiveDocumentChange();
            }
        }


        /// <summary>
        /// アクティブなドキュメントかどうかを返す
        /// </summary>
        /// <param name="doc">判定するドキュメント</param>
        /// <returns>
        /// true  => アクティブ
        /// false => アクティブではない
        /// </returns>
        public Boolean isActiveDocument(Document doc)
        {
            if(_activeDocument == null) {
                return false;
            }
            return _activeDocument.Equals(doc);
        }


        /// <summary>
        /// メインウィンドウを返すプロパティ
        /// </summary>
        public System.Windows.Forms.Form MainFrame
        { get { return _mainFrame; } }


        /// <summary>
        /// アクティブなドキュメントに変化があったことを
        /// 通知するイベントを起動する。
        ///
        /// Applicationクラス以外のクラス(Documentクラスなど)からは
        /// ActiveDocumentChangeイベントが発生させられないので、
        /// 代わりにこのメソッドを呼び出すようにする。
        /// </summary>
        public void notifyActiveDocumentChange()
        {
            ActiveDocumentChange(this, null);
        }


        /// <summary>
        /// ツールを設定する。
        /// 今はツールIDはただの数字だが、これを
        /// 定数等にする必要がある。セットするツールも毎回newしない。
        /// </summary>
        /// <param name="toolId">ツールID</param>
        public void setTool(Int32 toolId)
        {
            if(_tool != null && _tool.ToolId == toolId) {
                return;
            }

            _tool.onDeactivate();

            switch(toolId)
            {
            case 1: _tool = new PenTool();  break;
            case 2: _tool = new LineTool(); break;
            }

            _tool.onActivate();

            //今はこれでツールバーに変更を通知するしかない。
            //別な手段を用意する必要がある。
            notifyActiveDocumentChange();
        }


        /// <summary>
        /// 現在選択中のツールを返す。
        /// 後で読み込み専用のプロパティに変更する
        /// </summary>
        public ITool Tool
        { get { return _tool; } }
    }
}
