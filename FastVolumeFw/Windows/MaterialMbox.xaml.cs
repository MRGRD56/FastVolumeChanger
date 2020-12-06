using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FastVolumeFw.Windows
{
    /// <summary>
    /// Логика взаимодействия для MaterialMbox.xaml
    /// </summary>
    public partial class MaterialMbox : Window
    {
        public MessageBoxResult Result { get; private set; } = MessageBoxResult.None;

        public MaterialMbox(string message, string title = "", MessageBoxButton buttons = MessageBoxButton.OKCancel)
        {
            InitializeComponent();

            MessageTextBlock.Text = message;
            TitleLabel.Content = title;

            switch (buttons)
            {
                case MessageBoxButton.OKCancel:
                    OkCancelButtonsGrid.Visibility = Visibility.Visible;
                    OkSingleButton.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.OK:
                    OkCancelButtonsGrid.Visibility = Visibility.Collapsed;
                    OkSingleButton.Visibility = Visibility.Visible;
                    break;
                default:
                    throw new NotImplementedException("MaterialMbox supports only OK and Cancel buttons.");
            }
        }

        private void WindowTitleGrid_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void TitleCloseWindowButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var str = ((Button) sender).Tag.ToString();
            Result = str switch
            {
                "OK" => MessageBoxResult.OK,
                "Cancel" => MessageBoxResult.Cancel,
                _ => MessageBoxResult.None
            };
            Close();
        }
    }
}
