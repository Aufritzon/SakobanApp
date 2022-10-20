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
        private bool gameWon = false;
        private Map map;
        private List<GameObject> objects = new List<GameObject>();
        private List<GameObject> targets;

        public Game()
        {
            while (true)
            {
                gameWon = false;
                Console.Clear();
                Console.WriteLine("----------------------------------");
                Console.WriteLine("CClick ENTER to start the game");
                Console.WriteLine("Click ESC to quit");
                ConsoleKey key;
                while (true)
                {
                    key = Console.ReadKey().Key;
                    if (key == ConsoleKey.Enter)
                    {
                        break;

                    }
                    if (key == ConsoleKey.Escape)
                    {
                        return;
                    }

                }

                Console.Clear();
                Console.WriteLine("Click 1 to choose map1");
                Console.WriteLine("Click 2 to choose map2");
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.D1)
                {
                    map = new Map(1);

                }
                if (key == ConsoleKey.D2)
                {
                    map = new Map(2);
                } 

                initGame();
            }

        }

        public void initGame()
        {
            targets = map.GetMapTargets();

            while (true)
            {
                gameWon = false;
                Console.Clear();
                objects.Clear();
                objects.Add(new GameObject(3, 3, "b"));
                objects.Add(new GameObject(3, 5, "b"));
                objects.Add(new GameObject(5, 5, "P"));



                while (!gameWon)
                {
                    Console.Clear();
                    map.PrintMap(objects);
                    Console.WriteLine("Press SPACE to restart");
                    Console.WriteLine("Press ESC to go to start menu");
                    ConsoleKey key = Console.ReadKey().Key;

                    if (key == ConsoleKey.Spacebar)
                    {
                        break;

                    }
                    if (key == ConsoleKey.Escape)
                    {
                        Console.Clear();
                        return;
                    }

                    Direction dir = KeyToDir(key);

                    TryMove(dir, objects.Find(x => x.mapMarker == "P") ?? throw new InvalidOperationException());


                    if (gameWon){
                        Console.Clear();
                        map.PrintMap(objects);
                        Console.WriteLine("You Win");
                        Thread.Sleep(2000);
                        Console.Clear();

                    }

                    
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
                            gameWon = true;

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
