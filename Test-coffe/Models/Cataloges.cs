using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Test_coffe.Models
{
    public class Cataloges
    {
        public int id { get; set; }
        [MaxLength(255)]
        [MinLength(3)]
        [Display(Name = "Danh Mục", Prompt = "Nhập tên danh mục")]
        public string name { get; set; }
        [MaxLength(255)]
        public string permalink { get; set; }

        public int? ShopsId { get; set; }
        [Display(Name = "Cửa Hàng")]
        public Shops Shops { get; set; }
        [JsonIgnore]
        public ICollection<Products> Products { get; set; }

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
