using System;
using System.IO;
using Swift.Balancer.App;
using Swift.Balancer.Serialize;

namespace Swift.Balancer.Config
{
    public class ConfigService : ApplicationService
    {
        private Configuration _configuration;
        
        public ConfigService()
            : base()
        {
            Load();
        }

        public void Load()
        {
            var data = File.ReadAllText(ConfigFolder() + "\\config.json");
            _configuration = Serializer.Deserialize<Configuration>(data);
            if (!_configuration.Valid())
            {
                _configuration.Default();
            }
        }

        public void Save()
        {
            if (_configuration != null)
            {
                var data = Serializer.Serialize(_configuration);
                File.WriteAllText(ConfigFolder() + "\\config.json", data);
            }
        }

        public void Reload()
        {
            Save();
            Load();
        }

        public Configuration Get()
            => _configuration;
        
        public override string Name()
            => "[BALANCER] - Config Service";

        public override Version Version()
            => System.Version.Parse("1.0.0.0");
    }
}