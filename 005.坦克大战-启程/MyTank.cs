using _005.坦克大战_启程.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _005.坦克大战_启程
{

    class MyTank : Movething
    {
        public bool IsMoving { get; set; }
        public int HP { get; set; }
        private int originalX;
        private int originalY;
        //speed 每帧移动多少像素
        public MyTank(int x, int y, int speed)
        {
            IsMoving = false;
            HP = 4;
            this.X = x;
            this.Y = y;
            originalX = x;
            originalY = y;
            this.Speed = speed;
            BitmapUp = Resources.MyTankUp;
            BitmapDown = Resources.MyTankDown;
            BitmapLeft = Resources.MyTankLeft;
            BitmapRight = Resources.MyTankRight;
            this.Dir = Direction.Up;
        }

        public override void Update()
        {
            MoveCheck();//移动检查，判断坦克能否继续移动，用来防止坦克超出窗体边界和穿墙
            Move();

            base.Update();
        }

        private void MoveCheck()
        {
            #region 检查有没有超出窗体边界
            if (Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    IsMoving = false;return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if(Y + Height +Speed>390)
                {
                    IsMoving = false;return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if(X-Speed<0)
                {
                    IsMoving = false;return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if(X+Width+Speed>390)
                {
                    IsMoving = false;return;
                }
            }
            #endregion
            //检查有没有和其他元素发生碰撞
            Rectangle rect = GetRectangle();
            switch (Dir)
            {
                case Direction.Up:
                    rect.Y -= Speed;
                    break;
                case Direction.Down:
                    rect.Y += Speed;
                    break;
                case Direction.Left:
                    rect.X -= Speed;
                    break;
                case Direction.Right:
                    rect.X += Speed;
                    break;
            }

            if (GameObjectManager.IsCollidedWall( rect ) != null)
            {
                IsMoving = false;return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                IsMoving = false; return;
            }
        }

        private void Move()
        {
            if (IsMoving == false) return;

            switch (Dir)
            {
                case Direction.Up:
                    Y -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Left:X -= Speed;
                    break;
                case Direction.Right:X += Speed;
                    break;
            }
        }

        //系统自带事件，按键按下
        //当按键按下时，系统会自动创建一个KeyDown线程用于执行这部分内容
        public void KeyDown(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    Dir = Direction.Up;
                    IsMoving = true;
                    break;
                case Keys.S:
                    Dir = Direction.Down;
                    IsMoving = true;
                    break;
                case Keys.A:
                    Dir = Direction.Left;
                    IsMoving = true;
                    break;
                case Keys.D:
                    Dir = Direction.Right;
                    IsMoving = true;
                    break;
                case Keys.Space:
                    Attack();
                    break;
            }
        }

        private void Attack()
        {
            //发射子弹
            int x = this.X;
            int y = this.Y;

            switch (Dir) 
            {
                case Direction.Up:
                    x += Width / 2;
                    break;
                case Direction.Down:
                    x += Width / 2;
                    y += Height;
                    break;
                case Direction.Left:
                    y += Height / 2;
                    break;
                case Direction.Right:
                    x += Width;
                    y += Height / 2;
                    break;
            }
            GameObjectManager.CreateBullet(x, y, Dir, Tag.MyTank);
            SoundManager.PlayFire();
        }

        public void KeyUp(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    IsMoving = false;
                    break;
                case Keys.S:
                    IsMoving = false;
                    break;
                case Keys.A:
                    IsMoving = false;
                    break;
                case Keys.D:
                    IsMoving = false;
                    break;
            }
        }

        public void TakeDamage()
        {
            HP--;

            if(HP<=0)
            {
                X = originalX;
                Y = originalY;
                HP = 4;
            }
        }
    }
}
