using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MenuTest
{
    /// <summary>
    /// MDIの子ウィンドウ。
    /// ドキュメントのビュー。
    /// </summary>
    public partial class ChildForm : Form
    {
        /// <summary>
        /// このビューが参照するドキュメント
        /// </summary>
        private Document _doc;

        /// <summary>
        /// キャンバス（ペン等で描画する領域）
        /// </summary>
        private Canvas _ctrlCanvas;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="doc">ドキュメント</param>
        public ChildForm(Document doc)
        {
            _doc = doc;

            //Visual Studioのフォームデザイナが書いた
            //UI初期化コードの実行
            InitializeComponent();

            //ウィンドウタイトルの設定。
            //ファイル名が決定していればファイル名。
            //決定していなければドキュメント+ID
            if(_doc.FileName != "") {
                this.Text = _doc.FileName;
            } else {
                this.Text = String.Format("ドキュメント{0}", _doc.ID);
            }

            //ドキュメントの変更を監視する。
            _doc.DocumentStateChangeEvent += onDocumentChange;

            //キャンバスの配置
            _ctrlCanvas = new Canvas(_doc);
            ((System.ComponentModel.ISupportInitialize)_ctrlCanvas).BeginInit();
            this.Controls.Add(_ctrlCanvas);

            _ctrlCanvas.Dock = DockStyle.Fill;
            _ctrlCanvas.Width = 100;
            _ctrlCanvas.Height = 200;
            _ctrlCanvas.BorderStyle = BorderStyle.Fixed3D;
            ((System.ComponentModel.ISupportInitialize)_ctrlCanvas).EndInit();
            _ctrlCanvas.Show();
        }


        /// <summary>
        /// フォームがアクティブになった場合。
        /// 自分自身のドキュメントをアクティブにする。
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">イベント</param>
        private void MapForm_Activated(object sender, EventArgs e)
        {
            MenuTest.Application app = MenuTest.Application.getInstance();

            //Application側でonActiveDocumentChangeイベントが発生する
            app.ActiveDocument = _doc;
        }
        

        /// <summary>
        /// フォームがアクティブではなくなった場合
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">イベント</param>
        private void MapForm_Deactivate(object sender, EventArgs e)
        {
            MenuTest.Application app = MenuTest.Application.getInstance();
            if(!app.isActiveDocument(_doc)) {
                return;
            }
            
            //Application側でonActiveDocumentChangeイベントが発生する
            //複数の子ウィンドウが存在する場合は他のウィンドウが
            //すぐにアクティブになるのでnullに設定する。
            app.ActiveDocument = null;
        }


        /// <summary>
        /// ドキュメントの状態が変化すると呼び出される
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">イベント</param>
        private void onDocumentChange(object sender, EventArgs e)
        {
            //今はドキュメントにどんな小さな変更があっても通知される。
            //扱っているデータの数や量が少ない場合はこれで大丈夫だけど
            //データ量が増えたり、規模が大きくなってきた場合は
            //イベントを分けるか、EventArgsで判断するか、
            //オリジナルのパラメータを持った新しいイベントを定義する。

            ctrlPenSizeLabel.Text = "ペンサイズ：" + _doc.PenSize.ToString();
        }
    }
}