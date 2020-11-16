using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Kortspill
{
    public class Program
    { 
        private static readonly GameManager GameManager = new GameManager();

        private static void Main(string[] args )
        {
            GameManager.Init();

            Console.WriteLine("\n(Press any key to quit)");
            Console.ReadKey();
        }
    }
}
