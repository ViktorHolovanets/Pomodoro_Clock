using Microsoft.Win32;
using PomodoroRegister.Rainbow;
using System;

namespace PomodoroRegister
{
    public class RegSubject
    {
        RegistryKey key;

        public RegSubject()
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
                    rainbow.LogTextPaint(ConsoleColor.DarkRed, ConsoleColor.DarkYellow, key);
                }
            }
            else
            {
                key = Registry.CurrentUser.OpenSubKey(@"Software\Pomodoro_Clock");
                if (key == null)
                {
                    key = Registry.CurrentUser.CreateSubKey(@"Software\Pomodoro_Clock");
                    rainbow.LogTextPaint(ConsoleColor.DarkRed, ConsoleColor.DarkYellow, key);

                }
            }

        }

        public void Exit()
        {
            key.Close();
            rainbow.TextPaint(ConsoleColor.Gray, "Close Subject Register", 1);
        }
    }
}
