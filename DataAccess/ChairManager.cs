using System.Linq;
using System.Collections.Generic;
using System.Text;
using IDataInterface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ChairManager : IChairManager
    {
        public Chair GetChairByChairNumber(int chairNumber)
        {
            using var context = new RestaurantContext();
            return (from c in context.Chairs
                         where c.ChairNumber == chairNumber
                         select c)
                         .Include(c => c.Table)
                         .FirstOrDefault();
        }

        public void MoveChair(int chairID, int tableID)
        {
            using var context = new RestaurantContext();
            var chair = (from c in context.Chairs
                         where c.ChairID == chairID
                         select c)
                         .First();
            chair.TableID = tableID;
            context.SaveChanges();
        }
    }
}
