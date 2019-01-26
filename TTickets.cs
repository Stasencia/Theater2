using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Threading.Tasks;

namespace Project_theater
{
    [Table(Name = "Tickets")]
    class TTickets
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public int User_Id { get; set; }
        [Column]
        public int Performance_id { get; set; }
        [Column]
        public DateTime Date { get; set; }
        [Column]
        public int Seat { get; set; }
        [Column]
        public float Price { get; set; }
    }
}
