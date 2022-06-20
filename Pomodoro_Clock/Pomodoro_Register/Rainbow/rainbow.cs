using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro_Register.Rainbow
{
    public class rainbow
    {
        public static void TextPaint(ConsoleColor Color, string Text, int Id)
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

    }
}
