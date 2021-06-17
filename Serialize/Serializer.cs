using System.Collections.Generic;
using System.Linq;
using Swift.Balancer.Serialize.Interface;
using Swift.Balancer.Serialize.Providers;

namespace Swift.Balancer.Serialize
{
    public static class Serializer
    {
        /// <summary>
        /// Serializers container
        /// </summary>
        private static readonly List<ISerializable> Serializers;
        
        /// <summary>
        /// Default serializer
        /// </summary>
        private static readonly ISerializable DefaultSerializer;

        /// <summary>
        /// Default constructor
        /// </summary>
        static Serializer()
        {
            Serializers = new List<ISerializable>();
            DefaultSerializer = new JsonSerializable();
            
            Register();
        }

        /// <summary>
        /// Register the serializers
        /// </summary>
        private static void Register()
        {
            Serializers.Add(new JsonSerializable());
        }

        /// <summary>
        /// Deserialize object
        /// </summary>
        /// <param name="data"></param>
        /// <param name="serializer"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Deserialize<T>(string data, string serializer = null)
        {
            if (serializer != null && Find(serializer))
            {
                return Get(serializer).Deserialize<T>(data);
            }

            return DefaultSerializer.Deserialize<T>(data);
        }

        /// <summary>
        /// Serialize object
        /// </summary>
        /// <param name="data"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public static string Serialize(object data, string serializer = null)
        {
            if (serializer != null && Find(serializer))
            {
                return Get(serializer).Serialize(data);
            }

            return DefaultSerializer.Serialize(data);
        }

        /// <summary>
        /// Check existing of specific serializer
        /// </summary>
        /// <param name="serializer"></param>
        /// <returns></returns>
        private static bool Find(string serializer)
        {
            return serializer != null && Get(serializer) != null;
        }

        /// <summary>
        /// Get specific serializer
        /// </summary>
        /// <param name="serializer"></param>
        /// <returns></returns>
        private static ISerializable Get(string serializer)
        {
            return Serializers.FirstOrDefault(x => x.GetType().Name.ToLower().Contains(serializer.ToLower()));
        }
    }
}