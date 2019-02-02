using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;

namespace Project_theater
{
    [Table(Name = "Afisha")]
    class TAfisha
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Image { get; set; }
        [Column]
        public string Duration { get; set; }
        [Column]
        public string Age_restriction { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public string Big_image { get; set; }
        [Column]
        public string Small_info1 { get; set; }
        [Column]
        public string Small_info2 { get; set; }
        [Column]
        public string Small_info3 { get; set; }
        [Column]
        public string Small_name { get; set; }
        [Column]
        public string Small_image { get; set; }
        [Column]
        public double Price { get; set; }
        [Column]
        public bool Is_relevant { get; set; }
    }
}
