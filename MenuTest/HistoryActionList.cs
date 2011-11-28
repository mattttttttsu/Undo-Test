using System;
using System.Collections.Generic;
using System.Text;

namespace MenuTest
{
    /// <summary>
    /// IHistoryAction�I�u�W�F�N�g�̃��X�g�B
    /// Undo�ERedo�̗��������Ǘ�����
    /// </summary>
    public class HistoryActionList
    {
        /// <summary>
        /// �A�N�V�����̃��X�g
        /// </summary>
        private List<IHistoryAction> _list;

        /// <summary>
        /// undo�J�[�\���̌��݈ʒu
        /// </summary>
        private Int32 _pos;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public HistoryActionList()
        {
            _list = new List<IHistoryAction>();
            _pos = 0;
        }


        /// <summary>
        /// ���X�g�ɃA�N�V������ǉ�����B
        /// �J�[�\���̌��݈ʒu������ɂ���A�N�V������
        /// �폜�����B
        /// </summary>
        /// <param name="action">�ǉ�����A�N�V����</param>
        public void add(IHistoryAction action)
        {
            //�擪����J�[�\���ʒu�܂ł̃A�N�V������
            //�S�č폜����
            for(Int32 i = _pos; i > 0; i--)
            {
                _list.RemoveAt(0);
            }
            _pos = 0;
            _list.Insert(0, action);
        }


        /// <summary>
        /// �A���h�D���\���ǂ�����Ԃ�
        /// </summary>
        /// <returns>
        /// true  => �A���h�D�\
        /// false => �A���h�D�s�\
        /// </returns>
        public Boolean canUndo()
        {
            return (_list.Count > 0 && _pos < _list.Count);
        }


        /// <summary>
        /// ���h�D���\���ǂ�����Ԃ�
        /// </summary>
        /// <returns>
        /// true  => ���h�D���\
        /// false => ���h�D���s�\
        /// </returns>
        public Boolean canRedo()
        {
            return (_list.Count > 0 && _pos > 0);
        }


        /// <summary>
        /// �A���h�D�����s����
        /// </summary>
        public void undo()
        {
            Int32 pos = _pos;
            if(_list.Count > _pos)
            {
                _pos++;
            }
            _list[pos].unexecute();
        }


        /// <summary>
        /// ���h�D�����s����
        /// </summary>
        public void redo()
        {
            if(_pos < 1)
            {
                return;
            }
            _pos--;
            _list[_pos].execute();
        }
    }
}
