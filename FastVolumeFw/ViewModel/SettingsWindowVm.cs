using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FastVolumeFw.Annotations;
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

        public SettingsWindowVm()
        {
            IsAppDisabledInFullScreenMode = Default.IsAppDisabledInFullScreenMode;
            PlaySoundAfterVolumeChange = Default.PlaySoundAfterVolumeChange;
            UnmuteWhileChangingVolume = Default.UnmuteWhileChangingVolume;
            VolumeControlWithMouseWheel = Default.VolumeControlWithMouseWheel;
            WindowRightMargin = Default.WindowRightMargin;
        }

        public void RestoreDefaults()
        {
            IsAppDisabledInFullScreenMode = false;
            PlaySoundAfterVolumeChange = true;
            UnmuteWhileChangingVolume = true;
            VolumeControlWithMouseWheel = true;
            WindowRightMargin = 30;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
