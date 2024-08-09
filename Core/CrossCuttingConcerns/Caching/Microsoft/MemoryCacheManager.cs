using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;
        private readonly ConcurrentDictionary<string, bool> _cacheKeys;

        public MemoryCacheManager()
        {
            _cache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
            _cacheKeys = new ConcurrentDictionary<string, bool>();
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public void Add(string key, object data, int duration)
        {
            _cache.Set(key, data, TimeSpan.FromMinutes(duration));

            //In .NetCore3 version of removeByPattern method we weren't need this.
            //This added to store keys in ConcurrentDictionary to delete later.
            //The dictionary provides O(1) complexity for adding and removing keys, which is efficient.
            //This may not be the best practice - CHECK LATER
            _cacheKeys[key] = true; // Add key to tracking collection
        }

        public bool IsAdd(string key)
        {
            return _cache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
            _cacheKeys.TryRemove(key, out _); // Remove key from tracking collection
        }

        //This may not be the best practice - CHECK LATER - Details given on above
        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = _cacheKeys.Keys.Where(key => regex.IsMatch(key)).ToList();

            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);
                _cacheKeys.TryRemove(key, out _);
            }

        }
        
        //OLD VERSION - .NET CORE 3 

        //public void RemoveByPattern(string pattern)
        //{
        //    // (_cache.Keys) before .net core there was a method like beginning. But now .net core is not givings us cache object. To get this object we are writing these;
        //    //Half of the code came from Stackoverflow and other half written by Engin Demirog.
        //    //To get details of the code: https://www.udemy.com/course/net-core-c-sharp-kursu-2/learn/lecture/16393876#questions/8583358

        //    var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        //    var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_cache) as dynamic;


        //    List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

        //    foreach (var cacheItem in cacheEntriesCollection)
        //    {

        //        ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);


        //        cacheCollectionValues.Add(cacheItemValue);
        //    }

        //    var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //    var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

        //    foreach (var key in keysToRemove)
        //    {
        //        _cache.Remove(key);
        //    }
        //}
    }
}