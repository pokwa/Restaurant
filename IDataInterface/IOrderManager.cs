using System;
using System.Collections.Generic;
using System.Text;

namespace IDataInterface
{
    public interface IOrderManager
    {
        public void AddToOrder(int orderId, int dishId);
        Order GetActiveOrder(int tableID);
        Order CreateActiveOrder(int tableID);
        void CloseOrder(int orderID);
        List<Order> GetAllClosedOrders();
    }
}
