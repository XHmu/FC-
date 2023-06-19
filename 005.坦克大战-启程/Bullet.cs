using _005.坦克大战_启程.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _005.坦克大战_启程
{
    enum Tag
    {
        MyTank,
        EnemyTank
    }
    internal class Bullet:Movething
    {
        public Tag Tag { get; set; }
        public bool IsDestory { get; set; }
        public Bullet(int x, int y, int speed,Direction dir,Tag tag)
        {
            this.IsDestory = false;
            this.Speed = speed;
            BitmapUp = Resources.BulletUp;
            BitmapDown = Resources.BulletDown;
            BitmapLeft = Resources.BulletLeft;
            BitmapRight = Resources.BulletRight;
            this.Dir = dir;
            this.Tag = tag;
            this.X = x - Width / 2;
            this.Y = y - Height / 2;
        }

        public override void Update()
        {
            MoveCheck();
            Move();

            base.Update();
        }

        private void MoveCheck()
        {
            #region 检查有没有超出窗体边界
            if (Dir == Direction.Up)
            {
                if (Y - Height/2 + 2 < 0)
                {
                    IsDestory = true; return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y - Height/2 - 2 > 390)
                {
                    IsDestory = true; return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X -Width/2 + 2 < 0)
                {
                    IsDestory = true; return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X -Width/2 - 2 > 390)
                {
                    IsDestory = true; return;
                }
            }
            #endregion
            //检查有没有和其他元素发生碰撞
            Rectangle rect = GetRectangle();
            rect.X = X + Width/2 - 2;
            rect.Y = Y + Height/2 - 2;
            rect.Width = 4;
            rect.Height = 4;

            int xExp = X + Width / 2;
            int yExp = Y + Height / 2;

            //墙
            NotMovething wall;
            if ( (wall = GameObjectManager.IsCollidedWall(rect)) != null)
            {
                IsDestory = true;
                GameObjectManager.DestoryWall(wall);
                GameObjectManager.CreateExplosion(xExp, yExp);
                SoundManager.PlayBlast();
                return;
            }

            //钢墙
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                IsDestory = true;
                GameObjectManager.CreateExplosion(xExp, yExp); return;
            }

            //Boss
            if (GameObjectManager.IsCollidedBoss(rect) == true)
            {
                GameFramework.ChangeToGameover();
                SoundManager.PlayBlast();
                return;    
            }

            //敌人
            if (Tag == Tag.MyTank)
            {
                EnemyTank tank;
                if((tank = GameObjectManager.IsCollidedEnemyTank(rect)) != null)
                {
                    IsDestory = true;
                    GameObjectManager.DestoryEnemyTank(tank);
                    GameObjectManager.CreateExplosion(xExp, yExp);
                    SoundManager.PlayBlast();
                    return;
                }
            }
            else if(Tag == Tag.EnemyTank)
            {
                MyTank myTank;
                if((myTank = GameObjectManager.IsCollidedMyTank(rect)) != null)
                {
                    IsDestory = true;
                    myTank.TakeDamage();
                    GameObjectManager.CreateExplosion(xExp, yExp);
                    SoundManager.PlayHit();
                    return;
                }
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
