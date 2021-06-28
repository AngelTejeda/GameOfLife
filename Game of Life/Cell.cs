using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life
{
    public class Cell
    {
        public bool IsAlive { get; set; }
        public int Neighbours { get; set; }

        public Cell(bool isAlive)
        {
            IsAlive = isAlive;
        }

        public void NextGeneration()
        {
            if (IsAlive && (Neighbours < 2 || Neighbours > 3))
                IsAlive = false;
            else if (Neighbours == 3)
                IsAlive = true;

            Neighbours = 0;
        }
    }
}
