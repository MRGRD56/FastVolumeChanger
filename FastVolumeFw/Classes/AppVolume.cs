using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastVolumeFw.Classes
{
    public class AppVolume
    {
        public string Name { get; set; }

        public int Volume { get; set; }

        public bool IsMuted { get; set; }

        public static ObservableCollection<AppVolume> FromNames(List<string> names)
        {
            return new ObservableCollection<AppVolume>(names.Select(x => new AppVolume
            {
                Name = x,
                Volume = 0,//(int) ProgramsVolumesLib.GetApplicationVolume(x),
                IsMuted = false//(bool) ProgramsVolumesLib.GetApplicationMute(x)
            }));
        }
    }
}
