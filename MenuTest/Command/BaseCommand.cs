using System;
using System.Collections.Generic;
using System.Text;

namespace MenuTest.Command
{
    /// <summary>
    /// コマンドのベースとなるクラス
    /// ICommandインターフェースを持つクラスは
    /// 結局ほとんどがこの実装をすることになるので、
    /// それを簡略化するために用意する。
    /// </summary>
    public class BaseCommand : ICommand
    {
        /// <summary>
        /// コマンドID
        /// </summary>
        protected String _commandId;
        /// <summary>
        /// コマンドの表示名
        /// </summary>
        protected String _text;
        /// <summary>
        /// コマンドの説明文
        /// </summary>
        protected String _desc;
        /// <summary>
        /// 画像ID
        /// </summary>
        protected String _imageId;

        /// <summary>
        /// コマンドの状態が変化した時に呼び出されるイベント
        /// </summary>
        public event EventHandler CommandStateChangeEvent;

        /// <summary>
        /// コマンドIDを返すプロパティ
        /// </summary>
        public String CommandId
        { get { return _commandId; } }

        /// <summary>
        /// コマンドの表示名を返すプロパティ
        /// </summary>
        public String Text
        { get { return _text; } }

        /// <summary>
        /// コマンドの詳細文を返すプロパティ
        /// </summary>
        public String Desc
        { get { return _desc; } }

        /// <summary>
        /// コマンドの画像IDを返すプロパティ
        /// 今はコマンドIDをそのまま返す
        /// </summary>
        public String ImageId
        { get { return _commandId; } }


        /// <summary>
        /// メニューの有効状態を返す
        /// </summary>
        /// <returns>メニューの有効状態</returns>
        public virtual Boolean isEnabled()
        { return true; }

        /// <summary>
        /// メニューがチェックされているかを返す
        /// </summary>
        /// <returns>メニューがチェックされているか</returns>
        public virtual Boolean isChecked()
        { return false; }

        /// <summary>
        /// コマンドを実行する
        /// </summary>
        public virtual void execute()
        { return; }


        /// <summary>
        /// コマンドの状態に変化があったことを通知する
        /// サブクラスからはCommandStateChangeEventは
        /// 直接呼び出せない(？)ようなのでメソッドとして用意。
        /// </summary>
        public void notifyStateChange()
        {
            if(CommandStateChangeEvent != null)
            {
                CommandStateChangeEvent(this, null);
            }
        }
    }
}
