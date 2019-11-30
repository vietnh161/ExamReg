using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Model.Models
{
    [Table("sinhvien")]
    public class SinhVien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SinhVienId { set; get; }

        [MaxLength(8)]
        public string MSSV { set; get; }

        [MaxLength(256)]
        public string FullName { set; get; }

        [MaxLength(256)]
        public string Address { set; get; }

        public DateTime? BirthDay { set; get; }

        [StringLength(15)]
        public string phone { set; get; }

        [StringLength(50)]
        public string email { set; get; }

        [StringLength(128)]
        public string UserId { set; get; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { set; get; }
    }
}
