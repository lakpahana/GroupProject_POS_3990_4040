using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_POS.Model;
using Hotel_POS.View;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hotel_POS.ViewModel
{
    public partial class LoginWindowVM:ObservableObject
    {
        [ObservableProperty]
        public string username;
        [ObservableProperty]
        public string password;

        public string LoggedCashierName;
        public int LoggedCashierID;


        [RelayCommand]

        public void Login()
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter the username and Password");
                return;
            }

            using (var db = new DatabaseContext())
            {
                bool userFound = db.Users.Any(user => user.Name.ToUpper() == username.ToUpper() && user.Password == password);
                

                if (userFound)
                {
                    LoggedCashierName = username;
                   



                    bool accessAdmin = db.Users.Any(user => user.Name.ToUpper() == username.ToUpper() && user.IsAdmin == 1);
                    if (accessAdmin)
                    {

                        AdminWindowView main = new AdminWindowView();
                        main.Show();
                        CloseCurrentWindow();
                    }
                    else
                    {
                        CashierWindowVM cashierWindowVM = new CashierWindowVM();
                        cashierWindowVM.userID = db.Users.FirstOrDefault(user => user.Name.ToUpper() == username.ToUpper() && user.Password == password).UserId;
                        cashierWindowVM.GetUserDetails();
                        CashierWindow main = new CashierWindow(cashierWindowVM);

                        main.Show();
                        CloseCurrentWindow();
                    }
                }            
               else
                {
                    MessageBox.Show("Please check username and password");

                }
            }
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
