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

            Thread.Sleep(2000);
            objects.ForEach(x => TryMove(Direction.Left,x));
            Console.Clear();
            map.PrintMap(objects);
        }




        // Try to move the given object one step in the chosen direction (dir)
        public bool TryMove(Direction dir, GameObject gameObject)
        {
            string tile;

            // Get what kind of tile we're stepping on.
            switch (dir)
            {
                case Direction.Left:
                {
                    tile = map.ReturnTile(gameObject.X - 1, gameObject.Y);
                    break;
                }
                case Direction.Right:
                {
                    tile = map.ReturnTile(gameObject.X + 1, gameObject.Y);
                    break;
                }
                case Direction.Down:
                {
                    tile = map.ReturnTile(gameObject.X,gameObject.Y + 1);
                    break;
                }
                case Direction.Up:
                {
                    tile = map.ReturnTile(gameObject.X, gameObject.Y - 1);
                    break;
                }
                default:
                {
                    throw new NotImplementedException();
                }
            }

            switch (tile)
            {
                case "":
                    Console.WriteLine("That's out of bounds!");
                    return false;
                case "w":
                    Console.WriteLine("That's a wall!");
                    return false;
                default:
                    Move(dir, gameObject);
                    return true;
            }
        }

        // Move the given object one step in the chosen direction (dir)
        private void Move(Direction dir, GameObject gameObject)
        {
            switch (dir)
            {
                case Direction.Left:
                {
                    gameObject.X--; // Move the object's x one step left.
                    return;
                }
                case Direction.Right:
                {
                    gameObject.X++; // Move the object's x one step right.
                    return;
                }
                case Direction.Down:
                {
                    gameObject.Y++; // Move the object's y one step down.
                    return;
                }
                case Direction.Up:
                {
                    gameObject.Y--; // Move the object's y one step up.
                    return;
                }
                default:
                {
                    throw new NotImplementedException();
                }
            }
        }

    }
}
