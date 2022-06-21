using System;

namespace PomodoroRegister.Rainbow
{
    public class Lines
    {
        public static void line(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(" ***");
        }
    }
}
