using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_POS.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Hotel_POS.ViewModel
{
    public partial class AdminWindowViewModel : ObservableObject
    {
        
        
        [RelayCommand]
        public void Logout()
        {
            LoginWindow mainLg = new LoginWindow();
            mainLg.Show();
            CloseCurrentWindow();     
        }


        public void CloseCurrentWindow()
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }
    }
}