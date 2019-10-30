using IDataInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant
{
    public class TabDish
    {
        public TabDish(Dish dish)
        {
            Name = dish.Name;
            Price = dish.Price;
            DishNumber = dish.DishNumber;
        }
        public string Name;
        public decimal Price;
        public int DishNumber;
    }
}
