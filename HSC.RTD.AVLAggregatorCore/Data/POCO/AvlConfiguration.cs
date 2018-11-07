using System;
using System.ComponentModel.DataAnnotations;

namespace HSC.RTD.AVLAggregatorCore.Data.POCO
{
    public class AvlConfiguration : IValidatable
    {
        [HasValue]
        public int ConfigurationId { get; set; }
        [HasValue]
        public string ConfigurationKey { get; set; }
        [HasValue]
        public string ConfigurationValue { get; set; }
        public string Description { get; set; }
        public DateTime LastChangedDateTime { get; set; }
    }
}