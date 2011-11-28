using System;
using System.Collections.Generic;
using System.Text;

namespace MenuTest
{
    /// <summary>
    /// undo, redoが可能なアクションのインターフェース
    /// </summary>
    public interface IHistoryAction
    {
        /// <summary>
        /// アクションの実行、redo。
        /// </summary>
        void execute();


        /// <summary>
        /// アクションのundo。
        /// </summary>
        void unexecute();
    }
}
