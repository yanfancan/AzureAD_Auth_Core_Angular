using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSC.RTD.AVLAggregatorCore.Enums
{
    [Flags]
    public enum SessionStatus
    {
        Active = 1,
        Closed = 2,
        Expired = 4
    }
}