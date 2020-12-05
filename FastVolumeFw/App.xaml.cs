using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FastVolumeFw.Windows;

namespace FastVolumeFw
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MainWindow GetMainWindow() => (MainWindow) App.Current.MainWindow;
    }
}
