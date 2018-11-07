using HSC.RTD.AVLAggregatorCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace HSC.RTD.AVLAggregatorCore.Data.POCO
{
    public class Device : IValidatable
    {
        [Required]
        public int Id { get; set; }
        public string Address { get; set; }
        [HasValue]
        public int DeviceType { get; set; }
        [Required]
        public string VehicleName { get; set; }
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public int ServiceId { get; set; }
        //public int CACC { get; set; }



        public DateTime AddedDateTime { get; set; }
        [Required]
        public string AddedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string ModifiedBy { get; set; }
    }
}
