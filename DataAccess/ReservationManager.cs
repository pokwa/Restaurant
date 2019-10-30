using IDataInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class ReservationManager : IReservationManager
    {
        public TimeSlot FindOpenTable(int numberOfPeople, DateTime start)
        {
            using var context = new RestaurantContext();
            var tables = (from t in context.Tables.Include(t => t.Chairs)
                          where t.Chairs.Count >= numberOfPeople
                          && !t.Deleted
                          select t);
            var timeSlots = (from s in context.TimeSlots
                             where tables.Any(t => 
                                s.Reservations.All(r => r.TableID != t.TableID))
                                && s.Start >= start
                             orderby s.Start
                             select s);
            return timeSlots.FirstOrDefault();
        }

        public List<Reservation> GetNumberOfReservationsUntil(DateTime endDate)
        {
            using var context = new RestaurantContext();
            return (from s in context.TimeSlots.Include(s => s.Reservations)
                    where s.Start <= endDate && s.Start >= DateTime.Now
                    select s)
                    .SelectMany(s => s.Reservations)
                    .ToList();
        }

        public List<TimeSlot> GetTimeSlotsFrom(DateTime start)
        {
            using var context = new RestaurantContext();
            return (from s in context.TimeSlots
                    where s.Start >= start
                    select s).ToList();
        }
    }
}
