using System;
using System.Collections.Generic;
using System.Threading;

namespace Game_of_Life
{
    class GameOfLife
    {
        private readonly Board _board;
        private readonly int _ms = 250;

        public GameOfLife((int heigth, int width) dimensions, (int topMargin, int leftMargin) margins)
        {
            _board = new(dimensions, margins);
        }

        public GameOfLife(Board board)
        {
            _board = board;
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
            HashSet<Coordinate> deadCells = new();
            HashSet<Coordinate> newCells = new();
            HashSet<Coordinate> candidateCells = new();

            // Find the cells that will die.
            foreach (Coordinate coordinate in _board.CellsCoordinates)
            {
                List<Coordinate> neighbours = GetDeadNeighbours(coordinate);
                
                int liveNeighbours = 8 - neighbours.Count;

                candidateCells.UnionWith(neighbours);

                if (liveNeighbours < 2 || liveNeighbours > 3)
                    deadCells.Add(coordinate);
            }

            // Find the cells wich will be born.
            foreach (Coordinate cell in candidateCells)
            {
                List<Coordinate> neighbours = GetDeadNeighbours(cell);

                int liveNeighbours = 8 - neighbours.Count;

                if (liveNeighbours == 3)
                    newCells.Add(cell);
            }

            // Remove dead cells.
            foreach (Coordinate cell in deadCells)
                _board.RemoveCellWithKey(cell);

            // Add the new cells.
            foreach(Coordinate cell in newCells)
                _board.PlaceCellWithKey(cell);
        }

        private List<Coordinate> GetDeadNeighbours(Coordinate cell)
        {
            List<Coordinate> deadNeighbours = new();

            for (int i = cell.y - 1; i <= cell.y + 1; i++)
            {
                for (int j = cell.x - 1; j <= cell.x + 1; j++)
                {
                    if (i == cell.y && j == cell.x)
                        continue;

                    Coordinate cellCoordinate = new(i, j);

                    if (!_board.CellsCoordinates.Contains(cellCoordinate))
                        deadNeighbours.Add(cellCoordinate);
                }
            }

            return deadNeighbours;
        }
    }
}