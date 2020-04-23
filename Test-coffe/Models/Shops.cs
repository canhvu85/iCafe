using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Test_coffe.Models
{
    public class Shops
    {
        public int id { get; set; }
        [MaxLength(255)]
        public string name { get; set; }
        [MaxLength(255)]
        public string info { get; set; }
        [MaxLength(255)]
        public string images { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime")]
        public DateTime time_open { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime")]
        public DateTime time_close { get; set; }
        public int status { get; set; }
        [MaxLength(255)]
        public string permalink { get; set; }

        public int? CitiesId { get; set; }
        public Cities Cities { get; set; }
        [JsonIgnore]
        public ICollection<Floors> Floors { get; set; }

        [JsonIgnore]
        public ICollection<Cataloges> Cataloges { get; set; }
        [JsonIgnore]
        public ICollection<Users> Users { get; set; }

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
