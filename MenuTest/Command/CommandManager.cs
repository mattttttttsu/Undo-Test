using System;
using System.Collections.Generic;
using System.Text;

namespace MenuTest.Command
{
    /// <summary>
    /// �t�@�C���̐V�K�쐬�A�t�@�C�����J���_�C�A���O�̕\�����A
    /// �ÓI�Ɏg�p�����R�}���h�Ƃ���ɑΉ�����ID��ێ����A
    /// ID�ɑΉ�����R�}���h��Ԃ��@�\�����N���X
    /// </summary>
    public class CommandManager
    {
        /// <summary>
        /// �������g�̃C���X�^���X�B
        /// </summary>
        private static CommandManager _instance = new CommandManager();

        /// <summary>
        /// �R�}���h�̃��X�g�B�R�}���hID�ƃy�A�ŊǗ�����
        /// </summary>
        private Dictionary<String, ICommand> _commands;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        private CommandManager()
        {
            _commands = new Dictionary<String, ICommand>();
        }


        /// <summary>
        /// CommandManager�̃C���X�^���X��Ԃ��B
        /// �R���X�g���N�^�͌Ăяo���s�\�̂��ߕK�����̃��\�b�h���g�p����B
        /// </summary>
        /// <returns>CommandManager�̃C���X�^���X</returns>
        public static CommandManager getInstance()
        {
            return _instance;
        }


        /// <summary>
        /// �R�}���h��o�^����
        /// </summary>
        /// <param name="command">�R�}���h</param>
        public void registerCommand(ICommand command)
        {
            if(_commands.ContainsKey(command.CommandId))
            {
                return;
            }
            _commands.Add(command.CommandId, command);
        }


        /// <summary>
        /// �R�}���hID�ɑΉ�����R�}���h��Ԃ�
        /// </summary>
        /// <param name="commandId">�R�}���hID</param>
        /// <returns>�R�}���hID�ɑΉ�����ICommand</returns>
        public ICommand findCommand(String commandId)
        {
            return _commands[commandId];
        }
    }
}
