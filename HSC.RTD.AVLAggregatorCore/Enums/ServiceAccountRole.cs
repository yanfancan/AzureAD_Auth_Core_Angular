using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSC.RTD.AVLAggregatorCore.Enums
{
    [Flags]
    public enum ServiceAccountRole
    {
        Provider = 1,
        Consumer = 2
    }
}