using Newtonsoft.Json;
using Swift.Balancer.Serialize.Interface;

namespace Swift.Balancer.Serialize.Providers
{
    public class JsonSerializable : ISerializable
    {
        public T Deserialize<T>(string data)
            => JsonConvert.DeserializeObject<T>(data);

        public string Serialize(object data)
            => JsonConvert.SerializeObject(data);
    }
}