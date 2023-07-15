using Hotel_POS.ViewModel;
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

namespace Hotel_POS.View
{
    /// <summary>
    /// Interaction logic for DetailedBillWindow.xaml
    /// </summary>
    public partial class DetailedBillWindow : Window
    {
        public DetailedBillWindow(DetailedBillWindowVM vm)
        {
            DataContext = vm;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("asdasd");
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnResize_Click(object sender, RoutedEventArgs e)
        {

            //Window currentWindow = Application.Current.MainWindow;
            //if (!isFullScreen)
            //{

            //    currentWindow.WindowState = WindowState.Normal;
            //    currentWindow.WindowStyle = WindowStyle.None;
            //    currentWindow.ResizeMode = ResizeMode.CanResize;
            //    currentWindow.Top = 0;
            //    currentWindow.Left = 0;
            //    currentWindow.Width = SystemParameters.PrimaryScreenWidth;
            //    currentWindow.Height = SystemParameters.PrimaryScreenHeight;
            //    isFullScreen = true;
            //}
            //else
            //{
            //    currentWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //    currentWindow.Width = 800;
            //    currentWindow.Height = 600;
            //    isFullScreen = false;
            //}

            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

       

    }
}
