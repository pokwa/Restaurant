using IDataInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class DishManager : IDishManager
    {
        public Dish GetDishByDishNumber(int dishNumber)
        {
            using var context = new RestaurantContext();
            return (from d in context.Menu
                    where d.DishNumber == dishNumber
                    select d).FirstOrDefault();
        }

        public List<Dish> GetMenu()
        {
            using var context = new RestaurantContext();
            return (from d in context.Menu
                    select d).ToList();
        }

        internal Dish GetDishByDishId(int dishId)
        {
            using var context = new RestaurantContext();
            return (from d in context.Menu
                    where d.DishID == dishId
                    select d).FirstOrDefault();
        }
    }
}
