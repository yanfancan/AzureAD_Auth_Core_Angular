using HSC.RTD.AVLAggregatorCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace HSC.RTD.AVLAggregatorCore.Data.POCO
{

    public class User : IValidatable
    {
        [Required]
        public int Id { get; set; }
        [HasValue]
        public string FirstName { get; set; }
        [HasValue]
        public string LastName { get; set; }
        [HasValue]
        public string Department { get; set; }
        [HasValue]
        public string Email { get; set; }
        [HasValue]
        public string Position { get; set; }
        [HasValue]
        public int AccessLevel { get; set; }
        [HasValue]
        public bool AccountLocked { get; set; }
        [HasValue]
        public bool AccountDisabled { get; set; }
        public string Password { get; set; }
        [Required]
        public DateTime AddedDateTime { get; set; }
        [Required]
        public string AddedBy { get; set; }


        public DateTime ModifiedDateTime { get; set; }
        public string ModifiedBy { get; set; }

    }
}