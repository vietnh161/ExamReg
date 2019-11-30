using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Model.Models
{
    [Table("lichthi")]
    public class LichThi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LichThiId { set; get; }


        public int LophpId { set; get; }

        public int CaThiId { set; get; }

        public int PhongThiId { set; get; }

        public DateTime NgayThi { set; get; }

        public TimeSpan GioBatDau { set; get; }

        public TimeSpan GioKetThuc { set; get; }

        public bool Status { set; get; }

		public int Count { set; get; }

        [ForeignKey("CaThiId")]
        public virtual CaThi CaThi { set; get; }

        [ForeignKey("LophpId")]
        public virtual LopHocPhan LopHocPhan { set; get; }

        [ForeignKey("PhongThiId")]
        public virtual PhongThi PhongThi { set; get; }

    }
}
