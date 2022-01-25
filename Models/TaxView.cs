using System;
using System.ComponentModel.DataAnnotations;

namespace CodingTask
{
    public class TaxVew
    {
        [Key]
        public int Id { get; set; }
        public string MunicipalityName { get; set; }
        public string Date { get; set; }
        public decimal Result { get; set; }
    }
}
