namespace Swift.Balancer.Network.Networkable.Packets
{
    public class Ping : BaseNetworkable
    {
        public long SentAt { get; set; }
        public long ReceivedAt { get; set; }
        public long Throttle { get; set; }
        
        public Ping()
            : base((ulong)PacketDefines.Ping)
        { }
    }
}