using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Model.Models
{
    [Table("sinhvien_lophp")]
    public class SinhVienLophp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        
        public int SinhVienId { set; get; }

        public int LophpId { set; get; }

        public bool DuDieuKien { set; get; }

        [ForeignKey("SinhVienId")]
        public virtual SinhVien SinhVien { set; get; }

        [ForeignKey("LophpId")]
        public virtual LopHocPhan LopHocPhan { set; get; }


	
	}
}
