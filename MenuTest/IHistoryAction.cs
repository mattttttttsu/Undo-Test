using System;
using System.Collections.Generic;
using System.Text;

namespace MenuTest
{
    /// <summary>
    /// undo, redo���\�ȃA�N�V�����̃C���^�[�t�F�[�X
    /// </summary>
    public interface IHistoryAction
    {
        /// <summary>
        /// �A�N�V�����̎��s�Aredo�B
        /// </summary>
        void execute();


        /// <summary>
        /// �A�N�V������undo�B
        /// </summary>
        void unexecute();
    }
}
