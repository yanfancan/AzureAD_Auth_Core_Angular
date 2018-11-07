using System;
using System.Collections.Generic;

namespace HSC.RTD.AVLAggregatorCore
{
    public interface ICachedDictionary<T1, T2>
    {
        T2 this[int idx] { get; }
        T2 this[T1 key] { get; }

        IEnumerable<T2> All();
        T2 Find(Func<KeyValuePair<T1, T2>, bool> selector);
        void Clear();

        void Remove(T1 key);
    }
}