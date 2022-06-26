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
        public void AddSubjectSett(string arg1, string arg2, RegistryValueKind registryValue)
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
            rainbow.TextPaint(ConsoleColor.Gray, "Close Subject Register", 1);
        }
    }
}
