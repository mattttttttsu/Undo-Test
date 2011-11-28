using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MenuTest.Command;

namespace MenuTest.Menu
{
    /// <summary>
    /// �R�}���h�����蓖�Ă��c�[���o�[�̃{�^���̃N���X
    /// </summary>
    class CommandMenuButton : ToolStripButton
    {
        /// <summary>
        /// �{�^���Ɋ֘A�t�����Ă���R�}���h
        /// </summary>
        internal ICommand _command;

        /// <summary>
        /// �R�}���h
        /// </summary>
        /// <param name="commandId">�R�}���hID</param>
        public CommandMenuButton(String commandId)
        {
            //�R�}���h�}�l�[�W������ID�ɑΉ�����R�}���h���擾
            CommandManager cm = CommandManager.getInstance();
            _command = cm.findCommand(commandId);
            Text = _command.Text;
            ToolTipText = _command.Desc;
            DisplayStyle = ToolStripItemDisplayStyle.Image;

            //�摜�����݂���Ή摜��o�^����
            ImageList imgList = UiResourceManager.Instance.ImageList;
            if(imgList.Images.ContainsKey(_command.ImageId))
            {
                Image = imgList.Images[_command.ImageId];
            }

            //�N���b�N���̃C�x���g�ɂ͎������g��onClick��ǉ�
            Click += onClick;

            //�R�}���h�̏�ԕω����Ď�����
            _command.CommandStateChangeEvent += onCommandStateChange;

            //�쐬����A�������g���A�b�v�f�[�g����B
            //�������Ȃ��ƁA�����Ȃ薳���E�`�F�b�N�ςݏ�ԂɑΉ��ł��Ȃ�
            update();
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
        /// ���͎�ɁA�R�}���h�ɕω�������������
        /// �Ăяo����Ă���B
        /// </summary>
        public void update()
        {
            if(_command == null) {
                return;
            }
            //�R�}���h�̌��ݏ�Ԃɏ]��
            Enabled = _command.isEnabled();
            Checked = _command.isChecked();
        }


        /// <summary>
        /// �R�}���h�̏�Ԃɕω����������ꍇ�ɌĂяo�����
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�C�x���g</param>
        public void onCommandStateChange(object sender, EventArgs e)
        {
            update();
        }
    }
}
