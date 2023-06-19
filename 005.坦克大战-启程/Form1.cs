using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _005.坦克大战_启程
{
    public partial class Form1 : Form
    {
        private Thread t;
        private static Graphics windowG;//窗口画布
        private static Bitmap tempBitmap;//临时图片

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            windowG = this.CreateGraphics();

            //解决画面闪烁问题
            //一张图片一张图片（tempBitmap）的绘制，然后再把图片放到窗体画布上
            tempBitmap = new Bitmap(520, 490);
            Graphics bmpG = Graphics.FromImage(tempBitmap);
            GameFramework.g = bmpG;

            t = new Thread(new ThreadStart(GameMainThread));//创建线程
            t.Start();//开始线程
        }

        private static void GameMainThread()
        {
            //GameFramework
            GameFramework.Start();

            int sleepTime = 1000 / 60;

            while (true)
            {
                GameFramework.g.Clear(Color.Black);

                GameFramework.Update();

                windowG.DrawImage(tempBitmap, 0, 0);

                Thread.Sleep(sleepTime);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.Abort();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyUp(e);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyDown(e);
        }
    }

}
