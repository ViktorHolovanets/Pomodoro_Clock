using System.Windows;
using System.Windows.Controls;

namespace Pomodoro_Clock.Views
{
    /// <summary>
    /// Логика взаимодействия для CustomBalloonUserControl.xaml
    /// </summary>
    public partial class CustomBalloonUserControl : UserControl
    {
        public CustomBalloonUserControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) => Visibility = Visibility.Collapsed;
    }
}
