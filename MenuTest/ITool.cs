using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MenuTest
{
    /// <summary>
    /// ツールのインターフェース
    /// </summary>
    public interface ITool
    {
        /// <summary>
        /// ツールIDを返すプロパティ
        /// </summary>
        Int32 ToolId
        { get; }


        /// <summary>
        /// アクティブになった時の処理
        /// </summary>
        void onActivate();

        /// <summary>
        /// アクティブではなくなった時の処理
        /// </summary>
        void onDeactivate();
        
        /// <summary>
        /// マウスのクリックの処理
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">マウスイベント</param>
        void onMouseDown(Object sender, MouseEventArgs e);

        /// <summary>
        /// マウスのボタンを離した瞬間の処理
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">マウスイベント</param>
        void onMouseUp(Object sender, MouseEventArgs e);

        /// <summary>
        /// マウスの移動の処理
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">マウスイベントイベント</param>
        void onMouseMove(Object sender, MouseEventArgs e);

        /// <summary>
        /// キャンバスを描画する時の処理
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">ペイントイベント</param>
        void onPaint(Object sender, PaintEventArgs e);
    }
}
