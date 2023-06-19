using _005.坦克大战_启程.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _005.坦克大战_启程
{
    internal class GameObjectManager
    {
        private object _lock = new object(); 
        private static List<NotMovething> wallList = new List<NotMovething>();
        private static List<NotMovething> steelList = new List<NotMovething>();
        private static List<EnemyTank> enemyTanksList = new List<EnemyTank>();
        private static List<Bullet> bulletList = new List<Bullet>();
        private static List<Explosion> expList = new List<Explosion>();
        private static NotMovething boss;
        public static MyTank myTank;
        private static int enemyBornSpeed = 60;
        private static int enemyBornCount = 60;
        private static Point[] points = new Point[3];

        public static void Start()
        {
            points[0].X = 0; points[0].Y = 0;
            points[1].X = 6 * 30; points[1].Y = 0;
            points[2].X = 12 * 30; points[2].Y = 0;
        }

        public static void Update()
        {
            //foreach多线程不安全，不能在遍历的时候列表啥的被修改
            //迭代器遍历？取一个少一个会存在资源竞争的情况，用for循环不会出现这种问题
            //for循环在列表中某元素被去除之后，还会对其进行一次遍历，但由于那个位置存放的是空，不会绘制图片出来，
            //但会进行一次运算，最后在列表越来越大时，用for循环遍历列表会明显感觉程序越来越卡
            foreach (NotMovething num in wallList)
            {
                num.Update();
            }
            foreach (NotMovething num in steelList)
            {
                num.Update();
            }
            foreach(EnemyTank tank in enemyTanksList)
            {
                tank.Update();
            }
            CheckAndDestoryBullet();
            for (int i = 0;i < bulletList.Count;i++)
            {
                bulletList[i].Update();
            }
            CheckAndDestoryExplosion();
            foreach (Explosion exp in expList)
            {
                exp.Update();
            }
            boss.Update();
            myTank.Update();

            EnemyBorn();
        }

        private static void EnemyBorn()
        {
            enemyBornCount++;
            if (enemyBornCount < enemyBornSpeed) return;

            Random rd = new Random();
            int index = rd.Next(0, 3);
            Point position = points[index];
            int enemyType = rd.Next(1, 5);
            switch (enemyType)
            {
                case 1:
                    CreateEnemyTank1(position.X, position.Y);
                    break;
                case 2:
                    CreateEnemyTank2(position.X, position.Y);
                    break;
                case 3:
                    CreateEnemyTank3(position.X, position.Y);
                    break;
                case 4:
                    CreateEnemyTank4(position.X, position.Y);
                    break;
            }
            enemyBornCount = 0;
        }

        public static void CreateBullet(int x,int y,Direction dir,Tag tag)
        {
            Bullet bullet = new Bullet(x, y, 5, dir, tag);
            bulletList.Add(bullet);
        }

        private static void CheckAndDestoryBullet()
        {
            List<Bullet> needToDestory = new List<Bullet>();
            foreach(Bullet bullet in bulletList)
            {
                if (bullet.IsDestory == true)
                {
                    needToDestory.Add(bullet);
                }
            }
            foreach(Bullet bullet in needToDestory)
            {
                bulletList.Remove(bullet);
            }
        }

        private static void CheckAndDestoryExplosion()
        {
            List<Explosion> needToDestory = new List<Explosion>();
            foreach (Explosion exp in expList)
            {
                if (exp.IsNeedDestory == true)
                {
                    needToDestory.Add(exp);
                }
            }
            foreach (Explosion exp in needToDestory)
            {
                expList.Remove(exp);
            }
        }

        public static void CreateExplosion(int x,int y)
        {
            Explosion exp = new Explosion(x, y);
            expList.Add(exp);
        }

        public static void CreateMyTank()
        {
            int x = (int)(4.5 * 30);
            int y = 12 * 30;

            myTank = new MyTank(x, y, 2);
        }

        public static void CreateEnemyTank1(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2,Resources.GrayUp,Resources.GrayDown,Resources.GrayLeft,Resources.GrayRight);
            enemyTanksList.Add(tank);
        }
        public static void CreateEnemyTank2(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.GreenUp, Resources.GreenDown, Resources.GreenLeft, Resources.GreenRight);
            enemyTanksList.Add(tank);
        }
        public static void CreateEnemyTank3(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 4, Resources.QuickUp, Resources.QuickDown, Resources.QuickLeft, Resources.QuickRight);
            enemyTanksList.Add(tank);
        }
        public static void CreateEnemyTank4(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 1, Resources.SlowUp, Resources.SlowDown, Resources.SlowLeft, Resources.SlowRight);
            enemyTanksList.Add(tank);
        }
        public static void CreateMap()
        {
            //填充砖墙列表
            CreateWall(1, 1, 4, Resources.wall, wallList);
            CreateWall(3, 1, 4, Resources.wall, wallList);
            CreateWall(5, 1, 3, Resources.wall, wallList);
            CreateWall(7, 1, 3, Resources.wall, wallList);
            CreateWall(9, 1, 4, Resources.wall, wallList);
            CreateWall(11, 1, 4, Resources.wall, wallList);

            CreateWall(2, 6, 1, Resources.wall, wallList);
            CreateWall(3, 6, 1, Resources.wall, wallList);
            CreateWall(5, 5, 1, Resources.wall, wallList);
            CreateWall(7, 5, 1, Resources.wall, wallList);
            CreateWall(9, 6, 1, Resources.wall, wallList);
            CreateWall(10, 6, 1, Resources.wall, wallList);

            CreateWall(1, 8, 4, Resources.wall, wallList);
            CreateWall(3, 8, 4, Resources.wall, wallList);
            CreateWall(5, 7, 3.5, Resources.wall, wallList);
            CreateWall(6, 7.5, 1, Resources.wall, wallList);
            CreateWall(7, 7, 3.5, Resources.wall, wallList);
            CreateWall(9, 8, 4, Resources.wall, wallList);
            CreateWall(11, 8, 4, Resources.wall, wallList);

            CreateWall1(5.5, 11.5, 1.5, Resources.wall, wallList);
            CreateWall(6, 11.5, 0.5, Resources.wall, wallList);
            CreateWall1(7, 11.5, 1.5, Resources.wall, wallList);

            //填充铁墙列表
            CreateWall(6, 2.5, 1, Resources.steel, steelList);
            CreateWall(0, 6.5, 0.5, Resources.steel, steelList);
            CreateWall(12, 6.5, 0.5, Resources.steel, steelList);

            CreateBoss(6, 12, Resources.Boss);
        }

        private static void CreateWall(double x,double y,double count,Image img,List<NotMovething> wallList)
        {
            int xPosition = (int)(x * 30);
            int yPosition = (int)(y * 30);
            for(int i = yPosition; i < yPosition + count * 30; i += 15)
            {
                //一次建两列墙 i xPosition   i xPosition+15
                NotMovething wall1 = new NotMovething(xPosition, i, img);
                NotMovething wall2 = new NotMovething(xPosition+15, i, img);
                wallList.Add(wall1);
                wallList.Add(wall2);
            }
        }

        private static void CreateWall1(double x, double y, double count, Image img, List<NotMovething> wallList)
        {
            int xPosition = (int)(x * 30);
            int yPosition = (int)(y * 30);
            for (int i = yPosition; i < yPosition + count * 30; i += 15)
            {
                //一次建一列墙 i
                NotMovething wall1 = new NotMovething(xPosition, i, img);
                wallList.Add(wall1);
            }
        }

        public static void DestoryWall(NotMovething wall)
        {
            wallList.Remove(wall);
        }

        public static void DestoryEnemyTank(EnemyTank tank)
        {
            enemyTanksList.Remove(tank);
        }
        private static void CreateBoss(double x, double y, Image img)
        {
            int xPosition = (int)x * 30;
            int yPosition = (int)y * 30;
            boss = new NotMovething(xPosition, yPosition,img);
        }

        public static void KeyUp(KeyEventArgs args)
        {
            myTank.KeyUp(args);
        }

        public static void KeyDown(KeyEventArgs args)
        {
            myTank.KeyDown(args);
        }

        public static NotMovething IsCollidedWall(Rectangle rt)
        {
            foreach(NotMovething wall in wallList)
            {
                if (wall.GetRectangle().IntersectsWith(rt))
                    //rectangle.IntersectsWith(rt)  判断前面那个有没有和后面那个发生碰撞
                    return wall;
            }
            return null;
        }

        public static NotMovething IsCollidedSteel(Rectangle rt)
        {
            foreach (NotMovething wall in steelList)
            {
                if (wall.GetRectangle().IntersectsWith(rt))
                    return wall;
            }
            return null;
        }

        public static bool IsCollidedBoss(Rectangle rt)
        {
            return boss.GetRectangle().IntersectsWith(rt);
        }

        public static MyTank IsCollidedMyTank(Rectangle rt)
        {
            if (myTank.GetRectangle().IntersectsWith(rt))
                return myTank;
             else return null;
        }

        public static EnemyTank IsCollidedEnemyTank(Rectangle rt)
        {
            foreach (EnemyTank tank in enemyTanksList)
            {
                if (tank.GetRectangle().IntersectsWith(rt))
                    return tank;
            }
            return null;
        }
    }
}
