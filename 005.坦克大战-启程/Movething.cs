using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _005.坦克大战_启程
{
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    internal class Movething : GameObject
    {
        private object _lock = new object();
        public Bitmap BitmapUp { get; set; }
        public Bitmap BitmapDown { get; set; }
        public Bitmap BitmapLeft { get; set; }
        public Bitmap BitmapRight { get; set; }

        public int Speed { set; get; }

        private Direction dir;
        public Direction Dir
        {
            get { return dir; }
            set 
            { 
                dir = value;
                //下面这部分虽然与GetImage里的代码高度重合，看似可以放到一起，但实际上GetImage每帧都要调用，放到里面不仅没必要，而且浪费运行资源
                //从需求和实际上来看，会动元素的高宽仅会在方向改变时发生变化
                Bitmap bmp = null;
                switch (dir)
                {
                    case Direction.Up:
                        bmp = BitmapUp;
                        break;
                    case Direction.Down:
                        bmp = BitmapDown;
                        break;
                    case Direction.Left:
                        bmp = BitmapLeft;
                        break;
                    case Direction.Right:
                        bmp = BitmapRight;
                        break;
                }
                //加锁，当某个线程（线程1）执行到加锁相关内容时，另一个线（线程2）程要用到这部分内容里的资源时，
                //必须等线程1执行完加锁内容后才能继续执行
                lock (_lock)
                {
                    Width = bmp.Width;
                    Height = bmp.Height;
                }
            }
        }

        private Bitmap bitmap;
        protected override Image GetImage()
        {
            switch(Dir)
            {
                case Direction.Up:
                    bitmap = BitmapUp;
                    break;
                case Direction.Down:
                    bitmap = BitmapDown;
                    break;
                case Direction.Left:
                    bitmap = BitmapLeft;
                    break;
                case Direction.Right:
                    bitmap = BitmapRight;
                    break;
            }
            bitmap.MakeTransparent(Color.Black);
            return bitmap;
        }

        public override void Drawself()
        {
            lock (_lock)
            {
                base.Drawself();
            }
        }
    }
}
