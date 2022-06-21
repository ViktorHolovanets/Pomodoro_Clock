using System;

namespace PomodoroRegister.Rainbow
{
    public class rainbow
    {
        public static void TextPaint(ConsoleColor Color, object Text, int Id)
        {
            Console.ForegroundColor = Color;

            switch (Id)
            {
                case 1: Console.WriteLine(Text); break;
                case 2: Console.Write(Text); break;
                default:
                    break;
            }
        }

        public static void LogTextPaint(ConsoleColor colorText, ConsoleColor colorLine, object argument)
        {
            Console.WriteLine();
            rainbow.TextPaint(colorText, argument, 2); Lines.line(colorLine);
        }

    }
}
