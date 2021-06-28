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
        private Dictionary<(int y, int x), Cell> _cells;
        private int _height;
        private int _width;
        private int _leftMargin;
        private int _topMargin;
        private int _leftOffset = 0;
        private int _topOffset = 0;

        public Board((int height, int width) dimensions, (int leftMargin, int topMargin) margins)
        {
            _cells = new();
            _height = dimensions.height;
            _width = dimensions.width;
            _leftMargin = margins.leftMargin;
            _topMargin = margins.topMargin;
        }

        public Dictionary<(int y, int x), Cell> GetCells()
        {
            return _cells;
        }

        private (int yPrime, int xPrime) CalculatePrimeCoordinates(int y, int x)
        {
            int yPrime = y + _topMargin + _topOffset;
            int xPrime = x + _leftMargin + _leftOffset;

            return (yPrime, xPrime);
        }

        private (int yPrime, int xPrime) CalculateOriginalCoordinates(int yPrime, int xPrime)
        {
            int y = yPrime - _topMargin - _topOffset;
            int x = xPrime - _leftMargin - _leftOffset;

            return (y, x);
        }

        private void DisplayCellAt(int y, int x)
        {
            if (!IsCoordinateDisplayed(y, x))
                return;

            (int yPrime, int xPrime) = CalculatePrimeCoordinates(y, x);
            
            Console.SetCursorPosition(xPrime, yPrime);
            Console.Write("█");
            Console.SetCursorPosition(xPrime, yPrime);
        }

        private bool IsCoordinateDisplayed(int y, int x)
        {
            (int yPrime, int xPrime) = CalculatePrimeCoordinates(y, x);
            
            // Check y
            if (yPrime < 0 || yPrime > _height - 1)
                return false;

            // Check x
            if (xPrime < 0 || xPrime > _width - 1)
                return false;

            return true;
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

        public void EditBoard(bool clearScreen = false)
        {
            DisplayBoard(clearScreen);

            bool editing = true;

            while (editing)
            {
                (int yPos, int xPos, ConsoleKey keyPressed) = ConsoleMenuHandler.MoveCursor(
                    (_height, _width),
                    (_leftMargin + 1, _topMargin + 1),
                    Console.GetCursorPosition());

                switch(keyPressed)
                {
                    case ConsoleKey.Enter:
                        {
                            (int y, int x) = CalculateOriginalCoordinates(yPos, xPos);
                            PlaceCellAt(y, x);
                            break;
                        } 

                    case ConsoleKey.Backspace:
                        {
                            (int y, int x) = CalculateOriginalCoordinates(yPos, xPos);
                            RemoveCellAt(y, x);
                            break;
                        }

                    case ConsoleKey.Escape:
                        {
                            editing = false;
                            break;
                        }
                }
            }
        }

        public void PlaceCellAt(int y, int x)
        {
            _cells.TryAdd((y, x), new Cell(true));

            if (!IsCoordinateDisplayed(y, x))
                return;

            (int yPrime, int xPrime) = CalculatePrimeCoordinates(y, x);

            Console.SetCursorPosition(xPrime, yPrime);
            Console.Write("█");
            Console.SetCursorPosition(xPrime, yPrime);
        }

        public void RemoveCellAt(int y, int x)
        {
            _cells.Remove((y, x));

            if (!IsCoordinateDisplayed(y, x))
                return;

            (int yPrime, int xPrime) = CalculatePrimeCoordinates(y, x);

            Console.SetCursorPosition(xPrime, yPrime);
            Console.Write(" ");
            Console.SetCursorPosition(xPrime, yPrime);
        }

        private void SetCursorInsideBoard(int y, int x)
        {
            Console.SetCursorPosition(_leftMargin + x, _topMargin + y);
        }

        public void DisplayBoard(bool clearScreen = false)
        {
            if (clearScreen)
                Console.Clear();

            // Display Border

            // Corners
            Console.SetCursorPosition(_leftMargin, _topMargin);
            Console.Write("┌");
            Console.SetCursorPosition(_leftMargin + _width + 1, _topMargin);
            Console.Write("┐");
            Console.SetCursorPosition(_leftMargin, _topMargin + _height + 1);
            Console.Write("└");
            Console.SetCursorPosition(_leftMargin + _width + 1, _topMargin + _height + 1);
            Console.Write("┘");

            // Horizontal Lines
            for (int i = 1; i < _width + 1; i++)
            {
                Console.SetCursorPosition(_leftMargin + i, _topMargin);
                Console.Write("─");
                Console.SetCursorPosition(_leftMargin + i, _topMargin + _height + 1);
                Console.Write("─");
            }

            // Vertical Lines
            for (int i = 1; i < _height + 1; i++)
            {
                Console.SetCursorPosition(_leftMargin, _topMargin + i);
                Console.Write("│");
                Console.SetCursorPosition(_leftMargin + _width + 1, _topMargin + i);
                Console.Write("│");
            }
            Console.SetCursorPosition(_leftMargin + 1, _topMargin + 1);


            // Display Cells
            foreach ((int y, int x) in _cells.Keys)
            {
                // Prevents writing outside the window.
                if (y < 0 || x < 0 || y > _topMargin + _height || x > _leftMargin + _width)
                    continue;

                DisplayCellAt(y, x);
            }

            Console.SetCursorPosition(_leftMargin + 1, _topMargin + 1);
        }
    }
}
