using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Swift.Balancer.Database.Models
{
    [Table("server_challenge_keys")]
    public class ChallengeKey
    {
        [Key]
        [Column("id")]
        public ulong Id { get; set; }
        
        [Column("key")]
        public string Key { get; set; }
        
        [Column("word")]
        public string Word { get; set; }

        [Column("created_at")] 
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")] 
        public DateTime UpdatedAt { get; set; }
    }
}