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
        private static GameManager gm = new GameManager();
        static void Main(string[] args )
        {
            gm.Init();
        }
    }
}
