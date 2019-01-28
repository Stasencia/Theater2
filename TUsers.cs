using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_theater
{
    [Table(Name = "Users")]
    class TUsers
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public string Login { get; set; }
        [Column]
        public string Password { get; set; }
        [Column(IsDbGenerated = true)]
        public int Rights { get; set; }
    }
}
