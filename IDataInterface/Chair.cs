using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IDataInterface
{
    public class Chair
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChairID { get; set; }

        public int ChairNumber { get; set; }
        public int TableID { get; set; }
        public Table Table { get; set; }
    }
}
