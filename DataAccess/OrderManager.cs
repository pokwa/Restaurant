using IDataInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class OrderManager : IOrderManager
    {
        public void AddToOrder(int orderId, int dishId)
        {
            using var context = new RestaurantContext();
            var order = (from o in context.Orders
                         where o.OrderID == orderId
                         select o).First();
            var dishManager = new DishManager();
            var dish = dishManager.GetDishByDishId(dishId);

            order.Items.Add(dish);
            context.SaveChanges();
        }

        public void CloseOrder(int orderID)
        {
            using var context = new RestaurantContext();
            var order = (from o in context.Orders
                    where o.OrderID == orderID
                    select o)
                    .FirstOrDefault();
            order.IsActive = false;
            context.SaveChanges();
        }

        public Order CreateActiveOrder(int tableID)
        {
            using var context = new RestaurantContext();
            var order = new Order();
            order.IsActive = true;
            order.TableID = tableID;
            context.Orders.Add(order);
            context.SaveChanges();
            return order;
        }

        public Order GetActiveOrder(int tableID)
        {
            using var context = new RestaurantContext();
            return (from o in context.Orders
                    where o.TableID == tableID && o.IsActive
                    select o)
                    .Include(o => o.Items)
                    .FirstOrDefault();
        }
    }
}
