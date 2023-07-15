using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_POS.Model;
using Hotel_POS.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_POS.ViewModel
{
    public partial class ShowTransactionsVM : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<Bill> billsList = new ObservableCollection<Bill>();

        public ShowTransactionsVM()
        {
            using (var db = new DatabaseContext())
            {
                var itemsList = db.Bills.ToList();

                foreach (var it in itemsList)
                {
                    billsList.Add(it);
                }
            }
        }

        [RelayCommand]
        public void ShowItemList(object obj)
        {
            Bill bill = (Bill)obj;
            DetailedBillWindowVM vm = new DetailedBillWindowVM();
            vm.selectedBill = bill;
            vm.GenerateBillItems();
            DetailedBillWindow detailedBillWindow = new DetailedBillWindow(vm);
            detailedBillWindow.Show();


        }
    }
}
