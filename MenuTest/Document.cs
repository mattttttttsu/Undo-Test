using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace MenuTest
{
    /// <summary>
    /// ドキュメントのクラス
    /// </summary>
    public class Document
    {
        /// <summary>
        /// ドキュメントID
        /// </summary>
        private Int32 _id;

        /// <summary>
        /// ドキュメントに変更があったかどうか
        /// </summary>
        private bool _dirty;

        /// <summary>
        /// ドキュメントのファイル名
        /// 新規作成時は空
        /// </summary>
        private String _fileName;

        /// <summary>
        /// 作業用サーフェイス
        /// </summary>
        private Surface _surface;

        /// <summary>
        /// バックアップ用サーフェイス
        /// アンドゥを可能にするため、直前の編集の
        /// サーフェイスの内容を保持する。
        /// </summary>
        private Surface _backSurface;

        /// <summary>
        /// アンドゥ可能なアクションのリスト
        /// </summary>
        private HistoryActionList _historyList;

        /// <summary>
        /// ペンサイズ
        /// </summary>
        private Int32 _penSize;

        /// <summary>
        /// ドキュメントの状態に変更があった場合に呼び出されるイベント
        /// </summary>
        public event EventHandler DocumentStateChangeEvent;

        /// <summary>
        /// サーフェイスの状態に変更があったことを知るためのデリゲート
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="rect">変更のあった領域</param>
        public delegate void SurfaceChangeEventHandler(Object sender, Rectangle rect);

        /// <summary>
        /// サーフェイスに変更が起きた時に発生するイベント
        /// </summary>
        public event SurfaceChangeEventHandler SurfaceChangeEvent;

        

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id">ドキュメントID</param>
        public Document(Int32 id)
        {
            _id = id;
            _penSize = 1;
            _fileName = "";
            _dirty = false;

            _surface = new Surface(50, 50);
            _backSurface = _surface.Clone() as Surface;
            _historyList = new HistoryActionList();
        }

        /// <summary>
        /// IDを返すプロパティ
        /// </summary>
        public Int32 ID
        { get { return _id; } }


        /// <summary>
        /// ファイル名のプロパティ
        /// </summary>
        public String FileName
        { get { return _fileName; } }


        /// <summary>
        /// ペンサイズのプロパティ
        /// </summary>
        public Int32 PenSize
        { get { return _penSize; } }


        /// <summary>
        /// ドキュメントが変更されたかどうかのフラグ
        /// </summary>
        public Boolean Dirty
        { get { return _dirty; } }


        /// <summary>
        /// サーフェイスを返すプロパティ
        /// </summary>
        public Surface Surface
        { get { return _surface; } }


        /// <summary>
        /// バックサーフェイスを返す
        /// </summary>
        public Surface BackSurface
        { get { return _backSurface; } }


        /// <summary>
        /// アンドゥ可能なアクションのリストを返す
        /// </summary>
        public HistoryActionList HistoryList
        { get { return _historyList; } }


        /// <summary>
        /// ドキュメントのファイル名をセットする
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        public void setFileName(String fileName)
        {
            _fileName = fileName;
            update();
        }


        /// <summary>
        /// ペンサイズをセットする
        /// </summary>
        /// <param name="size">新しいペンサイズ</param>
        public void setPenSize(Int32 size)
        {
            _penSize = size;
            update();
        }


        /// <summary>
        /// ドキュメントの変更フラグを直接変更する
        /// </summary>
        /// <param name="newVal">新しい状態</param>
        public void setDirty(Boolean newVal)
        {
            _dirty = newVal;
            update();
        }


        /// <summary>
        /// サーフェイスに腺を描く
        /// </summary>
        /// <param name="start">始点</param>
        /// <param name="end">終点</param>
        public void putLine(Point start, Point end)
        {
            _surface.putLine(start, end, _penSize, 1);
            
            //サーフェイスを監視しているオブジェクトに通知する
            if(SurfaceChangeEvent != null)
            {
                Int32 x = 0,
                      y = 0,
                      w = _surface.W,
                      h = _surface.H;

                Rectangle rect = new Rectangle(x, y, w, h);
                SurfaceChangeEvent(this, rect);
            }
            
        }


        /// <summary>
        /// サーフェイスに対して連続した編集を行う時に
        /// 呼び出すメソッド。
        /// </summary>
        public void beginEdit()
        {
            //今は何もしない。
            //後々、endEditが呼び出されるまでの間に行われた
            //編集をJoinedHistoryActionとかそういう名前のオブジェクトに
            //溜め込むような仕様に変更する必要がある。
            return;
        }


        /// <summary>
        /// アンドゥ可能なアクションを履歴リストに追加する
        /// </summary>
        /// <param name="action">アクションオブジェクト</param>
        public void addEdit(IHistoryAction action)
        {
            //後々、beginEdit()が呼ばれている間はjoinedHistoryActionに
            //履歴が蓄積されるように変更する。
            _historyList.add(action);
        }


        /// <summary>
        /// ツールでの編集が終わった時に呼び出される関数。
        /// サーフェイスの内容をバックサーフェイスにコピーする。
        /// 今の仕様では、サーフェイスへの編集が終った後、
        /// beginEdit()を呼び出していなくても必ず呼ぶ必要がある。
        /// </summary>
        public void endEdit()
        {
            //後々、joinedHistoryActionがある場合はそれを
            //undoHistoryActionに追加する処理を実行する。
            //履歴が蓄積されるように変更する。

            //サーフェイスの内容をバックサーフェイス(undo用サーフェイス)にコピーする。
            //複数レイヤーのアプリの場合はどうするか考える必要がある。
            //マップエディタのように様々な種類のオブジェクトを配置する場合も含め、
            //このコードはここに置いておけない。
            _backSurface.copy(0, 0, _surface, 0, 0, _surface.W, _surface.H);
            Rectangle rc = new Rectangle(0, 0, _surface.W, _surface.H);
            SurfaceChangeEvent(this, rc);


            update();
        }


        /// <summary>
        /// ドキュメントの内容が変更されたのでイベントを発生させる。
        /// </summary>
        private void update()
        {
            //ドキュメントの状態が変更されたことを通知する。
            DocumentStateChangeEvent(this, null);

            //今自分自身がアクティブなら、
            //アクティブドキュメントの変更イベントを発生させる。
            //ツールバーが、アクティブなドキュメントの変化を追跡できないので
            //ここでこういう処理が必要になっているけど、本来は不要な処理。
            MenuTest.Application app = MenuTest.Application.getInstance();
            if(app.ActiveDocument == this) {
                app.notifyActiveDocumentChange();
            }
        }


        /// <summary>
        /// サーフェイスを更新したことをビューに通知する
        /// ツールからキャンバスを再描画する手段がないため
        /// やむを得ず追加。
        /// @param rc 再描画が必要な領域(未実装)
        /// </summary>
        /// <param name="rc"></param>
        public void updateSurface(Rectangle rc)
        {
            SurfaceChangeEvent(this, rc);
        }
    }
}
