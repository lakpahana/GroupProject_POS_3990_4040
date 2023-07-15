using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_POS.Model
{
    public class Item
    {
        public int itemId { get; set; }
        public string itemName { get; set; }
        public int itemPrice { get; set; }

        public string itemCategory { get; set; }

        public string itemPhoto { get; set; }



        public Item() { }
        public Item(int itemId, string itemName, int itemPrice, string itemCategory, string itemPhoto)
        {
            this.itemId = itemId;
            this.itemName = itemName;
            this.itemPrice = itemPrice;
            this.itemCategory = itemCategory;
            this.itemPhoto = itemPhoto;
        }
        public Item(string itemName, int itemPrice, string itemCategory, string itemPhoto)
        {

            this.itemName = itemName;
            this.itemPrice = itemPrice;
            this.itemCategory = itemCategory;
            this.itemPhoto = itemPhoto;
        }

    }
}
