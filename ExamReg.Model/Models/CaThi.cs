using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Model.Models
{
    [Table("cathi")]
    public class CaThi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CaThiId { set; get; }

        [StringLength(50)]
        public string Name { set; get; }

    }
}
