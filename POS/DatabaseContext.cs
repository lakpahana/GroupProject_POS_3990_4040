using Hotel_POS.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
namespace Hotel_POS
{
    public class DatabaseContext : DbContext
    {
        public static readonly string workingDirectory = Directory.GetCurrentDirectory();
        public string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("Data Source=" +
          Path.Combine(projectDirectory, "Database.db"));

        

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<TestModel> TM { get; set; }
        public object Categories { get; internal set; }
        //public DbSet<BillRowItem> BillRowItems { get; set; }

        //public DbSet<Transaction> Transactions { get; set; }
    }
}
