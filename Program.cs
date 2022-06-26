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

            int ready = 0;
            Random generator = new Random();
            int cellsCount = (int)(Math.Pow((double)(dim / step), 2.0));
            int maxRooms = cellsCount * 4 / 5;
            bool[,] cells = new bool[dim / step, dim / step];
            fillBool(cells);

            while (ready < maxRooms)
            {
                int x = generator.Next(dim - 2); //36
                int y = generator.Next(dim - 2); //2
                int cellX = x / step; //4
                int cellY = y / step; //0

                if (cells[cellY, cellX])
                {
                    continue;
                }

                int roomWidth = minWidth + generator.Next(step - 1 - minWidth); //5
                int roomHeight = minHeight + generator.Next(step - 1 - minHeight); //5
                int startY = y - y % step + 1, startX = x - x % step + 1; //1, 33
                int endY = step * (y + 1), endX = step * (x + 1); //8, 40

                startY = startY + generator.Next((endY - roomHeight) % step);
                startX = startX + generator.Next((endX - roomWidth) % step);

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

        public static float placementThreshold;    // chance of empty space

        public static void MazeDataGenerator()
        {
            placementThreshold = .1f;
        }

        public static int[,] FromDimensions(int sizeRows = dim, int sizeCols = dim)
        {
            int[,] maze = new int[sizeRows, sizeCols];

            int rMax = maze.GetUpperBound(0);
            int cMax = maze.GetUpperBound(1);
            Random generator = new Random();

            for (int i = 0; i <= rMax; i++)
            {
                for (int j = 0; j <= cMax; j++)
                {
                    // outside wall
                    if (i == 0 || j == 0 || i == rMax || j == cMax)
                    {
                        maze[i, j] = wall;
                    }

                    // every other inside space
                    else if (i % 2 == space && j % 2 == space)
                    {
                        //if (generator.NextDouble() > placementThreshold)
                        //{
                        maze[i, j] = wall;

                        // in addition to this spot, randomly place adjacent
                        int a = generator.NextDouble() < .5 ? 0 : (generator.NextDouble() < .5 ? -1 : 1);
                        int b = a != 0 ? 0 : (generator.NextDouble() < .5 ? -1 : 1);
                        maze[i + a, j + b] = wall;
                        //}
                    }
                }
            }

            int[,] maze2 = new int[rMax + 1, cMax + 1];
            int row = rMax;

            for (int i = 0; i <= rMax; i++)
            {
                for (int j = 0; j <= cMax; j++)
                {
                    maze2[i, j] = maze[row, j];
                }
                row--;
            }

            return maze2;
        }

        public static int[,] combina()
        {
            int[,] maze = FromDimensions();

            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    if (rooms[i, j] == space)
                    {
                        maze[i, j] = space;
                    }
                }
            }

            return maze;
        }

        public static void printMap()
        {

            int[,] maze = combina();
            int rMax = maze.GetUpperBound(0);
            int cMax = maze.GetUpperBound(1);

            string msg = "";

            for (int i = 0; i <= rMax; i++)
            {
                for (int j = 0; j <= cMax; j++)
                {
                    if (maze[i, j] == space)
                    {
                        msg += "  ";
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
            gridRoomsGen();
            printMap();
            //while (true)
            //{
            //
            //}
        }
    }
}
