using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Extensions.Object;
using Swift.Balancer.Config;
using Swift.Balancer.Crypto;
using Swift.Balancer.Database;
using Swift.Balancer.Extensions;
using Swift.Balancer.Network.Interface;
using Swift.Balancer.Network.Networkable.Packets;
using Swift.Balancer.Serialize;
using WatsonTcp;

namespace Swift.Balancer.Network.Networkable
{
    public class Node : INode
    {
        private readonly IConnection _connection;
        private readonly Thread _updateThread;
        private bool _updating;

        private static class NodeStatistics
        {
            public static string Os;
            public static string Signature;
            public static short Pid;
            public static Version Version;
        }

        public Node(IConnection connection)
        {
            _connection = connection;
            _updateThread = new Thread(Updating);
            _updating = true;
            
            _updateThread.Start();
        }

        [SuppressMessage("ReSharper", "FunctionNeverReturns")]
        private void Updating()
        {
            using var db = ExObject.FindObjectOfType<DatabaseService>().GetContext();
            var crypto = ExObject.FindObjectOfType<CryptoService>();
            var network = ExObject.FindObjectOfType<NetworkService>();
            var config = ExObject.FindObjectOfType<ConfigService>().Get();
            var updateInterval = config.NetworkNodeUpdateInterval == 0 ? config.NetworkNodeUpdateInterval : 3000;
            
            while (true)
            {
                if (!_updating)
                {
                    Thread.Sleep(updateInterval);
                    continue;
                }
                
                var item = db.ChallengeKeys.ToList().Random();
                var response = SendAndWait(new NodeStatistic
                {
                    NodeEncryptedWord = crypto.Encrypt(item.Word, item.Key)
                });

                var data = Serializer.Deserialize<NodeStatistic>(Encoding.UTF8.GetString(response.Data));
                if (data.Valid())
                {
                    NodeStatistics.Os = data.Os;
                    NodeStatistics.Pid = data.Pid;
                    NodeStatistics.Signature = data.Signature;
                    NodeStatistics.Version = data.Version;
                }
                else
                {
                    network.Error($"Error while updating node statistics: {_connection.Identifier().ToString()}");
                }
                
                Thread.Sleep(updateInterval);
            }
        }

        public void Disconnect()
        {
            _connection.Disconnect();

            try
            {
                _updateThread.Interrupt();
            }
            catch
            {
                // ignored
            }
        }

        public void Pause()
            => _updating = false;

        public void Resume()
            => _updating = true;

        public IConnection Connection()
            => _connection;

        public string Os()
            => NodeStatistics.Os;

        public short Pid()
            => NodeStatistics.Pid;

        public Version Version()
            => NodeStatistics.Version;

        public string Signature()
            => NodeStatistics.Signature;

        public bool Send(BaseNetworkable data)
            => _connection.Send(data);

        public Task<bool> SendAsync(BaseNetworkable data)
            => _connection.SendAsync(data);
        public SyncResponse SendAndWait(BaseNetworkable data)
            => _connection.SendAndWait(data);
    }
}