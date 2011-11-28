using System;
using System.Collections.Generic;
using System.Text;

namespace MenuTest.Command
{
    /// <summary>
    /// ファイルの新規作成、ファイルを開くダイアログの表示等、
    /// 静的に使用されるコマンドとそれに対応するIDを保持し、
    /// IDに対応するコマンドを返す機能を持つクラス
    /// </summary>
    public class CommandManager
    {
        /// <summary>
        /// 自分自身のインスタンス。
        /// </summary>
        private static CommandManager _instance = new CommandManager();

        /// <summary>
        /// コマンドのリスト。コマンドIDとペアで管理する
        /// </summary>
        private Dictionary<String, ICommand> _commands;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CommandManager()
        {
            _commands = new Dictionary<String, ICommand>();
        }


        /// <summary>
        /// CommandManagerのインスタンスを返す。
        /// コンストラクタは呼び出し不可能のため必ずこのメソッドを使用する。
        /// </summary>
        /// <returns>CommandManagerのインスタンス</returns>
        public static CommandManager getInstance()
        {
            return _instance;
        }


        /// <summary>
        /// コマンドを登録する
        /// </summary>
        /// <param name="command">コマンド</param>
        public void registerCommand(ICommand command)
        {
            if(_commands.ContainsKey(command.CommandId))
            {
                return;
            }
            _commands.Add(command.CommandId, command);
        }


        /// <summary>
        /// コマンドIDに対応するコマンドを返す
        /// </summary>
        /// <param name="commandId">コマンドID</param>
        /// <returns>コマンドIDに対応するICommand</returns>
        public ICommand findCommand(String commandId)
        {
            return _commands[commandId];
        }
    }
}
