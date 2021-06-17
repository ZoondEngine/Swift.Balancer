using System;

namespace Swift.Balancer.Network.Networkable.Packets
{
    public class NodeStatistic : BaseNetworkable
    {
        public string NodeEncryptedWord { get; set; }
        public string Os { get; set; }
        public string Signature { get; set; }
        public short Pid { get; set; }
        public Version Version { get; set; }

        public NodeStatistic()
            : base((ulong)PacketDefines.NodeStatistics)
        { }

        public override bool Valid()
        {
            return Os != null
                   && Signature != null
                   && Pid != 0
                   && Version != null;
        }
    }
}