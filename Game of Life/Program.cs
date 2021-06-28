using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Game_of_Life
{
    class Program
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;


        static void Main(string[] args)
        {
            ShowWindow(ThisConsole, MAXIMIZE);

            int[,] boat = new int[,]
            {
                { 1, 1, 0 },
                { 1, 0, 1 },
                { 0, 1, 0 }
            };

            int[,] ship = new int[,]
            {
                { 0, 1, 0 },
                { 0, 0, 1 },
                { 1, 1, 1 }
            };

            int[,] blinker = new int[,]
            {
                { 1, 1, 1}
            };

            int[,] aircraft = new int[,]
            {
                { 1, 1, 0, 0 },
                { 1, 0, 0, 1 },
                { 0, 0, 0, 1 },
                { 0, 0, 1, 1 }
            };

            int[,] python = new int[,]
            {
                { 0, 0, 0, 1, 1 },
                { 1, 0, 1, 0, 1 },
                { 1, 1, 0, 0, 0 }
            };

            int[,] eye = new int[,]
            {
                { 0, 0, 1, 0, 0 },
                { 0, 1, 0, 1, 0 },
                { 1, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 1 },
                { 0, 1, 0, 1, 0 },
                { 0, 0, 1, 0, 0 }
            };

            int[,] flower = new int[,]
            {
                { 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1 },
                { 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0 },
                { 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0 },
            };

            //board.PlaceFigureAt(ship, 1, 1);
            //board.PlaceFigureAt(ship, 1, 6);
            //board.PlaceFigureAt(ship, 6, 1);

            //board.PlaceFigureAt(eye, 20, 50);
            //board.PlaceFigureAt(blinker, 1, 10);

            //board.EditPiece();
            Board<Cell> board = new((20, 20), (2, 2));
            BoardManager<Cell> boardManager = new(board);

            //board.EditBoard();
            boardManager.PlaceFigureAt(flower, 10, 10);

            board.DisplayBoard();

            GameOfLife game = new(board);
            game.Play();
        }
    }
}
