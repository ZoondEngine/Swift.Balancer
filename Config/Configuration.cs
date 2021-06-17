namespace Swift.Balancer.Config
{
    public class Configuration
    {
        public string DbUsername { get; set; } = "root";
        public string DbPassword { get; set; } = "";
        public string DbName { get; set; } = "swift-global-db";
        public string DbHost { get; set; } = "127.0.0.1";
        public string DbPort { get; set; } = "3306";

        public int NetworkNodeUpdateInterval = 3000;

        public bool Valid()
        {
            return DbUsername != null
                   && DbName != null
                   && DbHost != null
                   && DbPort != null;
        }

        public void Default()
        {
            DbUsername = "root";
            DbPassword = "";
            DbName = "";
            DbHost = "127.0.0.1";
            DbPort = "3306";
        }
    }
}