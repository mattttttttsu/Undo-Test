using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MenuTest.Menu
{
    /// <summary>
    /// �R���e�L�X�g���j���[���܂񂾃c�[���o�[�̃{�^���̃N���X�B
    /// </summary>
    class CommandMenuButtonFolder : ToolStripSplitButton
    {
        /// <summary>
        /// �R���X�g���N�^
        /// �p�����[�^��name�������ƃC���[�W���ݒ�ł��Ȃ��B
        /// ���͉摜ID=�R�}���hID�Ȃ̂ŁA�摜ID���p�����[�^�ɒǉ�����̂��ρB
        /// </summary>
        /// <param name="name">�t�H���_�̖��O</param>
        public CommandMenuButtonFolder(String name)
        {
            Text = name;
            DropDownOpening += onPopup;
        }


        /// <summary>
        /// �|�b�v�A�b�v���ꂽ���ɌĂяo����鏈��
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�C�x���g</param>
        public void onPopup(object sender, EventArgs e)
        {
            if(DropDownItems.Count == 0) {
                return;
            }

            //�q�ǂ��̃��j���[���X�V����
            CommandMenuItem mic;
            foreach(System.Windows.Forms.ToolStripItem item in DropDownItems)
            {
                if((mic=(item as CommandMenuItem)) == null) {
                    //����CommandMenuItem�ȊO��ToolStripItem�͏������Ȃ�
                    continue;
                }

                //�q�A�C�e���̕\�����X�V����
                mic.update();
            }
        }
    }
}
