using IDataInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant
{
    public class StatisticsAPI
    {
        IOrderManager orderManager;
        IReservationManager reservationManager;

        public StatisticsAPI(IDishManager dishManager, 
            IOrderManager orderManager, 
            IReservationManager reservationManager)
        {
            this.orderManager = orderManager;
            this.reservationManager = reservationManager;
        }

        public decimal GetProjectedIncomeUntil(DateTime endDate)
        {
            var reservations = reservationManager.GetNumberOfReservationsUntil(endDate);
            var averageIncome = GetAverageIncome();
            return reservations.Count * averageIncome;
        }

        public decimal GetAverageIncome()
        {
            var orders = orderManager.GetAllClosedOrders();
            if (orders.Count == 0)
                return 0;
            return orders.Sum(o => o.Items.Sum(i => i.Price))/orders.Count;
        }
    }
}
