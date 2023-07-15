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
using Hotel_POS.ViewModel;
using System.Windows.Threading;
using System.Globalization;
using Hotel_POS.Components;

namespace Hotel_POS
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        //  private bool isFullScreen = false;

        public LoginWindow()
        {
            DataContext = new LoginWindowVM();
            InitializeComponent();


            //----------------------------------------------


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

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnResize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void PackIconMaterial_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void txtUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text) && textUserName.Text.Length > 0)
            {
                textUserName.Visibility = Visibility.Collapsed;
            }
            else
            {
                textUserName.Visibility = Visibility.Visible;
            }
        }

        private void textUserName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtUserName.Focus();
        }

        //private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    txtPassword.Focus();
        //}

        //private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtPassword.Text) && txtPassword.Text.Length > 0)
        //    {
        //        textPassword.Visibility = Visibility.Collapsed;
        //    }
        //    else
        //    {
        //        textPassword.Visibility = Visibility.Visible;
        //    }
        //}

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            WatermarkTextBlock.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var passwordBox = (BindablePasswordBox)sender;
            if (string.IsNullOrEmpty(passwordBox.Password))
            {
                WatermarkTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (BindablePasswordBox)sender;
            if (string.IsNullOrEmpty(passwordBox.Password))
            {
                WatermarkTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                WatermarkTextBlock.Visibility = Visibility.Collapsed;
            }
        }


    }
}

