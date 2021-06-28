using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life
{
    class ConsoleMenuHandler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public static (int, int, ConsoleKey) MoveCursor(int heigth, int width)
        {
            bool defaultVisibility = Console.CursorVisible;

            Console.CursorVisible = true;

            ConsoleKey pressedKey = ConsoleKey.Escape;
            int xPos = 0;
            int yPos = 0;
            bool exitLoop = false;
            Task task = null;

            Console.SetCursorPosition(xPos, yPos);

            while (!exitLoop)
            {
                if (task == null)
                {
                    task = Task.Factory.StartNew(() =>
                    {
                        pressedKey = Console.ReadKey(true).Key;
                    });
                }
                else if (task.IsCompleted)
                {
                    switch (pressedKey)
                    {
                        case ConsoleKey.RightArrow:
                            if (xPos < width - 1)
                                xPos++;
                            break;
                        case ConsoleKey.LeftArrow:
                            if (xPos > 0)
                                xPos--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (yPos < heigth - 1)
                                yPos++;
                            break;
                        case ConsoleKey.UpArrow:
                            if (yPos > 0)
                                yPos--;
                            break;
                        default:
                            exitLoop = true;
                            break;
                    }

                    Console.SetCursorPosition(xPos, yPos);

                    task = null;
                }
            }
            
            Console.CursorVisible = defaultVisibility;
            return (yPos, xPos, pressedKey);
        }
    }
}
