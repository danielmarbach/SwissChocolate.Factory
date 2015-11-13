using System;

namespace Blending
{
    public static class SpecialConsole
    {
        static readonly object Locker = new object();

        public static void WriteAt(int left, int top, string messsage)
        {
            lock (Locker)
            {
                var cursorPos = new { Left = Console.CursorLeft, Top = Console.CursorTop };
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, 1);
                Console.Write(messsage);
                Console.SetCursorPosition(cursorPos.Left, cursorPos.Top);
                Console.CursorVisible = true;
            }
        }

        public static void WriteLine(string message = null)
        {
            lock (Locker)
            {
                Console.WriteLine(message);
            }
        }
    }
}