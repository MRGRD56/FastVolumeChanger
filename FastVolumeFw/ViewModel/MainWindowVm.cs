using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using FastVolumeFw.Annotations;
using FastVolumeFw.Classes;
using GradeWinLib;
using static FastVolumeFw.Properties.Settings;

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
                    SetSystemVolumeAsync(_volume);

                if (Properties.Settings.Default.UnmuteWhileChangingVolume && _volume > 0 && IsMute) 
                    IsMute = false;

                OnPropertyChanged();
            }
        }

        public ObservableCollection<AppVolume> MixerApps { get; set; } = 
            AppVolume.FromNames(Default.MixerApps);

        public async void SetSystemVolumeAsync(int vol)
        {
            await Task.Run(() =>
            {
                DefaultPlaybackDevice.Volume = vol;
            });
        }

        public async void SetSystemMuteAsync(bool mute)
        {
            await Task.Run(() =>
            {
                DefaultPlaybackDevice.Mute(mute);
            });
        }

        public bool IsMute
        {
            get => _isMute;
            set
            {
                if (value == _isMute) return;
                _isMute = value;
                if (DefaultPlaybackDevice.IsMuted != _isMute)
                    SetSystemMuteAsync(_isMute);

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
