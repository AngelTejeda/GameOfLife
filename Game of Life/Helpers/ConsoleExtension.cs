using System;
using System.Runtime.InteropServices;

namespace Game_of_Life
{
    public static class ConsoleExtension
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private readonly static IntPtr ThisConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public enum WindowActions
        {
            HIDE = 0,
            MAXIMIZE = 3,
            MINIMIZE = 6,
            RESTORE = 9
        }

        public static (int height, int width) GetWindowSize()
        {
            int height = Console.WindowHeight;
            int width = Console.WindowWidth;

            return (height, width);
        }

        public static void ApplyWindowAction(WindowActions action)
        {
            ShowWindow(ThisConsole, ((int)action));
        }
    }
}