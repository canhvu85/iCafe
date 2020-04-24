using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Test_coffe.Models
{
    public class Users
    {
        public int id { get; set; }
        [MaxLength(255)]
        public string name { get; set; }
        [MaxLength(255)]
        public string images { get; set; }
        [MaxLength(255)]
        public string username { get; set; }
        [MaxLength(255)]
        public string password { get; set; }
        [MaxLength(255)]
        public string token { get; set; }
        [MaxLength(255)]
        public string permalink { get; set; }

        public int? PositionsId { get; set; }
        public Positions Positions { get; set; }
        public int? ShopsId { get; set; }
        public Shops Shops { get; set; }
        [JsonIgnore]
        public ICollection<PermissionDetails> PermissionDetails { get; set; }
        [JsonIgnore]
        public ICollection<Accounts> Accounts { get; set; }

        public bool isDeleted { get; set; } = false;
        [Column(TypeName = "datetime")]
        public DateTime? deleted_at { get; set; }
        [MaxLength(255)]
        public string? deleted_by { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime created_at { get; set; } = DateTime.Now;
        [MaxLength(255)]
        public string? created_by { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; }
        [MaxLength(255)]
        public string? updated_by { get; set; }
    }
}
