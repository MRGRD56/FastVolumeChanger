using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FastVolumeFw.Properties.Settings;

namespace FastVolumeFw.ViewModel
{
    public class SettingsWindowVm
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
            }
        }

        public uint WindowRightMargin
        {
            get => _windowRightMargin;
            set
            {
                _windowRightMargin = value;
                if (Default.WindowRightMargin != value)
                {
                    Default.WindowRightMargin = value;
                    Default.Save();
                    App.GetMainWindow().SetStartupWindowLocation();
                }
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
    }
}
