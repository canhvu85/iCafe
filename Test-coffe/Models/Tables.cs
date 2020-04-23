using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Test_coffe.Models
{
    public class Tables
    {
        public int id { get; set; }
        [MaxLength(255)]
        [Display(Name = "Bàn")]
        public string name { get; set; }
        public int status { get; set; }
        [MaxLength(255)]
        public string permalink { get; set; }

        public int? FloorsId { get; set; }
        public Floors Floors { get; set; }
        [JsonIgnore]
        public ICollection<Bills> Bills { get; set; }

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
