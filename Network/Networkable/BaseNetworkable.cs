using System;
using Newtonsoft.Json;
using Swift.Balancer.Network.Interface;
using Swift.Balancer.Serialize;

namespace Swift.Balancer.Network.Networkable
{
    /// <summary>
    /// Base network packet structure
    /// </summary>
    public class BaseNetworkable
    {
        /// <summary>
        /// Packet identifier
        /// </summary>
        public ulong Identifier { get; set; }
        
        /// <summary>
        /// Packet sent timestamp
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// Default constructor, for serialize/deserialize only
        /// </summary>
        public BaseNetworkable()
        { }

        /// <summary>
        /// Usable constructor
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="timestamp"></param>
        public BaseNetworkable(ulong identifier, long timestamp = 0u)
        {
            if (timestamp == 0u)
            {
                timestamp = DateTime.Now.ToFileTimeUtc();
            }
            
            Identifier = identifier;
            Timestamp = timestamp;
        }

        /// <summary>
        /// Extract the packet from base structure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Extract<T>() where T : BaseNetworkable
        {
            return (T)this;
        }

        /// <summary>
        /// Serialize current packet
        /// </summary>
        /// <returns></returns>
        public string Destruct()
            => Serializer.Serialize(this);

        /// <summary>
        /// Overridable validation packet flag
        /// </summary>
        /// <returns></returns>
        public virtual bool Valid()
        {
            return Identifier != 0u
                   && Timestamp != 0u;
        }
    }
}