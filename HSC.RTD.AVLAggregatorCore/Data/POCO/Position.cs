using System;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;

namespace HSC.RTD.AVLAggregatorCore.Data.POCO
{
    [Table("Positions")]
    public class Position
    {
        [HasValue(ValidateDirection.Inbound)]
        public int Id { get; set; }
        [HasValue]
        public string Address { get; set; }
        [Required]
        public decimal Latitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }
        [Required]
        public int Velocity { get; set; }
        [Required]
        public int Direction { get; set; }
        [HasValue]
        public DateTime AvlDateTime { get; set; }
        public string ModifiedBy { get; set; }
        [HasValue]
        public string VehicleName { get; set; }
    }
}