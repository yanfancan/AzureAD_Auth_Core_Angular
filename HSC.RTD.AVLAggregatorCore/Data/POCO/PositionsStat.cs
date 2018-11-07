using System;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;


namespace HSC.RTD.AVLAggregatorCore.Data.POCO
{
    public class PositionsStat
    {
        [HasValue(ValidateDirection.Inbound)]
        public int Id { get; set; }
        public DateTime? LastReportDateTime { get; set; }
        [Required]
        public int PositionId { get; set; }
    }
}