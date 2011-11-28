using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MenuTest.Menu
{
    /// <summary>
    /// コンテキストメニューを含んだツールバーのボタンのクラス。
    /// </summary>
    class CommandMenuButtonFolder : ToolStripSplitButton
    {
        /// <summary>
        /// コンストラクタ
        /// パラメータがnameだけだとイメージが設定できない。
        /// 今は画像ID=コマンドIDなので、画像IDをパラメータに追加するのも変。
        /// </summary>
        /// <param name="name">フォルダの名前</param>
        public CommandMenuButtonFolder(String name)
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
            foreach(System.Windows.Forms.ToolStripItem item in DropDownItems)
            {
                if((mic=(item as CommandMenuItem)) == null) {
                    //今はCommandMenuItem以外のToolStripItemは処理しない
                    continue;
                }

                //子アイテムの表示を更新する
                mic.update();
            }
        }
    }
}
