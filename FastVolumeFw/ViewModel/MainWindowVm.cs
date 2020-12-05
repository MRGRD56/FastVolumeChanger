using System.ComponentModel;
using System.Runtime.CompilerServices;
using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using FastVolumeFw.Annotations;
using GradeWinLib;

namespace FastVolumeFw.ViewModel
{
    public class MainWindowVm : INotifyPropertyChanged
    {
        public CoreAudioDevice DefaultPlaybackDevice { get; set; } =
            new CoreAudioController().GetDefaultDevice(DeviceType.Playback, Role.Multimedia);

        private int _volume;
        private bool _isMute;

        public int Volume
        {
            get => _volume;
            set
            {
                if (value == _volume) return;
                if (value < 0)
                {
                    _volume = 0;
                }
                if (value > 100)
                {
                    _volume = 100;
                }
                else
                {
                    _volume = value;
                }

                if ((int) DefaultPlaybackDevice.Volume != _volume)
                    DefaultPlaybackDevice.Volume = _volume;

                if (_volume > 0 && IsMute) 
                    IsMute = false;

                OnPropertyChanged();
            }
        }

        public bool IsMute
        {
            get => _isMute;
            set
            {
                if (value == _isMute) return;
                _isMute = value;
                if (DefaultPlaybackDevice.IsMuted != _isMute)
                    DefaultPlaybackDevice.Mute(_isMute);

                OnPropertyChanged();
            }
        }

        public PointXY CursorPos { get; set; }

        public MainWindowVm()
        {
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
