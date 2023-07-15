using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel_POS.Model;
using System.Windows;
using CommunityToolkit.Mvvm.Input;

namespace Hotel_POS.ViewModel
{
    public partial class ManageCashiersWindowVM:ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<User> users = new ObservableCollection<User>();

        [ObservableProperty]
        public User selectedUser = null;
     


        [RelayCommand]
        public void AddNewRecord()
        {

            if (selectedUser == null)
            {
                AddEditWindow addEditWindow = new AddEditWindow();
                addEditWindow.ShowDialog();
                LoadUsers();
            }
            else
            {
                //edit
                AddEditWindow addEditWindow2 = new AddEditWindow(selectedUser);
                addEditWindow2.ShowDialog();
                LoadUsers();
            }
        }

        [RelayCommand]

        public void Delete() 
        {
            if(selectedUser != null)
            {
                using(var db = new DatabaseContext())
                {
                    db.Users.Remove(selectedUser);
                    db.SaveChanges();
                    users.Clear();
                    LoadUsers();
                }
            }
            else
            {
                MessageBox.Show("Select a user before continue");
            }
        }


        private void LoadUsers()
        {
            users.Clear();

            //users = new ObservableCollection<User>();
            using (var db = new DatabaseContext())
            {
                var cashiers = db.Users
                .Where(b => b.IsAdmin == 0)
                .ToList();

                foreach (var cashier in cashiers)
                {
                    users.Add(cashier);
                }
            }
        }

        public ManageCashiersWindowVM()
        {
            LoadUsers();
        }
    }
}
