using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Model.Models
{
    [Table("lophocphan")]
    public class LopHocPhan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LophpId { set; get; }

        [StringLength(20)]
        public string Title { set; get; }

        [StringLength(50)]
        public string Name { set; get; }

        public int MonThiId { set; get; }
        
        [ForeignKey("MonThiId")]
        public virtual MonThi MonThi { set; get; }
    }
}
