using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_POS.Model
{
    public class User
    {
        private string itemName;
        private int itemPrice;
        private string itemCategory;
        private string img;

        public int UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int IsAdmin { get; set; }
        public string Password { get; set; }
        public string CashierId { get; set; }
        public string ProfilePicture { get; set; }
        public User() { 
            
        }

        public User(string name, string phoneNumber, int isAdmin, string password, string cashierId, string profilePicture)
        {
            //UserId = userId;
            Name = name;
            PhoneNumber = phoneNumber;
            IsAdmin = isAdmin;
            Password = password;
            CashierId = cashierId;
            ProfilePicture = profilePicture;
        }

        public User(string itemName, int itemPrice, string itemCategory, string img)
        {
            this.itemName = itemName;
            this.itemPrice = itemPrice;
            this.itemCategory = itemCategory;
            this.img = img;
        }
    }
}
