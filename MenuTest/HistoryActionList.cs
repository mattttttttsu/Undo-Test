using System;
using System.Collections.Generic;
using System.Text;

namespace MenuTest
{
    /// <summary>
    /// IHistoryActionオブジェクトのリスト。
    /// Undo・Redoの履歴情報を管理する
    /// </summary>
    public class HistoryActionList
    {
        /// <summary>
        /// アクションのリスト
        /// </summary>
        private List<IHistoryAction> _list;

        /// <summary>
        /// undoカーソルの現在位置
        /// </summary>
        private Int32 _pos;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HistoryActionList()
        {
            _list = new List<IHistoryAction>();
            _pos = 0;
        }


        /// <summary>
        /// リストにアクションを追加する。
        /// カーソルの現在位置よりも先にあるアクションは
        /// 削除される。
        /// </summary>
        /// <param name="action">追加するアクション</param>
        public void add(IHistoryAction action)
        {
            //先頭からカーソル位置までのアクションを
            //全て削除する
            for(Int32 i = _pos; i > 0; i--)
            {
                _list.RemoveAt(0);
            }
            _pos = 0;
            _list.Insert(0, action);
        }


        /// <summary>
        /// アンドゥが可能かどうかを返す
        /// </summary>
        /// <returns>
        /// true  => アンドゥ可能
        /// false => アンドゥ不可能
        /// </returns>
        public Boolean canUndo()
        {
            return (_list.Count > 0 && _pos < _list.Count);
        }


        /// <summary>
        /// リドゥが可能かどうかを返す
        /// </summary>
        /// <returns>
        /// true  => リドゥが可能
        /// false => リドゥが不可能
        /// </returns>
        public Boolean canRedo()
        {
            return (_list.Count > 0 && _pos > 0);
        }


        /// <summary>
        /// アンドゥを実行する
        /// </summary>
        public void undo()
        {
            Int32 pos = _pos;
            if(_list.Count > _pos)
            {
                _pos++;
            }
            _list[pos].unexecute();
        }


        /// <summary>
        /// リドゥを実行する
        /// </summary>
        public void redo()
        {
            if(_pos < 1)
            {
                return;
            }
            _pos--;
            _list[_pos].execute();
        }
    }
}
