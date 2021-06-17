using Swift.Balancer.Network.Interface;

namespace Swift.Balancer.Network.Networkable
{
    public class NetworkInput
    {
        private readonly string Data;
        private readonly IConnection Connection;

        public NetworkInput(string data, IConnection connection)
        {
            Data = data;
            Connection = connection;
        }

        public string GetData()
            => Data;

        public IConnection GetConnection()
            => Connection;
    }
}