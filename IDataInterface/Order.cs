using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IDataInterface
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        public int TableID { get; set; }
        public Table Table { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Dish> Items { get; set; }
    }
}
