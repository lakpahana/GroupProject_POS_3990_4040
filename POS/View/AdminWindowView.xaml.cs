using Hotel_POS.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Hotel_POS.View
{
    /// <summary>
    /// Interaction logic for AdminWindowView.xaml
    /// </summary>
    public partial class AdminWindowView : Window
    {
        public AdminWindowView()
        {
            DataContext = new AdminWindowViewModel();
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            string timeText = dateTime.ToString("h:mm:ss tt", CultureInfo.InvariantCulture);
            clockTextBlock.Text = timeText;
        }

        //Navigation

        private void ManageCashierCheck(object sender, RoutedEventArgs e)
        {
            NavigationControl.Content = new ManageCashierWindowView();
        }

        private void ManageItemCheck(object sender, RoutedEventArgs e)
        {
            NavigationControl.Content = new ManageItemWindowView();
        }

        private void ShowTranstractionChecked(object sender, RoutedEventArgs e)
        {
            NavigationControl.Content = new ShowTranstractionView();
        }

        //Controlling window

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }
        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

    }
}
