using System.Collections.Generic;

namespace Alf.Utils
{

public static class DictionaryExtensions
{
    public static void AddIfNew<TKey, TValue>(this IDictionary<TKey, TValue> map, TKey key, TValue value)
    {
        if(map.ContainsKey(key))
            return;
        map.Add(key, value);
    }

    // Overwrites dictionary value if key already exists, returning the overwritten value
    public static TValue AddOverwrite<TKey, TValue>(this IDictionary<TKey, TValue> map, TKey key, TValue value)
    {
        TValue ret = default;
        if(map.ContainsKey(key))
        {
            ret = map[key];
            map.Remove(key);
        }
        map.Add(key, value);
        return ret;
    }
}
}