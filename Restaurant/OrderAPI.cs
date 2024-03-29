﻿using DataAccess;
using IDataInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant
{
    public class OrderAPI
    {
        IDishManager dishManager;
        IOrderManager orderManager;
        ITableManager tableManager;

        public OrderAPI(IDishManager dishManager, IOrderManager orderManager, ITableManager tableManager)
        {
            this.dishManager = dishManager;
            this.orderManager = orderManager;
            this.tableManager = tableManager;
        }
        public List<Dish> GetMenu()
        {
            return dishManager.GetMenu();
        }

        public OrderErrorCodes AddToOrder(int dishNumber, int tableNumber)
        {
            var dish = dishManager.GetDishByDishNumber(dishNumber);
            if (dish == null)
                return OrderErrorCodes.NoSuchDish;
            var table = tableManager.GetTableByTableNumber(tableNumber);
            if (table == null)
                return OrderErrorCodes.NoSuchTable;
            var order = orderManager.GetActiveOrder(table.TableID);
            if (order == null)
                order = orderManager.CreateActiveOrder(table.TableID);
            orderManager.AddToOrder(order.OrderID, dish.DishID);
            return OrderErrorCodes.Ok;
        }

        public Tab GetTab(int tableNumber)
        {
            var table = tableManager.GetTableByTableNumber(tableNumber);
            if (table == null)
                return new Tab { TabStatus = Tab.Status.NoSuchTable };
            var order = orderManager.GetActiveOrder(table.TableID);
            if (IsTabSettled(order))
                return new Tab { TabStatus = Tab.Status.Settled };
            Tab tab = GetTabAfterErrorChecks(order);
            return tab;
        }

        private static Tab GetTabAfterErrorChecks(Order order)
        {
            var tab = new Tab();
            AddDishesToTab(order, tab);
            tab.TabStatus = GetTabStatusFromTabAmount(tab);
            return tab;
        }

        private static Tab.Status GetTabStatusFromTabAmount(Tab tab)
        {
            return tab.Amount > 0 ? Tab.Status.Open : Tab.Status.Settled;
        }

        private static bool IsTabSettled(Order order)
        {
            return order == null || !order.IsActive;
        }

        private static void AddDishesToTab(Order order, Tab tab)
        {
            tab.Dishes = new List<TabDish>();
            foreach (var item in order.Items)
            {
                tab.Amount += item.Price;
                tab.Dishes.Add(new TabDish(item));
            }
        }

        public PayTabErrorCodes PayTab(int tableNumber, decimal payedAmount)
        {
            var tab = GetTab(tableNumber);
            if (tab.TabStatus == Tab.Status.NoSuchTable)
                return PayTabErrorCodes.NoSuchTable;
            if (tab.TabStatus == Tab.Status.Settled)
                return PayTabErrorCodes.NoOpenTab;
            if (tab.Amount != payedAmount)
                return PayTabErrorCodes.WrongAmount;
            var table = tableManager.GetTableByTableNumber(tableNumber);
            var order = orderManager.GetActiveOrder(table.TableID);
            orderManager.CloseOrder(order.OrderID);
            return PayTabErrorCodes.Ok;
        }
    }
}
