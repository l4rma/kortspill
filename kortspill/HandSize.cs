using System;
using System.Collections.Generic;
using System.Text;

namespace Kortspill
{
    //INFO: Singleton Design Pattern
    public sealed class HandSize
    {
        private static HandSize _instance;
        private static readonly object _lockThis = new object();

        private HandSize()
        {
        }

        public static HandSize GetState()
        {
            lock (_lockThis)
            {
                if (_instance == null) _instance = new HandSize();
            }

            return _instance;
        }

        public int Max { get; set; } = 4;
    }
}

