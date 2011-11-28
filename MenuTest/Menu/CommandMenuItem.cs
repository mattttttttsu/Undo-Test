using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MenuTest.Command;

namespace MenuTest.Menu
{
    /// <summary>
    /// �R�}���h�����蓖�Ă����j���[�A�C�e���̃N���X
    /// </summary>
    public class CommandMenuItem : ToolStripMenuItem
    {
        /// <summary>
        /// �A�C�e���Ɋ��蓖�Ă�R�}���h
        /// </summary>
        internal ICommand _command;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="commandId">�A�C�e���Ɋ��蓖�Ă�R�}���h</param>
        public CommandMenuItem(String commandId)
        {
            CommandManager cm = CommandManager.getInstance();
            _command = cm.findCommand(commandId);

            Text = _command.Text;
            ToolTipText = _command.Desc;

            //�摜�����݂���Ή摜��o�^����
            //UiResourceManager���R�}���h�̉摜ID�ɑΉ�����
            //�摜�������Ă���Ύ����I�ɂ���ƌ��т���B
            ImageList imgList = UiResourceManager.Instance.ImageList;
            if(imgList.Images.ContainsKey(_command.ImageId))
            {
                Image = imgList.Images[_command.ImageId];
            }

            //�N���b�N�A�|�b�v�A�b�v�̃C�x���g�ɂ͂��ꂼ��
            //onClick�AonPupup��o�^����
            Click += onClick;
            DropDownOpening += onPopup;
        }


        /// <summary>
        /// �A�C�e�����N���b�N�������̃f�t�H���g�̓���
        /// �A�C�e���ɂ���ē�����㏑������\��������̂�
        /// static�ɂ͂��Ȃ��B
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�C�x���g</param>
        public void onClick(object sender, EventArgs e)
        {
            _command.execute();
        }


        /// <summary>
        /// �A�C�e�����|�b�v�A�b�v���ꂽ���̃f�t�H���g�̓���
        /// �A�C�e���ɂ���ē�����㏑������\��������̂�
        /// static�ɂ͂��Ȃ��B
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�C�x���g</param>
        public void onPopup(object sender, EventArgs e)
        {
            return;
        }


        /// <summary>
        /// �������g�̕\�����X�V����
        /// ���͎�ɐe���j���[���|�b�v�A�b�v���ꂽ
        /// �i�K�ŌĂяo����Ă���B
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
