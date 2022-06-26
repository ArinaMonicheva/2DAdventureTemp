using System;

namespace test
{
    class Program
    {
        private static int[,] rooms;
        private const int dim = 41;
        private const int step = 8;
        private const int wall = 1;
        private const int space = 0;
        private const int minWidth = 3;
        private const int minHeight = 3;

        public static int[,] data
        {
            get; private set;
        }

        public static void fillBool(bool[,] cells)
        {
            for (int i = 0; i <= cells.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= cells.GetUpperBound(1); j++)
                {
                    cells[i, j] = false;
                }
            }
        }
        public static void gridRoomsGen()
        {
            rooms = new int[dim, dim];

            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    rooms[i, j] = wall;
                }
            }

            int maxWidth = step - 1, maxHeight = maxWidth, ready = 0;
            Random generetor = new Random();
            int cellsCount = (int)(Math.Pow((double)(dim / step), 2.0));
            int maxRooms = cellsCount * 3 / 4;
            bool[,] cells = new bool[dim / step, dim / step];
            fillBool(cells);

            while (ready < maxRooms)
            {
                int x = generetor.Next(dim - 2); //36
                int y = generetor.Next(dim - 2); //2
                int cellX = x / step;
                int cellY = y / step;

                if (cells[cellY, cellX])
                {
                    continue;
                }

                int roomWidth = minWidth + generetor.Next(step - 1 - minWidth);
                int roomHeight = minHeight + generetor.Next(step - 1 - minHeight);
                int startY = y - y % step + 1, startX = x - x % step + 1;


                for (int i = startY; i < startY + roomHeight; i++)
                {
                    for (int j = startX; j < startX + roomWidth; j++)
                    {
                        rooms[i, j] = space;
                    }
                }

                cells[cellY, cellX] = true;
                ready++;
            }

        }

        public static void printMap()
        {

            int[,] maze = rooms;
            int rMax = maze.GetUpperBound(0);
            int cMax = maze.GetUpperBound(1);

            string msg = "";

            for (int i = 0; i <= rMax; i++)
            {
                for (int j = 0; j <= cMax; j++)
                {
                    if (maze[i, j] == space)
                    {
                        msg += "--";
                    }
                    else
                    {
                        msg += "==";
                    }
                }
                msg += "\n";
            }
            Console.WriteLine(msg);
        }

        static void Main(string[] args)
        {
            data = new int[,]
            {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
            };

            Console.WriteLine("Hello World!");
            gridRoomsGen();
            printMap();
            while (true)
            {

            }
        }
    }
}
