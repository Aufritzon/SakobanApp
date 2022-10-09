using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MapTemplate
{
    class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }

        public string mapMarker { get; set; }

        public GameObject(int x, int y, string mapMarker)
        {
            X = x;
            Y = y;
            this.mapMarker = mapMarker;
        }

        public Move()
    }
}
