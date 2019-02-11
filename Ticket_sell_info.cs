using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_theater
{
    class Ticket_sell_info
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Small_image { get; set; }
        public bool Cancelled { get; set; }
        public int Count { get; set; }
        public IEnumerable<int> Seats { get; set; }
    }
}
