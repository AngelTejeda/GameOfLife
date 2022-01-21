using System;
using System.Threading.Tasks;

namespace Game_of_Life
{
    class ConsoleMenuHandler
    {
        public static (int, int, ConsoleKey) MoveCursor(
            (int height, int width) dimensions,
            (int topMargin, int leftMargin) margins,
            (int xPos, int yPos) cursorPosition)
        {
            bool defaultVisibility = Console.CursorVisible;

            Console.CursorVisible = true;

            ConsoleKey pressedKey = ConsoleKey.Escape;
            bool exitLoop = false;
            Task task = null;

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
                            if (cursorPosition.xPos < margins.leftMargin + dimensions.width - 1)
                                cursorPosition.xPos++;
                            break;
                        case ConsoleKey.LeftArrow:
                            if (cursorPosition.xPos > margins.leftMargin)
                                cursorPosition.xPos--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (cursorPosition.yPos < margins.topMargin + dimensions.height - 1)
                                cursorPosition.yPos++;
                            break;
                        case ConsoleKey.UpArrow:
                            if (cursorPosition.yPos > margins.topMargin)
                                cursorPosition.yPos--;
                            break;
                        default:
                            exitLoop = true;
                            break;
                    }

                    Console.SetCursorPosition(cursorPosition.xPos, cursorPosition.yPos);

                    task = null;
                }
            }

            Console.CursorVisible = defaultVisibility;
            return (cursorPosition.yPos - margins.topMargin, cursorPosition.xPos - margins.leftMargin, pressedKey);
        }
    }
}