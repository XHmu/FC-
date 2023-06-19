using _005.坦克大战_启程.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _005.坦克大战_启程
{
    internal class EnemyTank:Movething
    {
        //用一块区域最好只生成一个随机数种子，同它去生根发芽
        /*随机数的算法是 获得从1970年的某个时间点到现在的秒数（还是毫秒数来着），然后对它进行一定的操作得到一个数，
          因为秒（毫秒）变化得很快，所以可以近似地看成随机的，
          以next(0,4)为例，假如之前0，1出现的次数比较多，它会加大2，3出现的概率，以此来获得一个人为的“随机”数，
          因而随机数算法得到的随机数被称为伪随机数
         */
        private static Random rd = new Random();

        private int AttackSpeed { get; set; }
        private int attackCount = 0;

        private int ChangeDirSpeed { get; set; }
        private int changeDirCount = 0;
        public EnemyTank(int x, int y, int speed,Bitmap bmpUp,Bitmap bmpDown,Bitmap bmpLeft,Bitmap bmpRight)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapUp = bmpUp;
            BitmapDown = bmpDown;
            BitmapLeft = bmpLeft;
            BitmapRight = bmpRight;
            this.Dir = Direction.Down;
            AttackSpeed = rd.Next(30, 61);
            ChangeDirSpeed = rd.Next(20, 61);
        }

        public override void Update()
        {
            AttackChesk();
            AutoChangeDir();
            MoveCheck();//移动检查，判断坦克能否继续移动，用来防止坦克超出窗体边界和穿墙
            Move();

            base.Update();
        }

        private void AttackChesk()
        {
            attackCount++;
            if (attackCount < AttackSpeed)
            {
                return;
            }
            Attack();
            attackCount = 0;
        }
        private void AutoChangeDir()
        {
            changeDirCount++;
            if (changeDirCount < ChangeDirSpeed)
            {
                return;
            }
            ChangeDirection();
            changeDirCount = 0;
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
            GameObjectManager.CreateBullet(x, y, Dir, Tag.EnemyTank);
        }
        private void ChangeDirection()
        {
            while(true)
            {
                Direction dir = (Direction)rd.Next(0, 4);
                if (dir == Dir)
                    continue;
                else
                {
                    Dir = dir;
                    break;
                }
            }
            MoveCheck();
        }
        private void MoveCheck()
        {
            #region 检查有没有超出窗体边界
            if (Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    ChangeDirection(); return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Height + Speed > 390)
                {
                    ChangeDirection(); return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    ChangeDirection(); return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Width + Speed > 390)
                {
                    ChangeDirection(); return;
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

            if (GameObjectManager.IsCollidedWall(rect) != null)
            {
                ChangeDirection(); return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                ChangeDirection(); return;
            }
        }

        private void Move()
        {
            switch (Dir)
            {
                case Direction.Up:
                    Y -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Left:
                    X -= Speed;
                    break;
                case Direction.Right:
                    X += Speed;
                    break;
            }
        }
    }
}
