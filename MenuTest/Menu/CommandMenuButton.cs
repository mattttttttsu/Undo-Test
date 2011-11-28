using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MenuTest.Command;

namespace MenuTest.Menu
{
    /// <summary>
    /// コマンドを割り当てたツールバーのボタンのクラス
    /// </summary>
    class CommandMenuButton : ToolStripButton
    {
        /// <summary>
        /// ボタンに関連付けられているコマンド
        /// </summary>
        internal ICommand _command;

        /// <summary>
        /// コマンド
        /// </summary>
        /// <param name="commandId">コマンドID</param>
        public CommandMenuButton(String commandId)
        {
            //コマンドマネージャからIDに対応するコマンドを取得
            CommandManager cm = CommandManager.getInstance();
            _command = cm.findCommand(commandId);
            Text = _command.Text;
            ToolTipText = _command.Desc;
            DisplayStyle = ToolStripItemDisplayStyle.Image;

            //画像が存在すれば画像を登録する
            ImageList imgList = UiResourceManager.Instance.ImageList;
            if(imgList.Images.ContainsKey(_command.ImageId))
            {
                Image = imgList.Images[_command.ImageId];
            }

            //クリック時のイベントには自分自身のonClickを追加
            Click += onClick;

            //コマンドの状態変化を監視する
            _command.CommandStateChangeEvent += onCommandStateChange;

            //作成直後、自分自身をアップデートする。
            //こうしないと、いきなり無効・チェック済み状態に対応できない
            update();
        }


        /// <summary>
        /// アイテムをクリックした時のデフォルトの動作
        /// アイテムによって動作を上書きする可能性があるので
        /// staticにはしない。
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">イベント</param>
        public void onClick(object sender, EventArgs e)
        {
            _command.execute();
        }


        /// <summary>
        /// アイテムがポップアップされた時のデフォルトの動作
        /// アイテムによって動作を上書きする可能性があるので
        /// staticにはしない。
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">イベント</param>
        public void onPopup(object sender, EventArgs e)
        {
            return;
        }


        /// <summary>
        /// 自分自身の表示を更新する
        /// 今は主に、コマンドに変化があった時に
        /// 呼び出されている。
        /// </summary>
        public void update()
        {
            if(_command == null) {
                return;
            }
            //コマンドの現在状態に従う
            Enabled = _command.isEnabled();
            Checked = _command.isChecked();
        }


        /// <summary>
        /// コマンドの状態に変化があった場合に呼び出される
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">イベント</param>
        public void onCommandStateChange(object sender, EventArgs e)
        {
            update();
        }
    }
}
