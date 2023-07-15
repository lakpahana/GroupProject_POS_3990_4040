using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
using Hotel_POS.Model;
using Hotel_POS.ViewModel;
using Hotel_POS.View;



namespace Hotel_POS
{
    /// <summary>
    /// Interaction logic for CashierWindow.xaml
    /// </summary>
    public partial class CashierWindow : Window
    {
        //private bool isFullScreen = false;
        public CashierWindow(CashierWindowVM vm)
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
            Application.Current.Shutdown();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            double value1 = 0, value2 = 0;
            if (TextBox1 != null && !string.IsNullOrEmpty(TextBox1.Text) && double.TryParse(TextBox1.Text, out value1) &&
                TextBox2 != null && !string.IsNullOrEmpty(TextBox2.Text) && double.TryParse(TextBox2.Text, out value2))
            {
                if (value1 != 0 || value2 != 0)
                {
                    TextBox3.Text = (value2 - value1).ToString();
                    TextBox1.Text = subTxt.Text;
                    TextBox_TextChanged();
                    //disCalc();


                }
            }
            else
            {
                PayBtn.IsEnabled = false;
                BillBtn.IsEnabled = false;
                TextBox3.Text = string.Empty;
            }
        }


        private void disCalc()
        {
            double discountAmtPercentage = Convert.ToDouble(disText.Text); //10%
            double priceBeforeDiscount = Convert.ToDouble(grandTxt.Text);
            double priceAfterDiscount = (priceBeforeDiscount * (100.0 - discountAmtPercentage)) / 100.0;
            subTxt.Text = priceAfterDiscount.ToString();
            TextBox1.Text = subTxt.Text;
        }


        private void TextBox_TextChanged()
        {
            //1 total payable
            //cash recived
            double value1 = 0, value2 = 0;
            if (TextBox1 != null && !string.IsNullOrEmpty(TextBox1.Text) && double.TryParse(TextBox1.Text, out value1) &&
                TextBox2 != null && !string.IsNullOrEmpty(TextBox2.Text) && double.TryParse(TextBox2.Text, out value2))
            {
                if (value1 != 0 || value2 != 0)
                {

                    if ((value2 - value1) > 0)
                    {
                        TextBox3.Text = (value2 - value1).ToString();
                        TextBox1.Text = subTxt.Text;
                        PayBtn.IsEnabled = true;
                        BillBtn.IsEnabled = true;
                    }
                    else
                    {
                        PayBtn.IsEnabled = false;
                        BillBtn.IsEnabled = false;
                        TextBox3.Text = "0";
                    }
                    //TextBox_TextChanged();

                }
            }
            else
            {
                TextBox3.Text = string.Empty;
            }
        }



        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

            if (qtyTxt != null)
            {
                disCalc();
                TextBox1.Text = subTxt.Text;
                TextBox_TextChanged();
            }


        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            double value1 = 0, value2 = 0;
            if (disText != null && !string.IsNullOrEmpty(disText.Text) && double.TryParse(disText.Text, out value1) &&
                grandTxt != null && !string.IsNullOrEmpty(grandTxt.Text) && double.TryParse(grandTxt.Text, out value2))
            {
                if (value1 != 0 || value2 != 0)
                {

                    //subTxt.Text = (value2 - value1).ToString();
                    //TextBox1.Text = subTxt.Text;
                    disCalc();

                    TextBox_TextChanged();
                    

                }
            }
            else
            {
                subTxt.Text = string.Empty;
            }
        }


    }
}
