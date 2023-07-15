using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_POS.Model;
using Hotel_POS.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Xaml;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using FontFamily = System.Windows.Media.FontFamily;
namespace Hotel_POS.ViewModel
{
    public partial class CashierWindowVM : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<BillRowItem> billRowItems = new ObservableCollection<BillRowItem>();

        ObservableCollection<BillRowItem> billRowItemsTemp = new ObservableCollection<BillRowItem>();

        [ObservableProperty]
        public ObservableCollection<Item> items = new ObservableCollection<Item>();

        [ObservableProperty]
        List<Category> itemsListCat = new List<Category>();


        [ObservableProperty]
        public ObservableCollection<Item> currentSelection = new ObservableCollection<Item>();




        public int _totalItems;

        public int TotalItems
        {
            get
            {
                return _totalItems;
            }
            set
            {
                if (_totalItems == value)
                {
                    return;
                }
                _totalItems = value;
                OnPropertyChanged(nameof(TotalItems));

            }
        }

        public int _balance;

        public int Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                if (_balance == value)
                {
                    return;
                }
                _balance = value;
                OnPropertyChanged(nameof(Balance));

            }
        }





        public double _grandTotal;
        public double GrandTotal
        {
            get
            {
                return _grandTotal;
            }

            set
            {
                if (_grandTotal == value)
                {

                    return;
                }


                _grandTotal = value;

                OnPropertyChanged(nameof(GrandTotal));
                CalcTotalItems();
            }
        }

        public double _discount;

        public double Discount
        {
            get
            {
                return _discount;
            }
            set
            {
                if (_discount == value)
                {
                    return;
                }
                _discount = value;
                OnPropertyChanged(nameof(Discount));
                OnPropertyChanged(nameof(GrandTotal));

            }
        }


        public double _subTotal;

        public double SubTotal
        {
            get
            {
                return _subTotal;
            }
            set
            {
                if (_subTotal == value)
                {
                    return;
                }
                _subTotal = value;
                OnPropertyChanged(nameof(SubTotal));
                OnPropertyChanged(nameof(GrandTotal));

            }
        }




        public double _cashRecieved;

        public double CashRecieved
        {
            get
            {
                return _cashRecieved;
            }
            set
            {
                if (_cashRecieved == value)
                {
                    return;
                }
                _cashRecieved = value;
                OnPropertyChanged(nameof(CashRecieved));
                OnPropertyChanged(nameof(GrandTotal));

            }
        }



        [ObservableProperty]
        public string billId;

        public int rowNo = 1;

        private void generateBillId()
        {
            Guid guid = Guid.NewGuid();
            billId = guid.ToString();

        }

        [RelayCommand]
        public void CatClick(object obj)
        {
            //string content = parameter as string;
            //MessageBox.Show(content);
            currentSelection.Clear();
            Category DoItem = obj as Category;
            using (var db = new DatabaseContext())
            {
                var currentList = db.Items.Where(it => it.itemCategory == DoItem.Name).ToList();
                foreach (var it in currentList)
                {
                    currentSelection.Add(it);
                }
            }

        }

        private void CalcTotalItems()
        {
            int total = 0;
            foreach (BillRowItem row in billRowItems)
            {
                total += row.Qty;
                // do something with cellValue
            }
            TotalItems = total;
        }


        [RelayCommand]
        public void Increment(object obj)
        {
            BillRowItem listItem = (BillRowItem)obj;
            //already have
            listItem.Qty += 1;
            listItem.Total = listItem.Qty * listItem.Item.itemPrice;
            //var listitem2 = listItem;


            foreach (var item in billRowItems)
            {
                billRowItemsTemp.Add(item);
            }

            billRowItems.Clear();

            foreach (var item in billRowItemsTemp)
            {
                billRowItems.Add(item);
            }

            billRowItemsTemp.Clear();



            GrandTotal += listItem.Item.itemPrice;
        }
        [RelayCommand]
        public void Decrement(object obj)
        {
            BillRowItem listItem = (BillRowItem)obj;
            if (listItem.Qty == 1)
            {
                listItem.Qty -= 1;
                listItem.Total = listItem.Qty * listItem.Item.itemPrice;
                GrandTotal -= listItem.Item.itemPrice;

                DeleteRow(listItem);
            }
            else
            {
                listItem.Qty -= 1;
                listItem.Total = listItem.Qty * listItem.Item.itemPrice;

                foreach (var item in billRowItems)
                {
                    billRowItemsTemp.Add(item);
                }
                billRowItems.Clear();
                foreach (var item in billRowItemsTemp)
                {
                    billRowItems.Add(item);
                }
                billRowItemsTemp.Clear();
                GrandTotal -= listItem.Item.itemPrice;
            }

        }


        [RelayCommand]
        public void ItemClicked(object obj)
        {
            Item clickedItem = obj as Item;

            var listItems = billRowItems.Where(i => i.Item.itemId == clickedItem.itemId);

            if (listItems.Count() == 1)
            {
                var listItem = billRowItems.Single(i => i.Item.itemId == clickedItem.itemId);


                //already have
                listItem.Qty += 1;
                listItem.Total = listItem.Qty * clickedItem.itemPrice;
                //var listitem2 = listItem;


                foreach (var item in billRowItems)
                {
                    billRowItemsTemp.Add(item);
                }

                billRowItems.Clear();

                foreach (var item in billRowItemsTemp)
                {
                    billRowItems.Add(item);
                }

                billRowItemsTemp.Clear();



                GrandTotal += clickedItem.itemPrice;


            }
            else if (listItems.Count() != 1)
            {
                //new
                billRowItems.Add(new BillRowItem(billId, rowNo, clickedItem, 1, clickedItem.itemPrice * 1, "Add comment"));
                rowNo++;
                GrandTotal += clickedItem.itemPrice * 1;

            }


        }

        [RelayCommand]
        public void CancelTransaction()
        {
            ResetAll();
        }

        private void ResetAll()
        {
            generateBillId();
            TotalItems = 0;
            GrandTotal = 0;
            Discount = 0;
            SubTotal = 0;
            CashRecieved = 0;
            Balance = 0;
            billRowItems.Clear();
            billRowItemsTemp.Clear();
            CalcTotalItems();
        }

        public CashierWindowVM()
        {

            getItems();
            ResetAll();

        }


        [RelayCommand]
        public void DeleteRow(object obj)
        {
            BillRowItem billRowItem = (BillRowItem)obj;

            GrandTotal = GrandTotal - billRowItem.Total;
            billRowItems.Remove(billRowItem);

            CalcTotalItems();


        }

        [RelayCommand]
        public void Payment()
        {
            Bill bill = new();
            bill.billId = billId;
            bill.billDate = DateTime.Now;
            bill.billData = JsonSerializer.Serialize(billRowItems);
            bill.discount = Discount;
            bill.subTotal = SubTotal;
            bill.grandTotal = GrandTotal;
            using (var db = new DatabaseContext())
            {
                db.Bills.Add(bill);
                if (db.SaveChanges() > 0)
                {
                    MessageBox.Show("Transaction Successfull!");
                    ResetAll();
                }
                else
                {
                    MessageBox.Show("Transaction Failed!");
                }
            }

        }



        private void getItems()
        {
            List<Category> listCat = new List<Category>();
            items.Clear();
            using (var db = new DatabaseContext())
            {
                var itemsList = db.Items.ToList();

                foreach (var it in itemsList)
                {
                    items.Add(it);
                    listCat.Add(new Category(it.itemCategory));
                }

                var x = listCat.Select(o => o.Name).Distinct().ToList();
                foreach (var ll in x)
                {
                    itemsListCat.Add(new Category(ll));
                }
                //MessageBox.Show(itemsListCat.Count.ToString());

            }
        }


        [RelayCommand]
        public void GeneratePrint()
        {
            FlowDocument doc = CreateFlowDocument();
            
            BillPrintViewWindow billPrintViewWindow = new BillPrintViewWindow();
            billPrintViewWindow.fdv.Document = doc;
            billPrintViewWindow.Show();
        }

        public FlowDocument CreateFlowDocument()
        {
            FlowDocument flowDocument = new FlowDocument();

            // Create paragraph for the document
            Paragraph paragraph = new Paragraph();
            Paragraph paragraph2 = new Paragraph();
            Paragraph paragraph3 = new Paragraph();
            Paragraph paragraph4 = new Paragraph();
            Paragraph paragraph5 = new Paragraph();



            // Create and set up fonts
            FontFamily fontFamily = new FontFamily("Courier New");
            FontWeight fontWeightNormal = FontWeights.Normal;
            FontWeight fontWeightBold = FontWeights.Bold;
            double fontSize9 = 9;
            double fontSize10 = 10;
            double fontSize11 = 11;
            double fontSize14 = 14;

            // Create brush for text color
            Brush brush = Brushes.Black;

            // Set layout and formatting options
            double startX = 16;
            double startY = 0;
            double Offset = 0;
            double leading = 3;
            double lineheight9 = fontSize9 + leading;
            double lineheight10 = fontSize10 + leading;
            double lineheight11 = fontSize11 + leading;
            double lineheight14 = fontSize14 + leading;

            StringFormat formatLeft = new StringFormat(StringFormatFlags.NoClip);
            StringFormat formatCenter = new StringFormat(formatLeft);
            StringFormat formatRight = new StringFormat(formatLeft);

            formatCenter.Alignment = StringAlignment.Center;
            formatRight.Alignment = StringAlignment.Far;
            formatLeft.Alignment = StringAlignment.Near;


            // Add text to the paragraph with different formatting
            paragraph.Inlines.Add(new Run("Bata Kade")
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightBold,
                FontSize = fontSize14,
                Foreground = brush,

            });

            paragraph.Inlines.Add(new LineBreak());
            Offset += lineheight14 + 6;

            paragraph.Inlines.Add(new Run("No 10, Wakwella Road,")
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize10,
                Foreground = brush,

            });

            paragraph.Inlines.Add(new LineBreak());
            Offset += lineheight10;

            paragraph.Inlines.Add(new Run("Hapugala, Wakewella.")
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize10,
                Foreground = brush,

            });

            paragraph.Inlines.Add(new LineBreak());
            Offset += lineheight10;

            paragraph.Inlines.Add(new Run("Tel: 091 7294265")
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize10,
                Foreground = brush,

            });

            paragraph.Inlines.Add(new LineBreak());
            Offset += lineheight10 - 2;

            paragraph.Inlines.Add(new Run("".PadRight(40, '='))
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize11,
                Foreground = brush,

            });




            Offset += lineheight10;



            string datetime = DateTime.Now.ToString();
            paragraph2.Inlines.Add(new Run("Date: " + datetime)
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize10,
                Foreground = brush,

            });

            paragraph2.Inlines.Add(new Run("     *PAID*")
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightBold,
                FontSize = fontSize14,
                Foreground = brush,

            });

            paragraph2.Inlines.Add(new LineBreak());
            Offset += lineheight10;

            string transno = billId;
            paragraph2.Inlines.Add(new Run("Bill No: " + transno)
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize10,
                Foreground = brush,

            });

            paragraph2.Inlines.Add(new LineBreak());
            Offset += lineheight10;

            string payment_type = "Dining";
            paragraph2.Inlines.Add(new Run("Payment: " + payment_type)
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize10,
                Foreground = brush,

            });

            paragraph2.Inlines.Add(new LineBreak());
            Offset += lineheight10;

            string user = "John Doe";
            paragraph2.Inlines.Add(new Run("Cashier: " + Username)
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize10,
                Foreground = brush,

            });

            paragraph2.Inlines.Add(new LineBreak());
            Offset += lineheight10 - 2;


            paragraph2.Inlines.Add(new Run("".PadRight(40, '='))
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize11,
                Foreground = brush,

            });

            paragraph2.Inlines.Add(new LineBreak());
            Offset += lineheight11 + 10;

            int i = 0;
            foreach (BillRowItem a in billRowItems)
            {
                paragraph3.Inlines.Add(new Run($"{a.Qty} x {a.Item.itemName} - LKR : {a.Total}")
                {
                    FontFamily = fontFamily,
                    FontWeight = fontWeightNormal,
                    FontSize = fontSize11,
                    Foreground = brush,

                });



                if (!string.IsNullOrWhiteSpace(a.Comment) && a.Comment != "Add comment")
                {
                    string[] req = a.Comment.Split(',');
                    foreach (string r in req)
                    {
                        Offset += lineheight9 - 2;
                        paragraph3.Inlines.Add(new Run("-" + r)
                        {
                            FontFamily = fontFamily,
                            FontWeight = fontWeightNormal,
                            FontSize = fontSize10,
                            Foreground = brush,

                        });

                    }
                }
                paragraph3.Inlines.Add(new LineBreak());


            }


            Offset += lineheight10 - 10;
            paragraph4.Inlines.Add(new Run("".PadRight(40, '-'))
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize11,
                Foreground = brush,

            });

            Offset += lineheight11 + 10;
            paragraph4.Inlines.Add(new LineBreak());

            paragraph4.Inlines.Add(new Run("Sub Total       ")
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize11,
                Foreground = brush,

            });

            double fulltotal = Convert.ToDouble(SubTotal);
            double discount = Convert.ToDouble(Discount);


            paragraph4.Inlines.Add(new Run(((fulltotal / (100 - discount)) * 100).ToString("0.00"))
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize11,
                Foreground = brush,

            });

            paragraph4.Inlines.Add(new LineBreak());

            Offset += lineheight11;

            paragraph4.Inlines.Add(new Run("Discount: " + discount + "% ")
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize11,
                Foreground = brush,

            });

            paragraph4.Inlines.Add(new Run("-" + ((fulltotal / (100 - discount)) * discount).ToString("0.00"))
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize11,
                Foreground = brush,

            });

            Offset += lineheight11 + 10;
            paragraph4.Inlines.Add(new LineBreak());

            paragraph4.Inlines.Add(new Run("Total       ")
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightBold,
                FontSize = fontSize11,
                Foreground = brush,

            });

            paragraph4.Inlines.Add(new Run(fulltotal.ToString("0.00"))
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightBold,
                FontSize = fontSize11,
                Foreground = brush,

            });
            paragraph4.Inlines.Add(new LineBreak());

            Offset += lineheight10 - 4;
            paragraph4.Inlines.Add(new Run("".PadRight(25, '-'))
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize11,
                Foreground = brush,

            });

            Offset += lineheight11 + 10;
            paragraph4.Inlines.Add(new LineBreak());

            paragraph4.Inlines.Add(new Run("Paid Amount  ")
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize11,
                Foreground = brush,

            });

            paragraph4.Inlines.Add(new Run(CashRecieved.ToString())
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize11,
                Foreground = brush,

            });

            Offset += lineheight11;
            paragraph4.Inlines.Add(new LineBreak());

            paragraph4.Inlines.Add(new Run("Change  ")
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize11,
                Foreground = brush,

            });

            paragraph4.Inlines.Add(new Run(Balance.ToString())
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize11,
                Foreground = brush,

            });

            paragraph4.Inlines.Add(new LineBreak());
            Offset = Offset + lineheight10 + 10;
            paragraph4.Inlines.Add(new Run("".PadRight(40, '='))
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize11,
                Foreground = brush,

            });

            Offset += lineheight11;
            paragraph4.Inlines.Add(new LineBreak());

            paragraph5.Inlines.Add(new Run("Please retain this receipt for any")
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize10,
                Foreground = brush,

            });

            paragraph5.Inlines.Add(new LineBreak());
            Offset += lineheight10;

            paragraph5.Inlines.Add(new Run("complaints and exchanges.")
            {
                FontFamily = fontFamily,
                FontWeight = fontWeightNormal,
                FontSize = fontSize10,
                Foreground = brush,

            });
            paragraph.TextAlignment = TextAlignment.Center;
            paragraph2.TextAlignment = TextAlignment.Left;
            paragraph3.TextAlignment = TextAlignment.Justify;
            paragraph4.TextAlignment = TextAlignment.Right;
            paragraph5.TextAlignment = TextAlignment.Center;

            paragraph.Margin = new Thickness(0);
            paragraph.LineHeight = 1;
            paragraph2.LineHeight = 1;

            paragraph2.Margin = new Thickness(0);
            paragraph3.Margin = new Thickness(0);
            paragraph3.LineHeight = 1;
            paragraph4.LineHeight = 1;
            paragraph4.Margin = new Thickness(0);
            paragraph5.LineHeight = 1;
            paragraph5.Margin = new Thickness(0);



            flowDocument.Blocks.Add(paragraph);
            flowDocument.Blocks.Add(paragraph2);
            flowDocument.Blocks.Add(paragraph3);
            flowDocument.Blocks.Add(paragraph4);
            flowDocument.Blocks.Add(paragraph5);

            //r,t,l,b
            flowDocument.PagePadding = new Thickness(20, 0, 20, 0);

            return flowDocument;
        }








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

        //----------------------------------------------------------------------

        public int userID;

        public double UserID
        {
            get
            {
                return userID;
            }
            set
            {
                if (userID == value)
                {
                    return;
                }
                userID = (int)value;
                OnPropertyChanged(nameof(userID));

            }
        }

        public string _cashierID;
        public string CashierID1
        {
            get
            {
                return _cashierID;
            }
            set
            {
                if (_cashierID == value)
                {
                    return;
                }
                _cashierID = value;
                OnPropertyChanged(nameof(CashierID1));

            }
        }
        



        public string _username;
        
        // The username of the cashier.
        
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username == value)
                    return;

                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        //// The username of the photo
        public string _cashierPhoto;

        public string CashierPhoto
        {
            get { return _cashierPhoto; }
            set
            {
                if (_cashierPhoto == value)
                    return;

                _cashierPhoto = value;
                OnPropertyChanged(nameof(CashierPhoto));
            }
        }

        //------------------------------------------------------------------------
        //public string _loggedCashierId;


        //public string LoggedCashierID
        //{
        //    get { return _loggedCashierId; }
        //    set
        //    {
        //        if (_loggedCashierId == value)
        //            return;

        //        _loggedCashierId = value;
        //        OnPropertyChanged(nameof(LoggedCashierID));
        //    }

        //}



        public void GetUserDetails()
        {
            using(var db = new DatabaseContext())
            {
                User user = db.Users.FirstOrDefault(user=>user.UserId==userID);
                Username = user.Name;
                CashierPhoto = user.ProfilePicture;
                CashierID1 = user.CashierId;
              //  MessageBox.Show(Username);
            }
        }

    
    }

}
