using System;
using System.ComponentModel.DataAnnotations;

namespace CodingTask
{
    public class Tax
    {
        [Key]
        public int Id { get; set; }
        public int Municipality_Id { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public decimal Result { get; set; }
    }
}
