using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Pomodoro_Register.RegSubject
{
    public class RegPomodoro
    {
        public void Start()
        {
            CreateSubject();
        }

        private void CreateSubject()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE");

            if (key != null)
            {
                
                Console.WriteLine("Yes");
            }else
            {

            }
            
            foreach (var item in key.GetSubKeyNames())
            {
                Console.WriteLine(item);
            }

            key.Close();
        }
    }
}
