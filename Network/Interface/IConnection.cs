using System;
using System.Threading.Tasks;
using Swift.Balancer.Network.Networkable;
using WatsonTcp;

namespace Swift.Balancer.Network.Interface
{
    public interface IConnection
    {
        public Guid Identifier();
        public bool Send(BaseNetworkable data);
        public Task<bool> SendAsync(BaseNetworkable data);
        public SyncResponse SendAndWait(BaseNetworkable data);
        public long Ping();
        public void Disconnect();
    }
}