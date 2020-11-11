using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace kortspill
{
    class Program
    {
        private object _lock;
        public static List<Thread> threads = new List<Thread>();
        public static List<Player> players = new List<Player>();

        static void Main(string[] args )
        {
            Init();
        }

        private static void Init()
        {
            GameManager gm = new GameManager();
            Deck deck = new Deck();

            Player player = PlayerFactory.CreatePlayer();
            Player player2 = PlayerFactory.CreatePlayer();
            Player player3 = PlayerFactory.CreatePlayer();
            Player player4 = PlayerFactory.CreatePlayer();

            players.Add(player);
            players.Add(player2);
            players.Add(player3);
            players.Add(player4);

            Thread t = new Thread(new ThreadStart(player.play));
            Thread t2 = new Thread(new ThreadStart(player2.play));
            Thread t3 = new Thread(new ThreadStart(player3.play));
            Thread t4 = new Thread(new ThreadStart(player4.play));

            threads.Add(t);
            threads.Add(t2);
            threads.Add(t3);
            threads.Add(t4);
            
            t.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            

        }


        

        
    }
}
