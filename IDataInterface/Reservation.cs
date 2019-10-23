using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IDataInterface
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationID { get; set; }

        public string Name { get; set; }

        public int TableID { get; set; }
        public Table Table { get; set; }

        public int TimeSlotID { get; set; }
        public TimeSlot TimeSlot { get; set; }
    }
}
