using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_POS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xaml;

namespace Hotel_POS.ViewModel
{   
    public partial class ManageItemsVM:ObservableObject
    {

        //all items.
        [ObservableProperty]
        ObservableCollection<Item> items = new ObservableCollection<Item>();
        
        //Unique category list
        [ObservableProperty]
        ObservableCollection<Category> itemsListCat = new ObservableCollection<Category>();


        //selected category as item.
        [ObservableProperty]
        public Item selectedItem = null;


        //selected item from listview from currentslection
        [ObservableProperty]
        public Item selectedOne;



        //currently selected category.
        public object currentCat = null;


        //items for currently selected category

        [ObservableProperty]
        ObservableCollection<Item> currentSelection = new ObservableCollection<Item>();
        //Uniqye           

        [RelayCommand]
        public void CatClick(object obj)
        {           

            currentSelection.Clear();
            currentCat = obj;
            Category DoItem = obj as Category;
            

            using(var db = new DatabaseContext())
            {
                var currentList = db.Items.Where(it => it.itemCategory == DoItem.Name).ToList();

                //currentSelection = new ObservableCollection<Item>(currentList);

                foreach (var it in currentList)
                {
                    currentSelection.Add(it);
                }
            }

        }
        

        //item clicked in items
        [RelayCommand]
        public void ItemClicked(object obj)
        {

            Item DoItem = obj as Item;
            selectedItem = DoItem;

        }


        public ManageItemsVM()
        {
            fetchCategories();
        }


        [RelayCommand]
        public void AddItem()
        {
            AddEditItemWindow addEditItemWindow = new AddEditItemWindow(true);
            addEditItemWindow.ShowDialog();
            if (currentCat != null)
            {
            CatClick(currentCat);

            }
            fetchCategories();
            
            //closeCurrentWindow();

        }

        [RelayCommand]
        public void EditItem()
        {
            if (selectedOne != null)
            {
                AddEditItemWindow addEditItemWindow1 = new AddEditItemWindow(false,selectedOne);
                //addEditItemWindow1.Show();
                addEditItemWindow1.ShowDialog();
                
                
                CatClick(currentCat);
                //closeCurrentWindow();
            }
            else
            {
                MessageBox.Show("Please select an Item to Edit", "Error");
            }
        }


        [RelayCommand]
        public void DeleteItem()
        {
            if (selectedOne == null)
            {
                MessageBox.Show("Please select an item to delete.");
            }
            else
            {
                if (MessageBox.Show("Are You Sure to Delete the Item?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                }
                else
                {
                    using (var db = new DatabaseContext())
                    {
                        var item = db.Items.SingleOrDefault(x => x.itemId == selectedOne.itemId);
                        db.Items.Remove(item);
                        
                        db.SaveChanges();
                    }
                    selectedOne = null;
                    currentSelection.Clear();
                    
                    fetchCategories();

                    CatClick(currentCat);
                }
            }
        }
        //private void closeCurrentWindow()
        //{
        //    foreach (Window item in Application.Current.Windows)
        //    {
        //        if (item.DataContext == this) item.Close();
        //    }
        //}

        private void updateEditedItem()
        {

        }
        


        private void fetchCategories()
        {           
            itemsListCat.Clear();           
            using(var db = new DatabaseContext())
            {
                //var itemsList = db.Items.ToList();

                //foreach (var it in itemsList)
                //{
                //    items.Add(it);
                //    listCat.Add(new Category(it.itemCategory));
                //}

                var uniqueCategories = db.Items.Select(item => item.itemCategory).Distinct().ToList();
                foreach (var item in uniqueCategories)
                {
                    itemsListCat.Add(new Category(item));
                }


                //var x = listCat.Select(o => o.Name).Distinct().ToList();
                //foreach(var ll in x)
                //{
                //    itemsListCat.Add(new Category(ll));
                //}



                //MessageBox.Show(itemsListCat.Count.ToString());


            }
        }


     

        

     
    }
}
