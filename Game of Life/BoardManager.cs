using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life
{
    class BoardManager<T> where T : new()
    {
        private readonly Board<T> _board;

        public BoardManager(Board<T> board)
        {
            _board = board;
        }

        public BoardManager((int width, int heigth) dimensions, (int leftMargin, int topMargin) margins) 
        {
            _board = new(dimensions, margins);
        }

        public void PlaceFigureAt(int[,] figure, int y, int x)
        {
            for (int i = 0; i < figure.GetLength(0); i++)
            {
                for (int j = 0; j < figure.GetLength(1); j++)
                {
                    if (figure[i, j] == 1)
                        _board.PlaceCellWithKey(y + i, x + j, new T());
                }
            }
        }

        public void EditBoard(bool clearScreen = true)
        {
            (int height, int width) dimensions = _board.GetDimensions();
            (int topMargin, int leftMargin) margins = _board.GetMargins();
            bool editing = true;

            margins.topMargin++;
            margins.leftMargin++;
            
            _board.DisplayBoard(clearScreen);


            while (editing)
            {
                (int yPos, int xPos, ConsoleKey keyPressed) = ConsoleMenuHandler.MoveCursor(
                    dimensions,
                    margins,
                    Console.GetCursorPosition());

                switch (keyPressed)
                {
                    case ConsoleKey.Enter:
                        {
                            _board.PlaceCellAtBoard(yPos, xPos, new T());
                            break;
                        }

                    case ConsoleKey.Backspace:
                        {
                            _board.RemoveCellAtBoard(yPos, xPos);
                            break;
                        }

                    case ConsoleKey.A:
                        {
                            _board.MoveLeft();
                            break;
                        }

                    case ConsoleKey.D:
                        {
                            _board.MoveRight();
                            break;
                        }

                    case ConsoleKey.W:
                        {
                            _board.MoveUp();
                            break;
                        }

                    case ConsoleKey.S:
                        {
                            _board.MoveDown();
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
    }
}
