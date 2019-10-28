using IDataInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class ReservationManager
    {
        public TimeSlot FindOpenTable(int numberOfPeople, DateTime start)
        {
            using var context = new RestaurantContext();
            var tables = (from t in context.Tables.Include(t => t.Chairs)
                          where t.Chairs.Count >= numberOfPeople
                          select t);
            var timeSlots = (from s in context.TimeSlots
                             where tables.Any(t => 
                                s.Reservations.All(r => r.TableID != t.TableID))
                                && s.Start >= start
                             orderby s.Start
                             select s);
            return timeSlots.FirstOrDefault();
        }
    }
}
