using System;
using System.Threading.Tasks;
using Swift.Balancer.Network.Networkable;
using WatsonTcp;

namespace Swift.Balancer.Network.Interface
{
    public interface INode
    {
        public void Disconnect();
        public void Pause();
        public void Resume();
        public IConnection Connection();
        public string Os();
        public short Pid();
        public Version Version();
        public string Signature();
        public bool Send(BaseNetworkable data);
        public Task<bool> SendAsync(BaseNetworkable data);
        public SyncResponse SendAndWait(BaseNetworkable data);
    }
}