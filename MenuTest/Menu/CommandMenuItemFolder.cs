using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MenuTest.Command;

namespace MenuTest.Menu
{
    /// <summary>
    /// �����̃��j���[�A�C�e�����i�[����N���X
    /// </summary>
    public class CommandMenuItemFolder : ToolStripMenuItem
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="name">�t�H���_�̖��O</param>
        public CommandMenuItemFolder(String name)
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
            foreach(ToolStripItem item in DropDownItems)
            {
                if((mic=(item as CommandMenuItem)) == null) {
                    //����CommandMenuItem�ȊO��ToolStripItem��
                    //�������Ȃ�
                    continue;
                }

                //�q�A�C�e���̕\�����X�V����
                mic.update();
            }
        }
    }
}
