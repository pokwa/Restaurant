using System;
using System.Collections.Generic;
using System.Text;

namespace IDataInterface
{
    public interface IReservationManager
    {
        List<TimeSlot> GetTimeSlotsFrom(DateTime start);
        List<Reservation> GetNumberOfReservationsUntil(DateTime endDate);
    }
}
