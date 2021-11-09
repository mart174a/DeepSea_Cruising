using System;
using System.Diagnostics;

namespace DeepSea_Cruising
{
    public static class WriteCode
    {
        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }
        public static void ClearMultipleLines(int lineCount)
        {
            lineCount -= 1;

            int cursorX = Console.CursorTop - lineCount;

            for (int i = 0; i <= lineCount; i++)
            {
                Debug.WriteLine(i);
                Console.SetCursorPosition(0, cursorX + i);
                Console.Write(new string(' ', Console.WindowWidth));
            }

            Console.SetCursorPosition(0, cursorX);
        }

        public static void WriteError(string errorString)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(errorString);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteInColor(string errorString)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(errorString);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
