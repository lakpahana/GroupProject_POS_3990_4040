using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_POS.Model
{
    public class BillRowItem
    {

        public string BillID { get; set; }
        public int RowNo { get; set; }
        public Item Item { get; set; }
        public int Qty { get; set; }
        public double Total { get; set; }
        public string Comment { get; set; }

        public BillRowItem(string billID, int rowNo, Item item, int qty, double total, string comment)
        {
            BillID = billID;
            RowNo = rowNo;
            Item = item;
            Qty = qty;
            Total = total;
            Comment = comment;
        }

    }
}