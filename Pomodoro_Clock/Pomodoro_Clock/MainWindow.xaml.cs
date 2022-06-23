using LibraryFunction;
using Pomodoro_Clock.DB.Dapper;
using Pomodoro_Clock.DB.Entities;
using Pomodoro_Clock.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Pomodoro_Clock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DB.Dapper.Dapper dapper;
        DbPomodoro db;
        DispatcherTimer MyTimer;
        TimeSpan MyTime;
        Pomodoro workPomodoro;
        bool IsRunPomodoro = false;
        AutoResetEvent MyResetEvent;
        string connection;
        ObservableCollection<Pomodoro> PlannedPomodoroCollection;
        ObservableCollection<Pomodoro> CompletedPomodoroCollection;
        bool IsEndPomdoro = true;
        public MainWindow()
        {
            InitializeComponent();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MyResetEvent = new AutoResetEvent(true);
            connection = MyFunction.StringConnection("DB/DbPomodoro.mdf");
            dapper = new DB.Dapper.Dapper(connection);
            PlannedPomodoroCollection = new ObservableCollection<Pomodoro>();
            CompletedPomodoroCollection = new ObservableCollection<Pomodoro>();
            db = new DbPomodoro(connection);
            workPomodoro = new Pomodoro() { };
            Calendar.SelectedDate = DateTime.Now.Date;
            ShowTime(workPomodoro.DurationPomodoro);
            DateTime t = DateTime.Now.Date;
            foreach (var item in db?.Pomodoros.Where(p => p.Created == t && !p.Completed).ToList())
            {
                PlannedPomodoroCollection.Add(item);
            }
            foreach (var item in db?.Pomodoros.Where(p => p.Created == t && p.Completed).ToList())
            {
                CompletedPomodoroCollection.Add(item);
            }
            lbCompledPomodoro.ItemsSource = CompletedPomodoroCollection;
            lbPlannedPomodoro.ItemsSource = PlannedPomodoroCollection;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void btnStartPomodoro_Click(object sender, RoutedEventArgs e)
        {
            btnStartPomodoro.Content = "⏸️";
            
            btnSettings.IsEnabled = false;
            btnStopPomodoro.IsEnabled = true;
            IsRunPomodoro = true;
            mItStopPomodoro.IsEnabled = true;
            Task.Run(() => RunPomodoro(workPomodoro));
        }

        private void StartTimer(object sender, EventArgs e)
        {
            tbTime.Text = MyTime.ToString(@"mm\:ss");
            if (MyTime == TimeSpan.Zero)
            {
                MyResetEvent.Set();
                MyTimer.Stop();
            }
            else if (MyTime == TimeSpan.FromSeconds(3))
            {
                ShowBalloon("Pomodoro");
                Task.Run(() =>
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Console.Beep(264, 500);
                        Thread.Sleep(500);
                    }
                    Console.Beep(325, 500);
                });
            }
            MyTime = MyTime.Add(TimeSpan.FromSeconds(-1));
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsPomodoro p = new SettingsPomodoro((Pomodoro)workPomodoro.Clone());
            p.ShowDialog();
            workPomodoro = p.PomodoroSettings;
            lbPlannedPomodoro.SelectedIndex = -1;
            ShowTime(workPomodoro.DurationPomodoro);
        }
        void StartTime(object time)
        {
            int n = (int)time;
            MyTime = TimeSpan.FromSeconds(n - 1);
            MyTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, StartTimer, Application.Current.Dispatcher);
            MyTimer.Start();
        }
        void brdWorkAreaBackground(object obj)
        {
            void c()
            {
                var bc = new BrushConverter();
                brdWorkArea.Background = (Brush)bc.ConvertFrom(obj);
            }
            if (!Dispatcher.CheckAccess())
                Dispatcher.Invoke(c);
            else c();
        }
        void ShowTime(int n)
        {
            void c()
            {
                tbTime.Text = TimeSpan.FromSeconds(n).ToString(@"mm\:ss");
            }
            if (!Dispatcher.CheckAccess())
                Dispatcher.Invoke(c);
            else c();
        }
        void ShowBalloon(string state)
        {
            void c()
            {
                if (Visibility == Visibility.Hidden)
                {
                    var tray = new CustomBalloonUserControl();
                    var nameBindingObject = new Binding("Background");
                    nameBindingObject.Mode = BindingMode.OneWay;
                    nameBindingObject.Source = brdWorkArea;
                    BindingOperations.SetBinding(tray.brBallon, Border.BackgroundProperty, nameBindingObject);

                    var textBindingObject = new Binding("Text");
                    textBindingObject.Mode = BindingMode.OneWay;
                    textBindingObject.Source = tbTime;
                    BindingOperations.SetBinding(tray.Username, TextBlock.TextProperty, textBindingObject);
                    tray.tbState.Text = state;
                    TbIInfo.ShowCustomBalloon(tray, System.Windows.Controls.Primitives.PopupAnimation.Scroll, 4000);
                }
            }
            if (!Dispatcher.CheckAccess())
                Dispatcher.Invoke(c);
            else c();
        }
        
        private void RunPomodoro(Pomodoro tmp)
        {
            for (int i = 0; i < tmp.DailGoal; i++)
            {
                MyResetEvent.WaitOne();
                brdWorkAreaBackground("#FFE84E4E");
                if (!IsRunPomodoro) break;                
                StartTime(tmp.DurationPomodoro);
                MyResetEvent.WaitOne();
                if (!IsRunPomodoro) break;
                if ((i + 1) % tmp.LongBreakDelay == 0)
                {
                    ShowTime(tmp.LongPause);
                    brdWorkAreaBackground("#FF4EE8AC");
                    StartTime(tmp.LongPause - 1);
                }
                else
                {
                    ShowTime(tmp.ShortPause);
                    StartTime(tmp.ShortPause - 1);
                    brdWorkAreaBackground("#FF4EC8E8");
                }
            }
            void c2()
            {
                brdWorkAreaBackground("#FFE84E4E");
                tbTime.Text = TimeSpan.FromSeconds(workPomodoro.DurationPomodoro).ToString(@"mm\:ss");
                TheEndPomodoro();
                if (IsEndPomdoro)
                {
                    workPomodoro.Completed = true;
                    db.SaveChanges();
                    CompletedPomodoroCollection.Add(workPomodoro);
                    PlannedPomodoroCollection.Remove(workPomodoro);
                }
            }
            if (!Dispatcher.CheckAccess())
                Dispatcher.Invoke(c2);
            else c2();

            IsEndPomdoro = true;
        }
        void TheEndPomodoro()
        {
            btnStartPomodoro.Content = "▶";
            btnSettings.IsEnabled = true;
            btnStopPomodoro.IsEnabled = false;
            IsRunPomodoro = false;
            mItStopPomodoro.IsEnabled = false;
            MyTimer?.Stop();
            MyResetEvent.Set();
        }
        private void btnStopPomodoro_Click(object sender, RoutedEventArgs e)
        {

            IsEndPomdoro = false;
            TheEndPomodoro();
        }

        private void TbIInfo_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
            MyFunction.SetFocusWindow(Title);
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (Calendar.SelectedDate.Value < DateTime.Today)
            {
                return;
            }
            Pomodoro pomodoro = new Pomodoro();
            pomodoro.Created = Calendar.SelectedDate.Value;
            pomodoro.Completed = false;
            pomodoro.NamePomodoro = tbPomodoroName.Text;
            pomodoro.DurationPomodoro = (int)(Double.Parse(tbDurationPomodoro.Text) * 60);
            pomodoro.ShortPause = (int)(Double.Parse(tbShortPause.Text) * 60);
            pomodoro.LongPause = (int)(Double.Parse(tbLongPause.Text) * 60);
            pomodoro.LongBreakDelay = int.Parse(tbLongBreakDelay.Text);
            pomodoro.DailGoal = int.Parse(tbDailGoal.Text);
            pomodoro.IsAutoPause = cbIsAutoPause.IsChecked.Value;
            pomodoro.IsAutoStart = cbIsAutoStart.IsChecked.Value;
            if (pomodoro.Created.Date == DateTime.Now.Date)
                PlannedPomodoroCollection.Add(pomodoro);
            db.Pomodoros.Add(pomodoro);
            db.SaveChanges();
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
        private void mItClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            SearchResult(pressed.Tag);
        }

        private void SearchResult(object tag)
        {
            List<ResultDapper> tmp = null;
            string stringname = null, stringvalue = null;
            switch (tag.ToString())
            {
                case "PomodoroDay":
                    tmp = dapper.DayPomodoro().ToList();
                    stringname = "Дата: ";
                    stringvalue = "Кількість помодорів: ";
                    break;
                case "PomodoroMonth":
                    stringname = "Місяць: ";
                    stringvalue = "Кількість помодорів: ";
                    tmp = dapper.MonthPomodoro().ToList();
                    break;
                case "MaxDurationPomodoro":
                    stringname = "Назва Pomodoro: ";
                    stringvalue = "Тривалість(секунд): ";
                    tmp = dapper.MaxDurationPomodoro().ToList();
                    break;
                case "MaxNumberPomodoro":
                    stringname = "Назва Pomodoro: ";
                    stringvalue = "Кількість Pomodoro: ";
                    tmp = dapper.MaxNumberPomodoro().ToList();
                    break;
                default:
                    break;
            }
            foreach (var item in tmp)
            {
                if (tag.ToString() == "PomodoroDay")
                    item.NameResult = item.NameResult.Split(' ')[0];
                item.NameAppendix = stringname;
                item.ValueAppendix = stringvalue;
            }
            lbResultStatistics.ItemsSource = tmp;
        }

        private void lbPlannedPomodoro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsRunPomodoro) return;
            if (lbPlannedPomodoro.SelectedIndex != -1)
                workPomodoro = (Pomodoro)lbPlannedPomodoro.SelectedItem;
            ShowTime(workPomodoro.DurationPomodoro);
        }

        private void lbPlannedPomodoro_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SettingsPomodoro p = new SettingsPomodoro(workPomodoro);
            p.ShowDialog();
            workPomodoro = p.PomodoroSettings;
            ShowTime(workPomodoro.DurationPomodoro);
        }
    }
}
