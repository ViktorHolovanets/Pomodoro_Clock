using Microsoft.Win32;
using PomodoroRegister.Rainbow;
using System;

namespace PomodoroRegister.SubSubject.Path
{
    public class pathSubject
    {
        RegistryKey key;

        public pathSubject()
        {
            key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Pomodoro_Clock\Path");
        }

        public void Start()
        {
            CreateSubject();
        }
        private void CreateSubject()
        {

            if (key != null)
            {
                rainbow.LogTextPaint(ConsoleColor.DarkRed, ConsoleColor.DarkYellow, key);
            }
            else
            {
                key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Pomodoro_Clock\Path");
                rainbow.LogTextPaint(ConsoleColor.DarkRed, ConsoleColor.DarkYellow, key);
            }
        }


        public void AddSubjectSett(string arg1, int arg2)
        {
            key.SetValue(arg1, arg2);
            rainbow.LogTextPaint(ConsoleColor.DarkGreen, ConsoleColor.DarkYellow, key + " Create new Settings");
        }
        public void AddSubjectSett(string arg1, string arg2)
        {
            key.SetValue(arg1, arg2);
            rainbow.LogTextPaint(ConsoleColor.DarkGreen, ConsoleColor.DarkYellow, key + " Create new Settings");
        }
        public void AddSubjectSett(string arg1, int arg2, RegistryValueKind registryValue)
        {
            key.SetValue(arg1, arg2, registryValue);
            rainbow.LogTextPaint(ConsoleColor.DarkGreen, ConsoleColor.DarkYellow, key + " Create new Settings");
        }

        public void GetSubjectSett(string arg)
        {
            rainbow.LogTextPaint(ConsoleColor.Gray, ConsoleColor.White, key.GetValue(arg));
        }

        public void Exit()
        {
            key.Close();
            Console.WriteLine();
            rainbow.TextPaint(ConsoleColor.Gray, "Close Path Register", 1);
        }
    }
}
