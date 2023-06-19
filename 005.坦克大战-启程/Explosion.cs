using _005.坦克大战_启程.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _005.坦克大战_启程
{
    internal class Explosion : GameObject
    {
        public bool IsNeedDestory { get; set; }
        private int playSpeed = 1;
        private int playCount = 0;
        private int index = 0;
        private Bitmap[] bmpArray = new Bitmap[]
        {
            Resources.EXP1,
            Resources.EXP2,
            Resources.EXP3,
            Resources.EXP4,
            Resources.EXP5
        };

        public Explosion(int x,int y)
        {
            IsNeedDestory = false;
            foreach(Bitmap bmp in bmpArray)
            {
                bmp.MakeTransparent(Color.Black);
            }
            this.X = x - bmpArray[0].Width / 2;
            this.Y = y - bmpArray[0].Height / 2;
        }
        protected override Image GetImage()
        {
            if (index > 4) return bmpArray[4];
            return bmpArray[index];
        }

        public override void Update()
        {
            index = (playCount ) / playSpeed;
            playCount++;
            if (index > 4)
                IsNeedDestory = true;
            base.Update();
        }


    }
}
