using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro_Register
{
    class Program
    {
        static void Main(string[] args)
        {
            RegSubject.RegPomodoro reg = new RegSubject.RegPomodoro();
            reg.Start();

            

            Console.ReadKey();
            Console.Clear();
        }
    }
}
