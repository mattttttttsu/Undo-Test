using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MenuTest.Command;

namespace MenuTest.Menu
{
    /// <summary>
    /// 複数のメニューアイテムを格納するクラス
    /// </summary>
    public class CommandMenuItemFolder : ToolStripMenuItem
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">フォルダの名前</param>
        public CommandMenuItemFolder(String name)
        {
            Text = name;
            DropDownOpening += onPopup;
        }


        /// <summary>
        /// ポップアップされた時に呼び出される処理
        /// </summary>
        /// <param name="sender">呼び出し元のオブジェクト</param>
        /// <param name="e">イベント</param>
        public void onPopup(object sender, EventArgs e)
        {
            if(DropDownItems.Count == 0) {
                return;
            }

            //子どものメニューを更新する
            CommandMenuItem mic;
            foreach(ToolStripItem item in DropDownItems)
            {
                if((mic=(item as CommandMenuItem)) == null) {
                    //今はCommandMenuItem以外のToolStripItemは
                    //処理しない
                    continue;
                }

                //子アイテムの表示を更新する
                mic.update();
            }
        }
    }
}
