using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pomodoro_Clock.Views
{
    /// <summary>
    /// Логика взаимодействия для SettingsPomodoro.xaml
    /// </summary>
    public partial class SettingsPomodoro : Window
    {
        public SettingsPomodoro()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void tbDouble_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            int i = t.Text.Length - 1;
            double n;
            if (!Double.TryParse(t.Text, out n) && i != -1)
            {
                if (t.Text[i] == ',')
                {
                    if(t.Text.Contains(','))
                        t.Text = t.Text.Remove(i);
                }
                else t.Text = t.Text.Remove(i);
            }
            t.Select(t.Text.Length, 0);
        }

        private void tbInt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            int i = t.Text.Length - 1;
            double n;
            if (!Double.TryParse(t.Text, out n) && i != -1)
            {
                t.Text = t.Text.Remove(i);
            }
            t.Select(t.Text.Length, 0);
        }
    }
}
