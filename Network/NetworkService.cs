using System;
using Extensions.Object.Attributes;
using Swift.Balancer.App;
using Swift.Balancer.Network.Behaviours;

namespace Swift.Balancer.Network
{
    [RequiredBehaviour(typeof(NodeManagingBehaviour))]
    [RequiredBehaviour(typeof(StoringBehaviour))]
    [RequiredBehaviour(typeof(ProcessoringBehaviour))]
    public class NetworkService : ApplicationService
    {
        public override string Name() 
            => "[BALANCER] - Network Service";
        public override Version Version() 
            => System.Version.Parse("1.0.0.0");

        public StoringBehaviour AsStore()
            => GetComponent<StoringBehaviour>();

        public ProcessoringBehaviour AsProcessor()
            => GetComponent<ProcessoringBehaviour>();

        public NodeManagingBehaviour AsNodeManager()
            => GetComponent<NodeManagingBehaviour>();
    }
}