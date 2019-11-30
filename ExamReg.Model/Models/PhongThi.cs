using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Model.Models
{
    [Table("phongthi")]
    public class PhongThi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhongThiId { set; get; }

        [StringLength(50)]
        public string Name { set; get; }

        public int SoLuongMay { set; get; }

    }
}
