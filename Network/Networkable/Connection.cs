using System;
using System.Text;
using System.Threading.Tasks;
using Swift.Balancer.Network.Interface;
using Swift.Balancer.Network.Networkable.Packets;
using Swift.Balancer.Serialize;
using WatsonTcp;

namespace Swift.Balancer.Network.Networkable
{
    public class Connection : IConnection
    {
        private readonly WatsonTcpClient _client;
        private readonly Guid _id;

        public Connection(WatsonTcpClient client)
        {
            _client = client;
            _id = Guid.NewGuid();
        }

        public Guid Identifier()
            => _id;
        public bool Send(BaseNetworkable data)
            => _client.Send(data.Destruct());
        public Task<bool> SendAsync(BaseNetworkable data)
            => _client.SendAsync(data.Destruct());
        public SyncResponse SendAndWait(BaseNetworkable data)
            => _client.SendAndWait(5000, Encoding.UTF8.GetBytes(data.Destruct()));
        
        public long Ping()
        {
            var response = Serializer.Deserialize<Ping>(
                Encoding.UTF8.GetString(
                    SendAndWait(new Ping { SentAt = DateTime.Now.ToFileTimeUtc() }).Data));

            return response.Throttle;
        }

        public void Disconnect()
        {
            _client.Disconnect();
            _client.Dispose();
        }
    }
}