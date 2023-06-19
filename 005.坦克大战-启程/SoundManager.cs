using _005.坦克大战_启程.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace _005.坦克大战_启程
{
    internal class SoundManager
    {

        private static SoundPlayer startPlayer = new SoundPlayer();
        private static SoundPlayer addPlayer = new SoundPlayer();
        private static SoundPlayer blastPlayer = new SoundPlayer();
        private static SoundPlayer hitPlayer = new SoundPlayer();
        private static SoundPlayer firePlayer = new SoundPlayer();

        public static void InitSound()
        {
            //引入声音源
            startPlayer.Stream = Resources.start;
            addPlayer.Stream = Resources.add;
            blastPlayer.Stream = Resources.blast;
            hitPlayer.Stream = Resources.hit;
            firePlayer.Stream = Resources.fire;
        }

        //播放
        //游戏开始时
        public static void PlayStart()
        {
            startPlayer.Play();
        }

        //吃到东西
        public static void PlayAdd()
        {
            addPlayer.Play();
        }

        public static void PlayBlast()
        {
            blastPlayer.Play();
        }

        public static void PlayHit()
        {
            hitPlayer.Play();
        }

        public static void PlayFire()
        {
            firePlayer.Play();
        }
    }
}
