using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSC.RTD.AVLAggregatorCore.Enums
{
    [Flags]
    public enum UserRole
    {
        Admin = 1,
        User = 2,
    }
}