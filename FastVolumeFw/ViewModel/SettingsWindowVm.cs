using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FastVolumeFw.Annotations;
using FastVolumeFw.Classes;
using FastVolumeFw.Windows;
using static FastVolumeFw.Properties.Settings;

namespace FastVolumeFw.ViewModel
{
    public class SettingsWindowVm : INotifyPropertyChanged
    {
        private bool _isAppDisabledInFullScreenMode;
        private bool _playSoundAfterVolumeChange;
        private bool _unmuteWhileChangingVolume;
        private bool _volumeControlWithMouseWheel;
        private uint _windowRightMargin;
        private uint _mouseWheelVolumeChangeStep;
        private bool _showPlaybackButtons;
        private MiddleMouseButtonAction _middleMouseButtonAction;
        private AppLanguage _appLanguage;
        private List<string> _mixerApps;

        public List<MiddleMouseButtonAction> MiddleMouseButtonActions { get; set; } =
            new List<MiddleMouseButtonAction>
            {
                MiddleMouseButtonAction.None,
                MiddleMouseButtonAction.PlayPause,
                MiddleMouseButtonAction.Mute,
                MiddleMouseButtonAction.OpenSettings
            };

        public List<AppLanguage> AppLanguages { get; set; } =
            new List<AppLanguage>
            {
                new AppLanguage("en-US", "English"),
                new AppLanguage("ru-RU", "Русский")
            };

        public bool IsAppDisabledInFullScreenMode
        {
            get => _isAppDisabledInFullScreenMode;
            set
            {
                _isAppDisabledInFullScreenMode = value;
                if (Default.IsAppDisabledInFullScreenMode != value)
                {
                    Default.IsAppDisabledInFullScreenMode = value;
                    Default.Save();
                }
                OnPropertyChanged();
            }
        }

        public bool PlaySoundAfterVolumeChange
        {
            get => _playSoundAfterVolumeChange;
            set
            {
                _playSoundAfterVolumeChange = value;
                if (Default.PlaySoundAfterVolumeChange != value)
                {
                    Default.PlaySoundAfterVolumeChange = value;
                    Default.Save();
                }
                OnPropertyChanged();
            }
        }

        public bool UnmuteWhileChangingVolume
        {
            get => _unmuteWhileChangingVolume;
            set
            {
                _unmuteWhileChangingVolume = value;
                if (Default.UnmuteWhileChangingVolume != value)
                {
                    Default.UnmuteWhileChangingVolume = value;
                    Default.Save();
                }
                OnPropertyChanged();
            }
        }

        public bool VolumeControlWithMouseWheel
        {
            get => _volumeControlWithMouseWheel;
            set
            {
                _volumeControlWithMouseWheel = value;
                if (Default.VolumeControlWithMouseWheel != value)
                {
                    Default.VolumeControlWithMouseWheel = value;
                    Default.Save();
                }
                OnPropertyChanged();
            }
        }

        public uint WindowRightMargin
        {
            get => _windowRightMargin;
            set
            {
                if (value > 100) return;

                _windowRightMargin = value;
                if (Default.WindowRightMargin != value)
                {
                    Default.WindowRightMargin = value;
                    Default.Save();
                    App.GetMainWindow().SetMainWindowParameters();
                }
                OnPropertyChanged();
            }
        }

        public uint MouseWheelVolumeChangeStep
        {
            get => _mouseWheelVolumeChangeStep;
            set
            {
                if (value < 1 || value > 10) return;

                _mouseWheelVolumeChangeStep = value;
                if (Default.MouseWheelVolumeChangeStep != value)
                {
                    Default.MouseWheelVolumeChangeStep = value;
                    Default.Save();
                }
                OnPropertyChanged();
            }
        }

        public bool ShowPlaybackButtons
        {
            get => _showPlaybackButtons;
            set
            {
                _showPlaybackButtons = value;
                if (Default.ShowPlaybackButtons != value)
                {
                    Default.ShowPlaybackButtons = value;
                    Default.Save();
                }
                App.GetMainWindow().SetPlaybackButtonsVisibility(value);
                OnPropertyChanged();
            }
        }

        public MiddleMouseButtonAction MiddleMouseButtonAction
        {
            get => _middleMouseButtonAction;
            set
            {
                _middleMouseButtonAction = value;
                if ((MiddleMouseButtonAction) Default.MiddleMouseButtonAction != value)
                {
                    Default.MiddleMouseButtonAction = (int) value;
                    Default.Save();
                }
                OnPropertyChanged();
            }
        }

        public AppLanguage AppLanguage
        {
            get => _appLanguage;
            set
            {
                _appLanguage = value;
                OnPropertyChanged();
                if (Default.AppLanguage != value.Code)
                {
                    Default.AppLanguage = value.Code;
                    Default.Save();
                    System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(value.Code);
                    var mbox = new MaterialMbox(Properties.Strings.LanguageChangeMessage, Properties.Strings.LanguageChangeTitle, MessageBoxButton.OK);
                    mbox.ShowDialog();
                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }
            }
        }

        public List<string> MixerApps
        {
            get => _mixerApps;
            set
            {
                if (Equals(value, _mixerApps)) return;
                _mixerApps = value;
                Default.MixerApps = value;
                Default.Save();
                OnPropertyChanged();
            }
        }

        public SettingsWindowVm()
        {
            IsAppDisabledInFullScreenMode = Default.IsAppDisabledInFullScreenMode;
            PlaySoundAfterVolumeChange = Default.PlaySoundAfterVolumeChange;
            UnmuteWhileChangingVolume = Default.UnmuteWhileChangingVolume;
            VolumeControlWithMouseWheel = Default.VolumeControlWithMouseWheel;
            WindowRightMargin = Default.WindowRightMargin;
            MouseWheelVolumeChangeStep = Default.MouseWheelVolumeChangeStep;
            ShowPlaybackButtons = Default.ShowPlaybackButtons;
            MiddleMouseButtonAction = (MiddleMouseButtonAction) Default.MiddleMouseButtonAction;
            AppLanguage = AppLanguages.FirstOrDefault(x => Default.AppLanguage == x.Code) ??
                          new AppLanguage("en-US", "English");
            MixerApps = Default.MixerApps;

            //TODO remove
            MixerApps = new List<string> { "chrome.exe", "msedge.exe" };
        }

        public void RestoreDefaults()
        {
            IsAppDisabledInFullScreenMode = false;
            PlaySoundAfterVolumeChange = true;
            UnmuteWhileChangingVolume = true;
            VolumeControlWithMouseWheel = true;
            WindowRightMargin = 30;
            MouseWheelVolumeChangeStep = 2;
            ShowPlaybackButtons = true;
            MiddleMouseButtonAction = MiddleMouseButtonAction.None;
            //AppLanguage - skip
            MixerApps = new List<string>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
