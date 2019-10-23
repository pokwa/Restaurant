using IDataInterface;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class TableManager : ITableManager
    {
        public void AddTable(int tableNumber)
        {
            using var context = new RestaurantContext();
            var table = new Table();
            table.TableNumber = tableNumber;
            context.Tables.Add(table);
            context.SaveChanges();
        }

        public Table GetTableByTableNumber(int tableNumber)
        {
            using var context = new RestaurantContext();
            return (from t in context.Tables
                    where t.TableNumber == tableNumber
                    select t)
                    .Include(t => t.Chairs)
                    .FirstOrDefault();
        }

        public void RemoveTable(int tableID)
        {
            using var context = new RestaurantContext();
            var table = (from t in context.Tables
                         where t.TableID == tableID
                         select t).FirstOrDefault();
            context.Tables.Remove(table);
            context.SaveChanges();
        }
    }
}
