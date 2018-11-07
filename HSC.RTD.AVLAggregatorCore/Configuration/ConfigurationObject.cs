using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;


namespace HSC.RTD.AVLAggregatorCore
{
    public class ConfigurationObject : CachedDictionary<string,string>, IAvlConfiguration
    {
        public ConfigurationObject(Func<string, Dictionary<string, string>> getDictionary, string componentName, IMemoryCache cache) : base("CIPConfiguration", getDictionary, 600, componentName, cache)
        {

        }
    }
}
