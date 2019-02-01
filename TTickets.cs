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
        public int Performance_info_id { get; set; }
        [Column]
        public int Seat { get; set; }
        [Column]
        public double Price { get; set; }
    }
}
