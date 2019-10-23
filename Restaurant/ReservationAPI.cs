using IDataInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant
{
    public class ReservationAPI
    {
        IReservationManager reservationManager;
        ITableManager tableManager;
        public ReservationAPI(IReservationManager reservationManager, ITableManager tableManager)
        {
            this.reservationManager = reservationManager;
            this.tableManager = tableManager;
        }
        public DateTime FindNextOpenTableForPartySize(int numberOfSeats, DateTime start)
        {
            var timeSlots = reservationManager.GetTimeSlotsFrom(start);
            var tables = tableManager.GetAllTables();
            return FindAvailableTimeForReservation(numberOfSeats, timeSlots, tables);
        }

        private static DateTime FindAvailableTimeForReservation(int numberOfSeats, List<TimeSlot> timeSlots, List<Table> tables)
        {
            for (int i = 0; i < timeSlots.Count; i++)
            {
                var open = FindOpenTablesThisTimeslot(numberOfSeats, timeSlots[i], tables);
                if (open.Count() > 0)
                    return timeSlots[i].Start;
            }
            return DateTime.MaxValue;
        }

        private static IEnumerable<Table> FindOpenTablesThisTimeslot(int numberOfSeats, TimeSlot timeSlot, List<Table> tables)
        {
            return tables.Where(t =>
                                !timeSlot.Reservations
                                    .Any(r => r.TableID == t.TableID)
                                && t.Chairs.Count >= numberOfSeats);
        }
    }
}
