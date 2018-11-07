using System;
using System.ComponentModel.DataAnnotations;

namespace HSC.RTD.AVLAggregatorCore.Data.POCO
{
   
    public class ServiceAccount : IValidatable
    {
        [Required]
        public int Id { get; set; }
        [HasValue]
        public string LoginName { get; set; }
        [HasValue]
        public string Name { get; set; }
        [HasValue]
        public string Password { get; set; }
        [Required]
        public Enums.ServiceAccountRole Roles { get; set; }
        [Required]
        public Enums.UserStatus Status { get; set; }
        [Required]
        public string TimeZone { get; set; }
        [Required]
        public DateTime AddedDateTime { get; set; }
        [Required]
        public string AddedBy { get; set; }

        public DateTime ModifiedDateTime { get; set; }
        public string ModifiedBy { get; set; }

    }
}