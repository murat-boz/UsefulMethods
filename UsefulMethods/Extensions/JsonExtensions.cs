using Newtonsoft.Json;
using System;

namespace UsefulMethods.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this object obj, JsonSerializerSettings jsonSerializerSettings = null)
        {
            return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
        }

        public static T FromJson<T>(this string json, JsonSerializerSettings jsonSerializerSettings = null)
        {
            return JsonConvert.DeserializeObject<T>(json, jsonSerializerSettings);
        }

        public static object FromJson(this string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }
    }
}
