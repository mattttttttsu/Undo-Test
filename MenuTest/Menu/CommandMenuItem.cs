using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MenuTest.Command;

namespace MenuTest.Menu
{
    /// <summary>
    /// コマンドを割り当てたメニューアイテムのクラス
    /// </summary>
    public class CommandMenuItem : ToolStripMenuItem
    {
        /// <summary>
        /// アイテムに割り当てるコマンド
        /// </summary>
        internal ICommand _command;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="commandId">アイテムに割り当てるコマンド</param>
        public CommandMenuItem(String commandId)
        {
            CommandManager cm = CommandManager.getInstance();
            _command = cm.findCommand(commandId);

            Text = _command.Text;
            ToolTipText = _command.Desc;

            //画像が存在すれば画像を登録する
            //UiResourceManagerがコマンドの画像IDに対応する
            //画像を持っていれば自動的にそれと結びつける。
            ImageList imgList = UiResourceManager.Instance.ImageList;
            if(imgList.Images.ContainsKey(_command.ImageId))
            {
                Image = imgList.Images[_command.ImageId];
            }

            //クリック、ポップアップのイベントにはそれぞれ
            //onClick、onPupupを登録する
            Click += onClick;
            DropDownOpening += onPopup;
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
        /// 今は主に親メニューがポップアップされた
        /// 段階で呼び出されている。
        /// </summary>
        public void update()
        {
            if(_command == null) {
                return;
            }
            Enabled = _command.isEnabled();
            Checked = _command.isChecked();
        }
    }


}
