using _005.坦克大战_启程.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _005.坦克大战_启程
{
    enum GameState
    {
        Running,
        GameOver
    }
    internal class GameFramework
    {
        public static Graphics g;
        private static GameState gameState = GameState.Running;
        public static void Start()
        {
            SoundManager.InitSound();
            GameObjectManager.Start();
            GameObjectManager.CreateMap();
            GameObjectManager.CreateMyTank();
            SoundManager.PlayStart();
        }

        public static void Update()
        {
            //FPS
            if(gameState == GameState.Running)
            {
                GameObjectManager.Update();
            }
            else if(gameState == GameState.GameOver)
            {
                GameOverUpdate();
            }
        }

        private static void GameOverUpdate()
        {
            Bitmap bmp = Resources.GameOver;
             bmp.MakeTransparent(Color.Black);
              int x = 410 / 2 - Resources.GameOver.Width / 2;
            int y = 420 / 2 - Resources.GameOver.Height / 2;
            g.DrawImage(Resources.GameOver, x, y);
        }
        public static void ChangeToGameover()
        {
            gameState = GameState.GameOver;
        }
    }
}
