using System;
using System.Collections.Generic;
using System.Text;

namespace MenuTest.Command
{
    /// <summary>
    /// �R�}���h�̃C���^�[�t�F�[�X
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// �R�}���hID
        /// </summary>
        String CommandId
        { get; }
        /// <summary>
        /// �R�}���h�̕\����(���O�H)
        /// </summary>
        String Text
        { get; }
        /// <summary>
        /// �R�}���h�̏ڍו�
        /// </summary>
        String Desc
        { get; }
        /// <summary>
        /// �摜ID
        /// </summary>
        String ImageId
        { get; }

        /// <summary>
        /// �L����Ԃ�Ԃ�
        /// </summary>
        /// <returns>�L�����</returns>
        Boolean isEnabled();
        /// <summary>
        /// �`�F�b�N����Ă��邩��Ԃ�
        /// </summary>
        /// <returns>�`�F�b�N����Ă��邩</returns>
        Boolean isChecked();


        /// <summary>
        /// �R�}���h�����s����B
        /// </summary>
        void execute();

        /// <summary>
        /// �R�}���h�̏�Ԃ��ω��������ɔ�������C�x���g
        /// </summary>
        event EventHandler CommandStateChangeEvent;
    }
}
