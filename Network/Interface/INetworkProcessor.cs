using Swift.Balancer.Network.Networkable;

namespace Swift.Balancer.Network.Interface
{
    public interface INetworkProcessor
    {
        public bool Used(BaseNetworkable data);
        public void Use(IConnection connection, BaseNetworkable data);
    }
}