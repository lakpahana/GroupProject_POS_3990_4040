using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_POS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Hotel_POS.ViewModel
{


    public partial class DetailedBillWindowVM: ObservableObject
    {
        [ObservableProperty]
        public Bill selectedBill  = new Bill();

        [ObservableProperty]
        ObservableCollection<BillRowItem> billRowItems = new ObservableCollection<BillRowItem>();

        [ObservableProperty]
        public double grandTotal;

        [ObservableProperty]
        public double subTotal;

        [ObservableProperty]
        public double disc;

        [ObservableProperty]
        public int totalItems;


        [RelayCommand]
        public void Check()
        {
            MessageBox.Show(selectedBill.billId);
        }

        public void GenerateBillItems()
        {
            string JsonbillItems = selectedBill.billData;
            billRowItems = JsonConvert.DeserializeObject<ObservableCollection<BillRowItem>>(JsonbillItems);
            TotalItems = CalcTotalItems();

            Disc = selectedBill.discount;
            GrandTotal = selectedBill.grandTotal;
            SubTotal = selectedBill.subTotal;

        }

        //ObservableCollection<BillRowItem> myCollection = new ObservableCollection<BillRowItem>(characterList.characters);
        private int CalcTotalItems()
        {
            int total = 0;
            foreach (BillRowItem row in billRowItems)
            {
                total += row.Qty;
                // do something with cellValue
            }
            return total;
        }


    }
}
