using System.ComponentModel;
using System.Media;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using FastVolumeFw.Annotations;
using FastVolumeFw.ViewModel;
using GradeWinLib;

namespace FastVolumeFw.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindowVm ViewModel { get; }

        private bool _showToolWindow = false;

        public PointXY ScreenSize
        {
            get
            {
                var w = SystemParameters.PrimaryScreenWidth;
                var h = SystemParameters.PrimaryScreenHeight;
                return new PointXY((int)w, (int)h);
            }
        }

        public bool ShowToolWindow
        {
            get => _showToolWindow;
            set
            {
                _showToolWindow = value;
                if (value)
                {
                    //ColorfulBorder.Background = new SolidColorBrush(Color.FromRgb(0x00, 0xff, 0x00));
                    Visibility = Visibility.Visible;
                }
                else
                {
                    //ColorfulBorder.Background = new SolidColorBrush(Color.FromRgb(0xff, 0x00, 0x00));
                    Visibility = Visibility.Hidden;
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowVm();
            DataContext = ViewModel;

            //ResTb.Text = $"{ScreenSize.X};\n{ScreenSize.Y}";

            SetStartupWindowLocation();

            TraceCursorAndVolume();
        }

        private void SetStartupWindowLocation()
        {
            var width = ScreenSize.X - this.Width - 30;
            var height = ((double)ScreenSize.Y) / 2 - this.Height / 2;
            this.Left = width;
            this.Top = height;
        }

        private async void TraceCursorAndVolume()
        {
            while (true)
            {
                //CursorCoordinatesString = $"X = {GetCursorPosition().X}; Y = {GetCursorPosition().Y}";
                if (Properties.Settings.Default.IsAppDisabledInFullScreenMode &&
                    Utilities.FullScreen.IsForegroundFullScreen())
                {
                    await Task.Delay(500);
                    continue;
                }

                if (ShowToolWindow)
                {
                    await Task.Run(() =>
                    {
                        ViewModel.Volume = (int) ViewModel.DefaultPlaybackDevice.Volume;
                        ViewModel.IsMute = ViewModel.DefaultPlaybackDevice.IsMuted;
                    });
                }

                if (ViewModel.CursorPos == Screen.CursorPosition)
                {
                    await Task.Delay(50);
                    continue;
                }

                ViewModel.CursorPos = Screen.CursorPosition;
                var yAdditionZone = ShowToolWindow ? 30 : 0;
                var xZone = ShowToolWindow ? 30 + Width + 30 : 4;
                ShowToolWindow = ViewModel.CursorPos.X > ScreenSize.X - xZone &&
                                 (double) ScreenSize.Y / 2 - Height / 2 - yAdditionZone <= ViewModel.CursorPos.Y &&
                                 (double) ScreenSize.Y / 2 + Height / 2 + yAdditionZone >= ViewModel.CursorPos.Y;

                await Task.Delay(50);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void VolumeThumb_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (Properties.Settings.Default.PlaySoundAfterVolumeChange)
                SystemSounds.Exclamation.Play();
        }

        private void MuteButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.IsMute = !ViewModel.IsMute;
        }

        private void OpenSettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win is SettingsWindow)
                    return;
            }

            new SettingsWindow().Show();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win != this)
                    win.Close();
            }
        }
    }
}
