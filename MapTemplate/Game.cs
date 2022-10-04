using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapTemplate
{
    class Game
    {
        Map map;
        List<GameObject> objects = new List<GameObject>();
        public Game()
        {
            map = new Map();
            objects.Add(new GameObject(5, 5, "P"));

            map.PrintMap(objects);
        }

        // Move the given object one step left.
        public bool MoveLeft(GameObject gameObject)
        {
            // Get what kind of tile we're stepping on.
            string tile = map.ReturnTile(gameObject.X - 1, gameObject.Y);

            if (tile.Equals(""))
            {
                Console.WriteLine("That's out of bounds!");
                return false;
            }
            else if (tile.Equals("w"))
            {
                Console.WriteLine("That's a wall!");
                return false;
            }

            // Perhaps add code to see if we're walking into another object, too.

            gameObject.X--; // Move the object's x one step left.
            return true;
        }
    }
}
