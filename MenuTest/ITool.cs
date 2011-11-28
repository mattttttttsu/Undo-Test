using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MenuTest
{
    /// <summary>
    /// �c�[���̃C���^�[�t�F�[�X
    /// </summary>
    public interface ITool
    {
        /// <summary>
        /// �c�[��ID��Ԃ��v���p�e�B
        /// </summary>
        Int32 ToolId
        { get; }


        /// <summary>
        /// �A�N�e�B�u�ɂȂ������̏���
        /// </summary>
        void onActivate();

        /// <summary>
        /// �A�N�e�B�u�ł͂Ȃ��Ȃ������̏���
        /// </summary>
        void onDeactivate();
        
        /// <summary>
        /// �}�E�X�̃N���b�N�̏���
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�}�E�X�C�x���g</param>
        void onMouseDown(Object sender, MouseEventArgs e);

        /// <summary>
        /// �}�E�X�̃{�^���𗣂����u�Ԃ̏���
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�}�E�X�C�x���g</param>
        void onMouseUp(Object sender, MouseEventArgs e);

        /// <summary>
        /// �}�E�X�̈ړ��̏���
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�}�E�X�C�x���g�C�x���g</param>
        void onMouseMove(Object sender, MouseEventArgs e);

        /// <summary>
        /// �L�����o�X��`�悷�鎞�̏���
        /// </summary>
        /// <param name="sender">�Ăяo�����̃I�u�W�F�N�g</param>
        /// <param name="e">�y�C���g�C�x���g</param>
        void onPaint(Object sender, PaintEventArgs e);
    }
}
