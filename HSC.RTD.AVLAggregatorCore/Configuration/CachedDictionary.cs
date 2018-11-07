using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace HSC.RTD.AVLAggregatorCore
{
    public class CachedDictionary<T1, T2> : ICachedDictionary<T1, T2>
    {
        protected long expiration;
        protected Func<string, Dictionary<T1, T2>> getDictionary;
        protected string componentName;
        protected string memoryKey;
        private readonly IMemoryCache Cache;
        private readonly object _lock = new object();
        private readonly MemoryCacheEntryOptions cacheEntryOptions;


        public CachedDictionary(string name, Func<string, Dictionary<T1,T2>> getDictionary, long cacheExpiration, string componentName, IMemoryCache cache)
        {
            this.expiration = cacheExpiration;
            this.getDictionary = getDictionary;
            this.componentName = componentName;
            this.memoryKey = name + componentName;
            this.Cache = cache;
            this.cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(this.expiration));
        }

        public virtual T2 this[T1 key]
        {
            get
            {
                return this.getValue(key);
            }
        }

        public virtual T2 this[int idx]
        {
            get
            {
                var d = this.getDict();
                return d.ElementAtOrDefault(idx).Value;
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                Cache.Remove(this.memoryKey);
            }
        }

        public void Remove(T1 key)
        {
            lock (_lock)
            {
                var dict = this.getDict();
                dict.Remove(key);
                Cache.Set(this.memoryKey, dict, cacheEntryOptions);
            }
        }


        public IEnumerable<T2> All()
        {
            return this.getDict().Select(x=>x.Value);
        }

        public T2 Find(Func<KeyValuePair<T1,T2>, bool> selector)
        {
            return this.getDict().Where(selector).Select(x=>x.Value).FirstOrDefault();
        }

        private T2 getValue(T1 key)
        {
            var d = this.getDict();
            return d.ContainsKey(key) ? d[key] : default(T2);
        }

        private Dictionary<T1, T2> getDict()
        {
            Dictionary<T1, T2> dict;
            if (!Cache.TryGetValue(this.memoryKey, out dict) )
            {
                lock (_lock)
                {
                    dict = this.getDictionary(componentName);
                    Cache.Set(this.memoryKey, dict, cacheEntryOptions);
                }
            }
            return dict;
        }
    }
}
