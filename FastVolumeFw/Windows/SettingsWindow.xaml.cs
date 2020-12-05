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
using FastVolumeFw.ViewModel;

namespace FastVolumeFw.Windows
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindowVm ViewModel { get; }

        public SettingsWindow()
        {
            InitializeComponent();
            ViewModel = new SettingsWindowVm();
            DataContext = ViewModel;
        }

        private void CloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            App.GetMainWindow().Close();
        }

        private void WindowTitleGrid_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void TitleMinimizeWindowButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void TitleCloseWindowButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
