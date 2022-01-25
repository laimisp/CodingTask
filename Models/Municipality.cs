using System;
using System.ComponentModel.DataAnnotations;

namespace CodingTask
{
    public class Municipality
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
