using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using FastVolumeFw.Annotations;
using FastVolumeFw.Classes;
using FastVolumeFw.Utilities;
using FastVolumeFw.ViewModel;
using GradeWinLib;
using Application = System.Windows.Application;
using Screen = GradeWinLib.Screen;

namespace FastVolumeFw.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
                    SetStartupWindowLocation();
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
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.AppLanguage);

            InitializeComponent();
            ViewModel = new MainWindowVm();
            DataContext = ViewModel;

            SetStartupWindowLocation();
            SetPlaybackButtonsVisibility(Properties.Settings.Default.ShowPlaybackButtons);

            TraceCursorAndVolume();
        }

        public void SetStartupWindowLocation()
        {
            var width = ScreenSize.X - this.Width - Properties.Settings.Default.WindowRightMargin;
            var height = ((double)ScreenSize.Y) / 2 - this.Height / 2;

            if ((int) Left != (int) width || (int) Top != (int) height)
            {
                this.Left = width;
                this.Top = height;
            }
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
                var xZone = ShowToolWindow ? 30 + Width + Properties.Settings.Default.WindowRightMargin : 4;
                ShowToolWindow = (ViewModel.CursorPos.X > ScreenSize.X - xZone &&
                                 (double) ScreenSize.Y / 2 - Height / 2 - yAdditionZone <= ViewModel.CursorPos.Y &&
                                 (double) ScreenSize.Y / 2 + Height / 2 + yAdditionZone >= ViewModel.CursorPos.Y) || MainContextMenu.IsOpen;

                await Task.Delay(50);
            }
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
            ReMute();
        }

        private void ReMute()
        {
            ViewModel.IsMute = !ViewModel.IsMute;
        }

        private void OpenSettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenSettigns();
        }

        private void OpenSettigns()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win is SettingsWindow)
                {
                    win.WindowState = WindowState.Normal;
                    win.Focus();
                    return;
                }
            }

            new SettingsWindow().Show();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            foreach (Window win in Application.Current.Windows)
            {
                try
                {
                    if (win != this)
                        win.Close();
                }
                catch (InvalidOperationException)
                {
                    //ignore
                }
            }
        }

        private void MainBorder_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!Properties.Settings.Default.VolumeControlWithMouseWheel)
                return;

            var step = Properties.Settings.Default.MouseWheelVolumeChangeStep;

            if (e.Delta > 0)
            {
                if (ViewModel.Volume <= 100 - step)
                    ViewModel.Volume += (int) step;
                else
                    ViewModel.Volume = 100;
            }
            else if (e.Delta < 0)
            {
                if (ViewModel.Volume >= step)
                    ViewModel.Volume -= (int) step;
                else
                    ViewModel.Volume = 0;
            }
        }

        public void SetPlaybackButtonsVisibility(bool visible)
        {
            if (visible)
            {
                PlaybackButtonsStackPanel.Visibility = Visibility.Visible;
                MainGrid.RowDefinitions.Last().Height = new GridLength(65);
            }
            else
            {
                PlaybackButtonsStackPanel.Visibility = Visibility.Collapsed;
                MainGrid.RowDefinitions.Last().Height = new GridLength(0);
            }
        }

        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            KeyboardSend.KeyPress(Keys.MediaPlayPause);
        }

        private void PreviousTrackButton_Click(object sender, RoutedEventArgs e)
        {
            KeyboardSend.KeyPress(Keys.MediaPreviousTrack);
        }

        private void NextTrackButton_Click(object sender, RoutedEventArgs e)
        {
            KeyboardSend.KeyPress(Keys.MediaNextTrack);
        }

        private void MainBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle &&
                e.ButtonState == MouseButtonState.Pressed)
            {
                switch ((MiddleMouseButtonAction) Properties.Settings.Default.MiddleMouseButtonAction)
                {
                    case MiddleMouseButtonAction.None:
                        return;
                    case MiddleMouseButtonAction.Mute:
                        ReMute();
                        break;
                    case MiddleMouseButtonAction.OpenSettings:
                        OpenSettigns();
                        break;
                    case MiddleMouseButtonAction.PlayPause:
                        KeyboardSend.KeyPress(Keys.MediaPlayPause);
                        break;
                }
            }
        }
    }
}
