using Pomodoro_Clock.DB.Entities;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Pomodoro_Clock.Views
{
    /// <summary>
    /// Логика взаимодействия для SettingsPomodoro.xaml
    /// </summary>
    public partial class SettingsPomodoro : Window
    {
        public Pomodoro PomodoroSettings;
        public SettingsPomodoro(Pomodoro tmp)
        {
            InitializeComponent();
            PomodoroSettings = tmp;
        }
        public SettingsPomodoro()
        {
            InitializeComponent();
            PomodoroSettings = new Pomodoro();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PomodoroSettings.IsAutoStart=cbIsAutoStart.IsChecked.Value;
            PomodoroSettings.IsAutoPause= cbIsAutoPause.IsChecked.Value;
            PomodoroSettings.DurationPomodoro = (int)(Double.Parse(tbDurationPomodoro.Text) * 60);
            PomodoroSettings.ShortPause = (int)(Double.Parse(tbShortPause.Text) * 60);
            PomodoroSettings.LongPause = (int)(Double.Parse(tbLongPause.Text) * 60);
            PomodoroSettings.LongBreakDelay = int.Parse(tbLongBreakDelay.Text);
            PomodoroSettings.DailGoal = int.Parse(tbDailGoal.Text);
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
                    if (t.Text.Contains(','))
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbDurationPomodoro.Text = ((double)PomodoroSettings.DurationPomodoro / 60).ToString();
            tbShortPause.Text = ((double)PomodoroSettings.ShortPause / 60).ToString();
            tbLongPause.Text = ((double)PomodoroSettings.LongPause / 60).ToString();
            tbLongBreakDelay.Text = PomodoroSettings.LongBreakDelay.ToString();
            tbDailGoal.Text = PomodoroSettings.DailGoal.ToString();
            cbIsAutoStart.IsChecked = PomodoroSettings.IsAutoStart;
            cbIsAutoPause.IsChecked=PomodoroSettings.IsAutoPause;
        }
    }
}
