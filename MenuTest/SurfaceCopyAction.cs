using System;
using System.Collections.Generic;
using System.Text;

namespace MenuTest
{
    /// <summary>
    /// �T�[�t�F�C�X�̓��e���ۂ��ƒu��������A�N�V����
    /// </summary>
    public class SurfaceCopyAction : IHistoryAction
    {
        /// <summary>
        /// �ҏW�Ώۂ̃h�L�������g
        /// </summary>
        private Document _doc;

        /// <summary>
        /// �ҏW��̃T�[�t�F�C�X
        /// </summary>
        private Surface _surface;

        /// <summary>
        /// �ҏW�O�̃T�[�t�F�C�X
        /// </summary>
        private Surface _backSurface;


        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="doc">�ҏW�Ώۂ̃h�L�������g</param>
        /// <param name="surface">�ҏW��̃T�[�t�F�C�X</param>
        /// <param name="backSurface">�ҏW�O�̃T�[�t�F�C�X</param>
        public SurfaceCopyAction(Document doc, Surface surface, Surface backSurface)
        {
            _doc = doc;
            _surface = surface;
            _backSurface = backSurface;
        }


        #region IHistoryAction �����o

        /// <summary>
        /// �A�N�V�����̎��s�Aredo�B
        /// </summary>
        public void execute()
        {
            _doc.Surface.copy(0, 0, _surface, 0, 0, _surface.W, _surface.H);
            _doc.endEdit();
        }


        /// <summary>
        /// �A�N�V������undo�B
        /// </summary>
        public void unexecute()
        {
            _doc.Surface.copy(0, 0, _backSurface, 0, 0, _surface.W, _surface.H);
            _doc.endEdit();
        }

        #endregion
    }
}
