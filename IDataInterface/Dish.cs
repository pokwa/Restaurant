using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IDataInterface
{
    public class Dish
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DishID { get; set; }
        public int DishNumber { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DishType DishType { get; set; }
    }
}
