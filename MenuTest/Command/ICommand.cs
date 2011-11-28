using System;
using System.Collections.Generic;
using System.Text;

namespace MenuTest.Command
{
    /// <summary>
    /// コマンドのインターフェース
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// コマンドID
        /// </summary>
        String CommandId
        { get; }
        /// <summary>
        /// コマンドの表示名(名前？)
        /// </summary>
        String Text
        { get; }
        /// <summary>
        /// コマンドの詳細文
        /// </summary>
        String Desc
        { get; }
        /// <summary>
        /// 画像ID
        /// </summary>
        String ImageId
        { get; }

        /// <summary>
        /// 有効状態を返す
        /// </summary>
        /// <returns>有効状態</returns>
        Boolean isEnabled();
        /// <summary>
        /// チェックされているかを返す
        /// </summary>
        /// <returns>チェックされているか</returns>
        Boolean isChecked();


        /// <summary>
        /// コマンドを実行する。
        /// </summary>
        void execute();

        /// <summary>
        /// コマンドの状態が変化した時に発生するイベント
        /// </summary>
        event EventHandler CommandStateChangeEvent;
    }
}
