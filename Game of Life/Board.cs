using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game_of_Life
{
    public class Board
    {
        private readonly Dictionary<(int y, int x), Cell> _activeCells;
        private readonly Dictionary<(int y, int x), Cell> _candidateCells;
        private int _height;
        private int _width;

        public Board()
        {
            _activeCells = new();
            _candidateCells = new();
            _height = Console.WindowHeight;
            _width = Console.WindowWidth;
        }

        public void PlaceFigureAt(int[,] figure, int y, int x)
        {
            for (int i = 0; i < figure.GetLength(0); i++)
            {
                for (int j = 0; j < figure.GetLength(1); j++)
                {
                    if (figure[i, j] == 1)
                        PlaceCellAt(y + i, x + j);
                }
            }
        }

        public void PlaceCellAt(int y, int x)
        {
            _activeCells.TryAdd((y, x), new Cell(true));
        }

        public void RemoveCellAt(int y, int x)
        {
            _activeCells.Remove((y, x));
        }

        public void DisplayBoard()
        {
            Console.Clear();

            foreach ((int y, int x) in _activeCells.Keys)
            {
                // Prevents writing outside the window.
                if (y < 0 || x < 0 || y > _height || x > _width)
                    continue;

                Console.SetCursorPosition(x, y);
                Console.Write("X");
            }

            Console.SetCursorPosition(0, 0);
        }

        public void Play()
        {
            bool cursorVisibility = Console.CursorVisible;

            Console.CursorVisible = false;

            for (int i=0; i<500; i++)
            {
                CalculateNextGeneration();
                Thread.Sleep(50);
            }

            Console.CursorVisible = cursorVisibility;
        }

        private void CalculateNextGeneration()
        {
            foreach (KeyValuePair<(int y, int x), Cell> entry in _activeCells)
            {
                ExploreCellNeighbours(entry);
            }

            foreach (KeyValuePair<(int y, int x), Cell> entry in _activeCells) {
                entry.Value.NextGeneration();

                if (!entry.Value.IsAlive)
                    _activeCells.Remove(entry.Key);

                // Display
                if (!entry.Value.IsAlive)
                {
                    Console.SetCursorPosition(entry.Key.x, entry.Key.y);
                    Console.Write(" ");
                }
            }

            foreach (KeyValuePair<(int y, int x), Cell> entry in _candidateCells)
            {
                entry.Value.NextGeneration();

                if (entry.Value.IsAlive)
                    _activeCells.Add(entry.Key, new Cell(true));

                _candidateCells.Remove(entry.Key);

                // Display
                if (entry.Value.IsAlive)
                {
                    Console.SetCursorPosition(entry.Key.x, entry.Key.y);
                    Console.Write("X");
                }
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

            for (int i=y-1; i<=y+1; i++)
            {
                for (int j=x-1; j<=x+1; j++)
                {
                    if (i < -outOfBoundsLimit || i > _width + outOfBoundsLimit)
                        continue;

                    if (j < -outOfBoundsLimit || j > _height + outOfBoundsLimit)
                        continue;

                    if (i == y && j == x)
                        continue;

                    if (_activeCells.ContainsKey((i, j)))
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
