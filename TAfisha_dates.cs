using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;

namespace Project_theater
{
    [Table(Name = "Afisha_dates")]
    class TAfisha_dates
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public int Id_performance { get; set; }
        [Column]
        public DateTime Date { get; set; }
    }
}
