using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FastVolumeFw.Annotations;
using FastVolumeFw.Classes;
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

        public List<MiddleMouseButtonAction> MiddleMouseButtonActions { get; set; } =
            new List<MiddleMouseButtonAction>
            {
                MiddleMouseButtonAction.None,
                MiddleMouseButtonAction.PlayPause,
                MiddleMouseButtonAction.Mute,
                MiddleMouseButtonAction.OpenSettings
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
                    App.GetMainWindow().SetStartupWindowLocation();
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
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
