﻿using System;

namespace Game_of_Life
{
    class GameManager
    {
        private readonly Board _board;

        public GameManager(Board board)
        {
            _board = board;
        }

        public GameManager((int width, int heigth) dimensions, (int leftMargin, int topMargin) margins)
        {
            _board = new(dimensions, margins);
        }

        public void PlaceFigureAt(int[,] figure, Coordinate cellCoordinate)
        {
            for (int i = 0; i < figure.GetLength(0); i++)
            {
                for (int j = 0; j < figure.GetLength(1); j++)
                {
                    Coordinate coordinate = new()
                    {
                        y = cellCoordinate.y + i,
                        x = cellCoordinate.x + j
                    };

                    if (figure[i, j] == 1)
                        _board.PlaceCellWithKey(coordinate);
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

                Coordinate boardCoordinate = new()
                {
                    y = yPos,
                    x = xPos
                };

                switch (keyPressed)
                {
                    case ConsoleKey.Enter:
                        {
                            _board.PlaceCellAtBoard(boardCoordinate);
                            break;
                        }

                    case ConsoleKey.Backspace:
                        {
                            _board.RemoveCellAtBoard(boardCoordinate);
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

        public void DrawTitle()
        {
            //The title is 65 characters long and 5 characters tall.
            int margin = (_board.GetDimensions().width - 65) / 2;
            margin += _board.GetMargins().leftMargin;

            WriteWithMargin(margin, 2, "" +
                " ███  ███  ██   ██ ████     ███  █████    █     █████ █████ █████\n" +
                "█    █   █ █ █ █ █ █       █   █ █        █       █   █     █\n" +
                "█ ██ █████ █  █  █ ███     █   █ ███      █       █   ███   ███\n" +
                "█  █ █   █ █  █  █ █       █   █ █        █       █   █     █\n" +
                " ██  █   █ █     █ ████     ███  █        █████ █████ █     █████\n");
        }

        private static void WriteWithMargin(int margin, int line, string text)
        {
            Console.SetCursorPosition(margin, line);

            for(int i=0; i<text.Length; i++)
            {
                if (text[i] == '\n')
                    Console.SetCursorPosition(margin, ++line);
                else
                    Console.Write(text[i]);
            }
        }
    }
}