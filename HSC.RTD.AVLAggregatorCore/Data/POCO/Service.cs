using System;
using System;
using System.ComponentModel.DataAnnotations;

namespace HSC.RTD.AVLAggregatorCore.Data.POCO
{
    public class Service : IValidatable
    {
        [Required]
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string Description{ get; set; }

        public DateTime AddedDateTime { get; set; }
        [Required]
        public string AddedBy { get; set; }
    }
}
