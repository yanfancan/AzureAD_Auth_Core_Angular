using System;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;

namespace HSC.RTD.AVLAggregatorCore.Data.POCO
{
    
    [Table("Sessions")]
    public class Session : IValidatable
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Enums.SessionStatus Status { get; set; }
        [HasValue]
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        [HasValue]
        public DateTime LastRequestDateTime { get; set; }
        [HasValue]
        public string AddedBy { get; set; }
        [Required]
        public int ServiceAccountId { get; set; }
        [HasValue]
        public string TimeZone { get; set; }
        [Required]
        public Enums.ServiceAccountRole Roles { get; set; }

        public string ClientIp { get; set; }
        [Required]
        public bool IsGeoZoneEnabled { get; set; }

    }
}