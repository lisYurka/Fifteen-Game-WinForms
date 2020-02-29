using System;

namespace GIIS_4
{
    class Logic
    {
        static Random rnd = new Random();
        int gameSize;
        int spaceX, spaceY;
        int[,] field;
        int counter = 0;
        public Logic(int Size)
        {
            if (Size > 6)
                Size = 6;
            if (Size < 2)
                Size = 2;
            gameSize = Size;
            field = new int[gameSize, gameSize];
        }
        public int TransOfCoordToAPos(int x, int y)
        {
            if (x < 0)
                x = 0;
            if (x > gameSize - 1)
                x = gameSize - 1;
            if (y < 0)
                y = 0;
            if (y > gameSize - 1)
                y = gameSize - 1;
            return x + gameSize * y;
        }
        private void TransOfPosToACoord(int pos, out int x, out int y)
        {
            if (pos < 0)
                pos = 0;
            if (pos > gameSize * gameSize - 1)
                pos = gameSize * gameSize - 1;
            y = pos / gameSize;
            x = pos % gameSize;
        }
        public void Start()
        {
            for (int i = 0; i < gameSize; i++)
            {
                for (int j = 0; j < gameSize; j++)
                    field[i, j] = TransOfCoordToAPos(i, j) + 1;
            }
            spaceX = gameSize - 1;
            spaceY = gameSize - 1;
            field[spaceX, spaceY] = 0;//типо пробел на 16 кнопке
        }
        public int getNum(int pos)
        {
            int x, y;
            TransOfPosToACoord(pos, out x, out y);
            if (x >= gameSize || x < 0)
                return 0;
            if (y >= gameSize || y < 0)
                return 0;
            return field[x, y];
        }
        public void Moving(int pos)
        {
            int x, y;
            TransOfPosToACoord(pos, out x, out y);
            if (Math.Abs(spaceX - x) + Math.Abs(spaceY - y) != 1)
                return;
            field[spaceX, spaceY] = field[x, y];
            field[x, y] = 0;
            spaceX = x;
            spaceY = y;
        }
        public void MovingForRandom()
        {
            int x = spaceX;
            int y = spaceY;
            int step = rnd.Next(0, gameSize);
            switch (step)
            {
                case 0:
                    x--;
                    break;
                case 1:
                    x++;
                    break;
                case 2:
                    y--;
                    break;
                case 3:
                    y++;
                    break;
            }
            Moving(TransOfCoordToAPos(x,y));
        }
        public bool gameFinish()
        {
            if (!(spaceX == gameSize - 1 && spaceY == gameSize - 1))
                return false;
            for (int i = 0; i < gameSize; i++)
                for (int j = 0; j < gameSize; j++)
                    if (!(i == gameSize - 1 && j == gameSize - 1))
                        if (field[i, j] != TransOfCoordToAPos(i, j) + 1)
                        return false;
            return true;
        }
        public int CounterOfMoves(bool flag)
        {
            if (flag)
                return ++counter;
            else return counter = 0;
        }
    }
}
