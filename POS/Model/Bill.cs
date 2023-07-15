using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Hotel_POS.Model
{
    public class Bill
    {
        public string billId { get; set; }
        public DateTime billDate { get; set; }
        public string billData { get; set; }

        public double discount { get; set; }

        public double grandTotal { get; set; }

        public double subTotal { get; set; }




        public Bill() { }

        public Bill(string billId, DateTime billDate, string billData, double discount, double grandTotal, double subTotal)
        {
            this.billId = billId;
            this.billDate = billDate;
            this.billData = billData;
            this.discount = discount;
            this.grandTotal = grandTotal;
            this.subTotal = subTotal;
        }
    }
}
