using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Model.Models
{
    [Table("sinhvien_lichthi")]
    public class SinhVienLichThi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public int SinhVienId { set; get; }

	

		public int LichThiId { set; get; }

		public bool Status { set; get; }

        [ForeignKey("SinhVienId")]
        public virtual SinhVien SinhVien { set; get; }

        [ForeignKey("LichThiId")]
        public virtual LichThi LichThi { set; get; }

	
	}
}
