using System;
using System.Collections.Generic;
using System.Text;
using MenuTest.Command;
using MenuTest.Menu;
using System.Windows.Forms;
using System.Drawing;

namespace MenuTest
{
    /// <summary>
    /// �A�v���P�[�V�����̃X�^�[�g�A�b�v�I�u�W�F�N�g�ƂȂ�N���X
    /// </summary>
    static class Program
    {
        /// <summary>
        /// �G���g���|�C���g
        /// </summary>
        [STAThread]
        static void Main()
        {
            init();
            MenuTest.Application app = MenuTest.Application.getInstance();
            
            Form1 mainFrame = (Form1)app.MainFrame;
            mainFrame.createToolStrip();

            System.Windows.Forms.Application.Run(app.MainFrame);
        }


        /// <summary>
        /// �����������s����
        /// </summary>
        public static void init()
        {
            //���͂����ŃR�}���h�p�̃A�C�R�����\�[�X�����[�h
            ImageList imgList = UiResourceManager.Instance.ImageList;
            Bitmap imgToolBar = new Bitmap("toolbar.png");
            imgList.Images.AddStrip(imgToolBar);
            imgList.Images.SetKeyName(0, "basic.file.newfile");
            imgList.Images.SetKeyName(1, "basic.file.openfile");
            imgList.Images.SetKeyName(2, "basic.file.savefile");
            imgList.Images.SetKeyName(3, "basic.edit.undo");
            imgList.Images.SetKeyName(4, "basic.edit.redo");
            imgList.Images.SetKeyName(10, "basic.tool.pen");
            imgList.Images.SetKeyName(11, "basic.tool.line");

            //���͂����Ŋ�{�R�}���h�����������o�^
            CommandManager cm = CommandManager.getInstance();

            cm.registerCommand(new NewFileCommand());
            cm.registerCommand(new OpenFileCommand());
            cm.registerCommand(new SaveFileCommand());
            cm.registerCommand(new ExitCommand());

            cm.registerCommand(new UndoCommand());
            cm.registerCommand(new RedoCommand());

            cm.registerCommand(new PenSizeChangeCommand(1));
            cm.registerCommand(new PenSizeChangeCommand(2));
            cm.registerCommand(new PenSizeChangeCommand(3));

            cm.registerCommand(new PenToolCommand());
            cm.registerCommand(new LineToolCommand());
        }
    }
}
