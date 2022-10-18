using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace MapTemplate
{



    class Game
    {
        private Map map = new();
        private List<GameObject> objects = new List<GameObject>();
        private List<GameObject> targets;

        public Game()
        {


            initGame();

            targets = map.GetMapTargets();
            targets.ForEach(x => Console.WriteLine(x.mapMarker));

            (int x1, int y1) = (targets[0].X, targets[0].Y);
            (int x2, int y2) = (targets[1].X, targets[1].Y);

            objects.Add(new GameObject(x1, y1, "b"));
            objects.Add(new GameObject(x2, y2, "b"));


            map.PrintMap(objects);


            Console.WriteLine(OnTarget(objects[1]));

            Console.WriteLine(AllBoxesOnTarget());

        }

        public void initGame()
        {
            targets = map.GetMapTargets();

            while (true)
            {

                Console.Clear();
                objects.Clear();
                objects.Add(new GameObject(3, 3, "b"));
                objects.Add(new GameObject(3, 4, "b"));
                objects.Add(new GameObject(5, 5, "P"));



                while (true)
                {

                    map.PrintMap(objects);
                    Console.WriteLine("Press SPACE to restart");
                    ConsoleKey key = Console.ReadKey().Key;

                    if (key == ConsoleKey.Spacebar)
                    {
                        break;

                    }

                    Direction dir = KeyToDir(key);

                    TryMove(dir, objects.Find(x => x.mapMarker == "P") ?? throw new InvalidOperationException());

                    Console.Clear();
                }

            }
        }

        public Direction KeyToDir(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                {
                    return Direction.Up;
                }
                case ConsoleKey.DownArrow:
                {
                    return Direction.Down;
                }
                case ConsoleKey.LeftArrow:
                {
                    return Direction.Left;
                }
                default:
                {
                    return Direction.Right;
                }
            }

        }

        public void UpdateView()
        {

        }




        // Try to move the given object one step in the chosen direction (dir)
        public bool TryMove(Direction dir, GameObject gameObject)
        {
            string tile;

            int x, y;
            
            // Get what kind of tile we're stepping on.
            switch (dir)
            {
                case Direction.Left:
                {
                    tile = map.ReturnTile(x = gameObject.X - 1, y = gameObject.Y);

                    break;
                }
                case Direction.Right:
                {
                    tile = map.ReturnTile(x = gameObject.X + 1, y = gameObject.Y);
                    break;
                }
                case Direction.Down:
                {
                    tile = map.ReturnTile(x = gameObject.X, y = gameObject.Y + 1);
                    break;
                }
                case Direction.Up:
                {
                    tile = map.ReturnTile(x = gameObject.X, y = gameObject.Y - 1);
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
                case ".":
                    if (IsBoxHere(x,y))
                    {
                        bool boxMoved = TryMove(dir, objects.Find(obj => obj.X == x & obj.Y == y));

                        if (!boxMoved) return false;
                        Move(dir,gameObject);
                        return true;

                    }
                    Move(dir,gameObject);
                    return true;
                case "t":
                    if (IsBoxHere(x, y))
                    {
                        bool boxMoved = TryMove(dir, objects.Find(obj => obj.X == x & obj.Y == y));

                        if (!boxMoved) return false;
                        Move(dir, gameObject);
                        return true;

                    }
                    if (gameObject.mapMarker.Equals("b"))
                    {
                        Move(dir,gameObject);
                        Console.Clear();
                        map.PrintMap(objects);
                        if (AllBoxesOnTarget())
                        {
                            Console.WriteLine("You Win");
                            Thread.Sleep(2000);
                        }

                        return true;
                    }
                    Move(dir, gameObject);

                    return true;

                default:
                    Move(dir, gameObject);
                    return true;
 
            }
        }

        private bool OnTarget(GameObject obj)
        {
            return targets.Exists(x => x.X == obj.X & x.Y ==obj.Y);
        }
        

        private bool AllBoxesOnTarget()
        {
            return objects.FindAll(b => b.mapMarker.Equals("b")).TrueForAll(OnTarget);
        }
        public bool IsBoxHere(int x, int y)
        {
            GameObject obj = objects.Find(obj => obj.X == x & obj.Y == y);

            if (obj == null) return false;
            return true;

        }

        private bool checkCollitions(Direction dir, int x, int y)
        {
            GameObject obj = objects.Find(obj => obj.X == x & obj.Y == y);

            if (obj == null) return false;

            if (obj.mapMarker.Equals("b"))
            {
                return TryMove(dir, obj);
                
            }

            return false;

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
