using ExamReg.Data.Infrastructure;
using ExamReg.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Data.Repositories
{
	public interface IKiThiRepository : IRepository<KiThi>
	{

	}

	public class KiThiRepository : RepositoryBase<KiThi>, IKiThiRepository
	{
		public KiThiRepository(IDbFactory dbFactory) : base(dbFactory)
		{

		}
	}
}
