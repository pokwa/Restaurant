using IDataInterface;
using System.Linq;
using System.Collections.Generic;
using System.Text;

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
                    select t).FirstOrDefault();
        }
    }
}
