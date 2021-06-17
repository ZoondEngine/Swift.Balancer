using System;
using Extensions.Object;
using Microsoft.EntityFrameworkCore;
using Swift.Balancer.Config;
using Swift.Balancer.Database.Models;

namespace Swift.Balancer.Database
{
    public sealed class Db : DbContext
    {
        public DbSet<ChallengeKey> ChallengeKeys { get; set; }
        
        public Db()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = ExObject.FindObjectOfType<ConfigService>().Get();
            var credentials =
                $"server={config.DbHost};user={config.DbUsername};password={config.DbPassword};database={config.DbUsername}";

            optionsBuilder.UseMySql(credentials, ServerVersion.AutoDetect(credentials));
        }
    }
}