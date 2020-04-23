using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Test_coffe.Models
{
    public class BillDetails
    {
        public int id { get; set; }
        [Column(TypeName = "decimal(8,0)")]
        [Display(Name = "Giá")]
        public decimal price { get; set; }
        [Display(Name = "SL")]
        public int quantity { get; set; }
        [Column(TypeName = "decimal(8,0)")]
        [Display(Name = "Tổng Tiền")]
        public decimal total { get; set; }
        [MaxLength(255)]
        public string permalink { get; set; }
        [Display(Name = "Tình Trạng")]
        public int status { get; set; }

        public int? ProductsId { get; set; }
        public Products Products { get; set; }
        public int? BillsId { get; set; }
        public Bills Bills { get; set; }

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
