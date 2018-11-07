using System;
using System.ComponentModel.DataAnnotations;

namespace HSC.RTD.AVLAggregatorCore.Data.POCO
{
    public class ServiceAccountServices : IValidatable
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ServiceAcountId { get; set; }
        [Required]
        public int ServiceId { get; set; }
        

        public DateTime AddedDateTime { get; set; }
        [Required]
        public string AddedBy { get; set; }
    }
}
