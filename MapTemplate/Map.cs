using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MapTemplate
{
    class Map
    {
        private string[][] map;

        private string[][] map2 =
        {
            new string[] { "w", "w", "w", "w", "w", "w", "w", "w", "w", "w" },
            new string[] { "w", ".", ".", ".", ".", ".", ".", ".", ".", "w" },
            new string[] { "w", ".", ".", ".", "t", ".", ".", ".", ".", "w" },
            new string[] { "w", ".", ".", ".", ".", ".", ".", ".", ".", "w" },
            new string[] { "w", ".", ".", ".", ".", ".", ".", ".", ".", "w" },
            new string[] { "w", ".", ".", ".", ".", ".", ".", ".", ".", "w" },
            new string[] { "w", ".", "t", "w", ".", ".", ".", ".", ".", "w" },
            new string[] { "w", ".", ".", "w", ".", ".", ".", ".", ".", "w" },
            new string[] { "w", ".", ".", "w", ".", ".", ".", ".", ".", "w" },
            new string[] { "w", "w", "w", "w", "w", "w", "w", "w", "w", "w" }
        };

        private string[][] map1 =
        {
            new string[] { "w", "w", "w", "w", "w", "w", "w", "w", "w", "w" },
            new string[] { "w", ".", ".", ".", ".", ".", ".", ".", ".", "w" },
            new string[] { "w", ".", ".", ".", ".", "t", ".", ".", ".", "w" },
            new string[] { "w", ".", "w", ".", ".", ".", ".", ".", ".", "w" },
            new string[] { "w", ".", "w", ".", ".", ".", ".", ".", ".", "w" },
            new string[] { "w", ".", "w", ".", ".", ".", ".", "t", ".", "w" },
            new string[] { "w", ".", ".", ".", ".", "w", "w", ".", ".", "w" },
            new string[] { "w", ".", ".", ".", ".", ".", ".", ".", ".", "w" },
            new string[] { "w", ".", ".", ".", ".", ".", ".", ".", ".", "w" },
            new string[] { "w", "w", "w", "w", "w", "w", "w", "w", "w", "w" }
        };



        public Map(int mapOpt)
        {
            ChooseMap(mapOpt);
        }

        private void ChooseMap(int mapOption)
        {
            if(mapOption == 1)
            {
                map = map1;
            } else if(mapOption == 2)
            {
                map = map2;
            } 
        } 

        // This function places all my moveable objects on my static map,
        // then prints it.
        public void PrintMap(List<GameObject> objects)
        {
            var tempMap = CloneMap(map);

            foreach (GameObject obj in objects)
            {
                tempMap[obj.Y][obj.X] = obj.mapMarker;
            }

            PrintMap(tempMap);
        }

        private string[][] CloneMap(string[][] map)
        {
            string[][] mapClone = new string[map.Length][];

            for (int i = 0; i < map.Length; i++)
            {
                mapClone[i] = (string[]) map[i].Clone();
            }
            return mapClone;
        }

        // This function prints a map that is given to it.
        private void PrintMap(string[][] mapToPrint)
        {
            for (int y = 0; y < mapToPrint.Length; y++)
            {
                for (int x = 0; x < mapToPrint[y].Length; x++)
                {
                    Console.Write(mapToPrint[y][x] + " ");
                }
                Console.WriteLine();
            }
        }

        public List<GameObject> GetMapTargets()
        {
            List<GameObject> targets = new List<GameObject>();
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x].Equals("t"))
                    {
                        targets.Add(new GameObject(x,y,"t"));
                    }
                }
            }

            return targets;
        }

        // Returns what kind of tile it is via an x and y value.
        public string ReturnTile(int x, int y)
        {
            if (x < 0 || y < 0 || x == map[0].Length || y == map.Length)
            {
                return "";
            }
            return map[y][x];
        }

    }
}
