using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace _005.坦克大战_启程
{
    internal abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        protected abstract Image GetImage();

        public virtual void Drawself()
        {
            Graphics g = GameFramework.g;
            g.DrawImage(GetImage(), X, Y);
        }

        public virtual void Update()
        {
            Drawself();
        }

        public Rectangle GetRectangle()
        {
            Rectangle rectangle = new Rectangle(X, Y, Width, Height);
            return rectangle;
        }              
    }
}
