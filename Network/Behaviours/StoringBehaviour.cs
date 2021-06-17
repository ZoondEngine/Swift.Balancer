using Extensions.Object;

namespace Swift.Balancer.Network.Behaviours
{
    public class StoringBehaviour : ExBehaviour
    {
        private NetworkService _service;

        public override void Awake()
        {
            _service = ParentObject.Unbox<NetworkService>();
        }
    }
}