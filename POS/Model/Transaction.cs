using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_POS.Model
{
    public class Transaction
    {
        [Key]
        public int billId { get; set; }
        public string billDateTime { get; set; }
        public List<BillRowItem> billItems { get; set; }
        public double billTotal { get; set; }

        public Transaction
            (
                int billId, 
                string billDateTime, 
                List<BillRowItem> billItems, 
                double billTotal
            )
        {
            this.billId = billId;
            this.billDateTime = billDateTime;
            this.billItems = billItems;
            this.billTotal = billTotal;
        }


    }
}
