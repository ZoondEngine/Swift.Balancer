namespace Swift.Balancer.Serialize.Interface
{
    public interface ISerializable
    {
        T Deserialize<T>(string data);
        string Serialize(object data);
    }
}