using System;
using System.Collections.Generic;

namespace HSC.RTD.AVLAggregatorCore
{
    public interface IAvlConfiguration : IValidatable
    {
        string this[string key] { get; }
        string this[int idx] { get; }

        void Clear();

        string Find(Func<KeyValuePair<string, string>, bool> selector);

        IEnumerable<string> All();
    }
}