using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Pomodoro_Register.Rainbow;

namespace Pomodoro_Register.RegSubject
{
    public class RegPomodoro
    {
        RegistryKey key;

        public RegPomodoro()
        {
            key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE");
        }

        public void Start()
        {
            CreateSubject();
        }

        private void CreateSubject()
        {

            
           if (key != null)
           {
                key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Pomodoro_Clock");
                if (key == null)
                {
                    key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Pomodoro_Clock");
                    rainbow.TextPaint(ConsoleColor.DarkRed, key + " ", 1);

                }
           }else
           {
                key = Registry.CurrentUser.OpenSubKey(@"Software\Pomodoro_Clock");
                if (key == null)
                {
                    key = Registry.CurrentUser.CreateSubKey(@"Software\Pomodoro_Clock");
                    rainbow.TextPaint(ConsoleColor.DarkRed, key + " ", 1);

                }
            }

            key.Close();
        }
    }
}
