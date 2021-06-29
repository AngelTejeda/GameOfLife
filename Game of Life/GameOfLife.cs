using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game_of_Life
{
    class GameOfLife
    {
        private readonly Dictionary<(int y, int x), Cell> _candidateCells;
        private readonly Board<Cell> _board;
        private readonly int _height;
        private readonly int _width;
        private readonly int _ms = 250;

        public GameOfLife((int heigth, int width) dimensions, (int topMargin, int leftMargin) margins)
        {
            _board = new(dimensions, margins);
            _candidateCells = new();
            _height = Console.WindowHeight;
            _width = Console.WindowWidth;
        }

        public GameOfLife(Board<Cell> board)
        {
            _board = board;
            _candidateCells = new();
            _height = Console.WindowHeight;
            _width = Console.WindowWidth;
        }

        public void Play()
        {
            bool cursorVisibility = Console.CursorVisible;

            Console.CursorVisible = false;

            for (int i = 0; i < 500; i++)
            {
                CalculateNextGeneration();
                Thread.Sleep(_ms);
            }

            Console.CursorVisible = cursorVisibility;
        }

        private void CalculateNextGeneration()
        {
            foreach (KeyValuePair<(int y, int x), Cell> entry in _board.GetCells())
            {
                ExploreCellNeighbours(entry);
            }

            foreach (KeyValuePair<(int y, int x), Cell> entry in _board.GetCells())
            {
                entry.Value.NextGeneration();

                if (!entry.Value.IsAlive)
                    _board.RemoveCellWithKey(entry.Key.y, entry.Key.x);
            }

            foreach (KeyValuePair<(int y, int x), Cell> entry in _candidateCells)
            {
                entry.Value.NextGeneration();

                _candidateCells.Remove(entry.Key);

                if (entry.Value.IsAlive)
                    _board.PlaceCellWithKey(entry.Key.y, entry.Key.x, new Cell());
            }
        }


        // Counts the number of live cells arround a certain cell.
        // The dead cells around it will be added to the _candidateCells dictionary and their
        // "Neighbours" value will increase.
        /*
         X X X
         X O X
         X X X
         */
        private void ExploreCellNeighbours(KeyValuePair<(int, int), Cell> entry)
        {
            const int outOfBoundsLimit = 200;

            if (!entry.Value.IsAlive)
                return;

            (int y, int x) = entry.Key;

            for (int i = y - 1; i <= y + 1; i++)
            {
                for (int j = x - 1; j <= x + 1; j++)
                {
                    if (i < -outOfBoundsLimit || i > _width + outOfBoundsLimit)
                        continue;

                    if (j < -outOfBoundsLimit || j > _height + outOfBoundsLimit)
                        continue;

                    if (i == y && j == x)
                        continue;

                    if (_board.GetCells().ContainsKey((i, j)))
                        entry.Value.Neighbours++;
                    else
                    {
                        _candidateCells.TryAdd((i, j), new Cell(false));
                        _candidateCells[(i, j)].Neighbours++;
                    }
                }
            }
        }
    }
}
