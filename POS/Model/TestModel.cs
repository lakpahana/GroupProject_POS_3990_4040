using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_POS.Model
{
    public class TestModel
    {
        [Key]
        public int id { get; set; }
        public string ds { get; set; }

    }
}
