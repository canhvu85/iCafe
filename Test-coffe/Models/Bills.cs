using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Test_coffe.Models
{
    public class Bills
    {
        public int id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime time_enter { get; set; } = DateTime.Now;
        [Display(Name = "Ngày Đã Thanh Toán")]
        [Column(TypeName = "datetime")]
        public DateTime? time_out { get; set; }
        [Display(Name = "Tình Trạng")]
        public int status { get; set; } = 0;
        [Column(TypeName = "decimal(8,0)")]
        [Display(Name = "Tổng Tiền")]
        public decimal sub_total { get; set; }
        [Column(TypeName = "decimal(8,0)")]
        [Display(Name = "Phí Dịch Vụ")]
        public decimal fee_service { get; set; }
        [Display(Name = "Tổng Cộng")]
        [Column(TypeName = "decimal(8,0)")]
        public decimal total_money { get; set; }

        public int? TablesId { get; set; }
        public Tables Tables { get; set; }
        [JsonIgnore]
        public ICollection<BillDetails> BillDetails { get; set; }

        [DisplayName("Xóa")]
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
