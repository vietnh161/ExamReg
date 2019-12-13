using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Model.Models
{
	[Table("kithi")]
	public class KiThi
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int KiThiId { set; get; }

		[StringLength(50)]
		public string Name { set; get; }

		public DateTime CreatedDate { set; get; }
	}
}
