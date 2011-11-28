using System;
using System.Collections.Generic;
using System.Text;

namespace MenuTest.Command
{
    /// <summary>
    /// �R�}���h�̃x�[�X�ƂȂ�N���X
    /// ICommand�C���^�[�t�F�[�X�����N���X��
    /// ���ǂقƂ�ǂ����̎��������邱�ƂɂȂ�̂ŁA
    /// ������ȗ������邽�߂ɗp�ӂ���B
    /// </summary>
    public class BaseCommand : ICommand
    {
        /// <summary>
        /// �R�}���hID
        /// </summary>
        protected String _commandId;
        /// <summary>
        /// �R�}���h�̕\����
        /// </summary>
        protected String _text;
        /// <summary>
        /// �R�}���h�̐�����
        /// </summary>
        protected String _desc;
        /// <summary>
        /// �摜ID
        /// </summary>
        protected String _imageId;

        /// <summary>
        /// �R�}���h�̏�Ԃ��ω��������ɌĂяo�����C�x���g
        /// </summary>
        public event EventHandler CommandStateChangeEvent;

        /// <summary>
        /// �R�}���hID��Ԃ��v���p�e�B
        /// </summary>
        public String CommandId
        { get { return _commandId; } }

        /// <summary>
        /// �R�}���h�̕\������Ԃ��v���p�e�B
        /// </summary>
        public String Text
        { get { return _text; } }

        /// <summary>
        /// �R�}���h�̏ڍו���Ԃ��v���p�e�B
        /// </summary>
        public String Desc
        { get { return _desc; } }

        /// <summary>
        /// �R�}���h�̉摜ID��Ԃ��v���p�e�B
        /// ���̓R�}���hID�����̂܂ܕԂ�
        /// </summary>
        public String ImageId
        { get { return _commandId; } }


        /// <summary>
        /// ���j���[�̗L����Ԃ�Ԃ�
        /// </summary>
        /// <returns>���j���[�̗L�����</returns>
        public virtual Boolean isEnabled()
        { return true; }

        /// <summary>
        /// ���j���[���`�F�b�N����Ă��邩��Ԃ�
        /// </summary>
        /// <returns>���j���[���`�F�b�N����Ă��邩</returns>
        public virtual Boolean isChecked()
        { return false; }

        /// <summary>
        /// �R�}���h�����s����
        /// </summary>
        public virtual void execute()
        { return; }


        /// <summary>
        /// �R�}���h�̏�Ԃɕω������������Ƃ�ʒm����
        /// �T�u�N���X�����CommandStateChangeEvent��
        /// ���ڌĂяo���Ȃ�(�H)�悤�Ȃ̂Ń��\�b�h�Ƃ��ėp�ӁB
        /// </summary>
        public void notifyStateChange()
        {
            if(CommandStateChangeEvent != null)
            {
                CommandStateChangeEvent(this, null);
            }
        }
    }
}
