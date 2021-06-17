using System;
using Swift.Balancer.App;

namespace Swift.Balancer.Database
{
    public class DatabaseService : ApplicationService
    {
        public Db GetContext()
            => new();
        
        public override string Name()
            => "[BALANCER] - Database Service";

        public override Version Version()
            => System.Version.Parse("1.0.0.0");
    }
}