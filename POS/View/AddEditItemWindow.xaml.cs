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
using Hotel_POS.Model;
using System.Globalization;
using System.Windows.Threading;

namespace Hotel_POS
{
    /// <summary>
    /// Interaction logic for AddEditItemWindow.xaml
    /// </summary>
    public partial class AddEditItemWindow : Window
    {
        public AddEditItemWindow(bool isNew)
        {
            // MessageBox.Show("addw new");
            DataContext = new AddEditItemWindowVM(isNew);
            InitializeComponent();
            //time
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

        }

        public AddEditItemWindow(bool isNew, Item selectedItem)
        {
            // MessageBox.Show("editw new");

            DataContext = new AddEditItemWindowVM(isNew, selectedItem);
            InitializeComponent();
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
