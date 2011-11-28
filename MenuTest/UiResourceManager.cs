using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MenuTest
{
    /// <summary>
    /// ���j���[�o�[�A�c�[���o�[���Ŏg�����\�[�X��
    /// �Ǘ�������ȒP�ŗǂ����@���v�����Ȃ��̂�
    /// Manager�N���X��p�ӁB
    /// </summary>
    public class UiResourceManager
    {
        /// <summary>
        /// �N���X�̃C���X�^���X
        /// </summary>
        private static UiResourceManager _instance = new UiResourceManager();
        /// <summary>
        /// �摜���X�g
        /// </summary>
        private ImageList _imageList;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        private UiResourceManager()
        {
            _imageList = new ImageList();
        }


        /// <summary>
        /// �w�肵��ID�̉摜��Ԃ�
        /// </summary>
        public ImageList ImageList
        { get{ return _imageList; } }


        /// <summary>
        /// �N���X�̃C���X�^���X��Ԃ�
        /// </summary>
        public static UiResourceManager Instance
        { get { return _instance; } }
    }
}
