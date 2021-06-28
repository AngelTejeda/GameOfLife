using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life
{
    public static class  ConsoleExtension
    {
        public static (int width, int heigth) GetWindowSize()
        {
            int height = Console.WindowHeight;
            int width = Console.WindowWidth;

            return (height, width);
        }
    }
}
