using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using Hotel_POS.Model;

namespace Hotel_POS.ViewModel
{
    public partial class AddEditItemWindowVM:ObservableObject
    {
        [ObservableProperty]
        public int kkp;
        [ObservableProperty]
        public int itemId;
        [ObservableProperty]
        public string itemName;
        [ObservableProperty]
        public int itemPrice;
        [ObservableProperty]
        public string itemCategory;

        //[ObservableProperty]
        public BitmapImage selectedImage;
        
        public bool neww { get; set; }
        public Item SelectedItem { get; set; }

        public AddEditItemWindowVM(bool isNew)
        {
            neww = isNew;            
        }

        public AddEditItemWindowVM(bool isNew2,Item selected)
        {
            neww = isNew2;
            if(SelectedItem == null)
            {
                //MessageBox.Show("2");

                Item newItem = new Item(selected.itemId,selected.itemName,selected.itemPrice,selected.itemCategory,selected.itemPhoto);
                SelectedItem = newItem;
                ItemName = newItem.itemName;
                ItemPrice = newItem.itemPrice;
                ItemCategory = newItem.itemCategory;
                Img = newItem.itemPhoto;

               

                Kkp = 55;
                //MessageBox.Show(itemName);
            }
            else
            {
                MessageBox.Show("Not selected");
            }

        }

        public AddEditItemWindowVM()
        {
        }

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

        //private void openPreviousWindow()
        //{
        //    ManageItemsWindow manageCashiersWindow = new ManageItemsWindow();
        //    manageCashiersWindow.Show();
        //    closeCurrentWindow();

        //}


        private void closeCurrentWindow()
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }

        [RelayCommand]
         public void Save()
        {
            if (string.IsNullOrEmpty(itemName) || itemPrice == 0 || string.IsNullOrEmpty(itemCategory) || string.IsNullOrEmpty(Img))
            {
                MessageBox.Show("Please fill in all required fields and upload a photo.");
                return;
            }

            
                if (SelectedItem == null)
                     {
                using(var db = new  DatabaseContext())
                {
                    Item newItem = new Item(ItemName, ItemPrice, ItemCategory, Img);
                    db.Items.Add(newItem);
                    db.SaveChanges();
                    MessageBox.Show("Item Added Succesfully.");
                    closeCurrentWindow();
                    //openPreviousWindow();
                }
            }
            else
            {
                var usernew = new User(itemName, itemPrice, itemCategory, Img);

                using (var db = new DatabaseContext())
                {
                    var item = db.Items.SingleOrDefault(x => x.itemId == SelectedItem.itemId);
                    item.itemName = ItemName;
                    item.itemPrice = ItemPrice;
                    item.itemCategory = ItemCategory;
                    item.itemPhoto = Img;
                    db.SaveChanges();
                    MessageBox.Show("Item Saved Successfully.");
                    closeCurrentWindow();

                    //openPreviousWindow();
                }
            }
        }
    }
}
