using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life
{
    class BoardManager<T> where T : new()
    {
        private Board<T> _board;

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
                        _board.PlaceCellAt(y + i, x + j, new T(), true);
                }
            }
        }
    }
}
