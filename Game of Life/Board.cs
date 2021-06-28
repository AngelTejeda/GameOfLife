using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game_of_Life
{
    public class Board<T> where T : new()
    {
        private Dictionary<(int y, int x), T> _cells;
        private int _height;
        private int _width;
        private int _leftMargin;
        private int _topMargin;
        private int _leftOffset = 0;
        private int _topOffset = 0;

        public Board((int height, int width) dimensions, (int topMargin, int leftMargin) margins)
        {
            _cells = new();
            _height = dimensions.height;
            _width = dimensions.width;
            _leftMargin = margins.leftMargin;
            _topMargin = margins.topMargin;
        }

        public Dictionary<(int y, int x), T> GetCells()
        {
            return _cells;
        }

        // Given the coordinate (y, x) of a cell, it calculates the position inside the board where
        // the cell should be displayed considering the current offset and the position of the board.
        private (int yPos, int xPos) CalculateBoardCoordinates(int y, int x)
        {
            int yPos = y + _topOffset;
            int xPos = x + _leftOffset;

            return (yPos, xPos);
        }

        // Given the coordinate (yPos, xPos) inside the board, it calculates the coordinate of the cell
        // in the given position considering the current offset and the position of the board.
        private (int yPrime, int xPrime) CalculateCellCoordinates(int yPos, int xPos)
        {
            int y = yPos - _topOffset;
            int x = xPos - _leftOffset;

            return (y, x);
        }

        // Verifies if the cell in the coordinate (y, x) should be displayed in the board, considering
        // the current offset and the position of the board.
        private bool IsCoordinateDisplayed(int y, int x)
        {
            (int yPos, int xPos) = CalculateBoardCoordinates(y, x);
            
            // Check y
            if (yPos < 0 || yPos > _height - 1)
                return false;

            // Check x
            if (xPos < 0 || xPos > _width - 1)
                return false;

            return true;
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
                            (int y, int x) = CalculateCellCoordinates(yPos, xPos);
                            PlaceCellAt(y, x, new T(), true);
                            break;
                        } 

                    case ConsoleKey.Backspace:
                        {
                            (int y, int x) = CalculateCellCoordinates(yPos, xPos);
                            RemoveCellAt(y, x, true);
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

        // Writes a certain character inside the board. If the coordinate (y, x) is outside the
        // board range, it will  not be displayed.
        private void DisplayCharAt(char c, int y, int x)
        {
            if (!IsCoordinateDisplayed(y, x))
                return;

            (int yPrime, int xPrime) = CalculateBoardCoordinates(y, x);

            SetCursorInsideBoard(xPrime, yPrime);
            Console.Write(c);
            SetCursorInsideBoard(xPrime, yPrime);
        }

        // Adds a coordinate to the HashTable of selected coordinates.
        // If desired and posible to do so, the cell will be displayed in the board.
        public void PlaceCellAt(int y, int x, T cellObject, bool display = false)
        {
            _cells.TryAdd((y, x), cellObject);

            if (display)
                DisplayCharAt('█', y, x);
        }

        // Removes a coordinate from the HashTable of selected coordinates.
        // If desired and posible to do so, the cell will be removed from the board.
        public void RemoveCellAt(int y, int x, bool display = false)
        {
            _cells.Remove((y, x));

            if (display)
                DisplayCharAt(' ', y, x);
        }

        private void SetCursorInsideBoard(int x, int y)
        {
            Console.SetCursorPosition(_leftMargin + 1 + x, _topMargin + 1 + y);
        }

        private void DrawBoarder()
        {
            // Corners
            SetCursorInsideBoard(-1, -1);
            Console.Write("┌");
            SetCursorInsideBoard(_width, -1);
            Console.Write("┐");
            SetCursorInsideBoard(-1, _height);
            Console.Write("└");
            SetCursorInsideBoard(_width, _height);
            Console.Write("┘");

            // Horizontal Lines
            for (int i = 1; i < _width + 1; i++)
            {
                SetCursorInsideBoard(i - 1, -1);
                Console.Write("─");
                SetCursorInsideBoard(i - 1, _height);
                Console.Write("─");
            }

            // Vertical Lines
            for (int i = 1; i < _height + 1; i++)
            {
                SetCursorInsideBoard(-1, i - 1);
                Console.Write("│");
                SetCursorInsideBoard(_width, i - 1);
                Console.Write("│");
            }
        }

        private void DrawCells()
        {
            // Display Cells
            foreach ((int y, int x) in _cells.Keys)
            {
                DisplayCharAt('█', y, x);
            }
        }

        public void DisplayBoard(bool clearScreen = false)
        {
            if (clearScreen)
                Console.Clear();

            DrawBoarder();
            DrawCells();

            SetCursorInsideBoard(0, 0);
        }
    }
}
