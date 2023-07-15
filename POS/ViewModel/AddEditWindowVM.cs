using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_POS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace Hotel_POS.ViewModel
{
    public partial class AddEditWindowVM:ObservableObject
    {
        [ObservableProperty]
        public string username;
        [ObservableProperty]
        public string password;
        [ObservableProperty]
        public string phonenumber;
        [ObservableProperty]
        public string cashierid;


        public User selectedUser;

        public BitmapImage selectedImage;

        public string _img;
        public string Img
        {
            get
            {
                return _img;
            }

            set
            {
                if (_img == value)
                {

                    return;
                }


                _img = value;

                OnPropertyChanged(nameof(Img));

            }
        }

        


        public AddEditWindowVM(User sel)
        {
            
            selectedUser = sel;
            username = selectedUser.Name;
            password = selectedUser.Password;
            phonenumber = selectedUser.PhoneNumber;
            cashierid = selectedUser.CashierId;
        }

        public AddEditWindowVM()
        {
            selectedUser = null;
        }
        [RelayCommand]
        public void UploadPhoto()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.bmp; *.png; *.jpg";
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == true)
            {
                selectedImage = new BitmapImage(new Uri(dialog.FileName));
                Img = dialog.FileName;

                MessageBox.Show("Image successfuly uploded!", "Successfull");
            }
        }

        [RelayCommand]
        public void Save()
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(phonenumber) || string.IsNullOrEmpty(cashierid))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            if (selectedUser != null)
            {
                //edit
                int studentID = selectedUser.UserId;
                
                if (studentID > 0)
                {
                    using(var db= new DatabaseContext())
                    {
                        var item = db.Users.SingleOrDefault(x => x.UserId == studentID);

                        var current = db.Users.Find(studentID);
                        current.Name = username;
                        current.Password = password;
                        current.PhoneNumber = phonenumber;
                        current.CashierId = cashierid;
                        current.ProfilePicture = Img;
                        db.SaveChanges();
                        MessageBox.Show("Successfully Changed");
                    }
                }
            }
            else
            {
                var usernew = new User(Username, Phonenumber, 0, Password, Cashierid, Img);
                using (var db = new DatabaseContext())
                {
                    //db.TM.Add(new TestModel(6, "dfgasdasddfg",0));
                    db.Users.Add(usernew);
                    db.SaveChanges();
                    MessageBox.Show("Successfully Adeded");


                }
            }
            
        }

      

        
    }
}
