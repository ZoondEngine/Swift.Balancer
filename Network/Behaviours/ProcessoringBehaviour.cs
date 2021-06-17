using System.Collections.Generic;
using System.Linq;
using Extensions.Object;
using Swift.Balancer.Network.Interface;
using Swift.Balancer.Network.Networkable;
using Swift.Balancer.Serialize;

namespace Swift.Balancer.Network.Behaviours
{
    public class ProcessoringBehaviour : ExBehaviour
    {
        private NetworkService _service;
        private List<INetworkProcessor> _processors;
        private Queue<NetworkInput> _messages;
        private bool _processing;

        public override void Awake()
        {
            _service = ParentObject.Unbox<NetworkService>();
            _processors = new List<INetworkProcessor>();
            _messages = new Queue<NetworkInput>();
            _processing = true;
        }

        public void Queue(NetworkInput input)
            => _messages.Enqueue(input);

        public void Pause()
            => _processing = false;

        public void Resume()
            => _processing = true;

        public override void Update()
        {
            if (_messages.Count <= 0 || !_processing) return;
            
            var item = _messages.Dequeue();
            var data = Serializer.Deserialize<BaseNetworkable>(item.GetData());

            if (!data.Valid())
            {
                _service.Warning($"Messages queue contains invalid message. Skipped");
                return;
            }
            
            foreach (var processor in _processors.Where(processor => processor.Used(data)))
            {
                processor.Use(item.GetConnection(), data);
            }
        }
    }
}