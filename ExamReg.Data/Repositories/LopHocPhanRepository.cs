using ExamReg.Data.Infrastructure;
using ExamReg.Model.Models;
using MoreLinq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Data.Repositories
{
	public interface ILopHocPhanRepository : IRepository<LopHocPhan>
	{
		//IEnumerable<LopHocPhan> getMulti(int kithiId);
		//IEnumerable<LopHocPhan> getMulti(int kiThiId, int monThiId);
	}

	public class LopHocPhanRepository : RepositoryBase<LopHocPhan>, ILopHocPhanRepository
	{
		public LopHocPhanRepository(IDbFactory dbFactory) : base(dbFactory)
		{

		}

		//public IEnumerable<LopHocPhan> getMulti(int kiThiId)
		//{


		//	var query = from p in DbContext.LopHocPhan
		//				join pt in DbContext.SinhVienLophp
		//				on p.LophpId equals pt.LophpId
		//				where p.KiThiId == kiThiId
		//				orderby p.LophpId descending
		//				select p;
		//	var a = query.DistinctBy(x => x.LophpId).AsEnumerable<LopHocPhan>(); ;
		//	return a;

		//}
		//public IEnumerable<LopHocPhan> getMulti(int kiThiId, int monThiId)
		//{


		//	var query = from p in DbContext.LopHocPhan
		//				join pt in DbContext.SinhVienLophp
		//				on p.LophpId equals pt.LophpId
		//				where p.MonThiId == monThiId && p.KiThiId == kiThiId
		//				orderby p.LophpId descending
		//				select p;
		//	var a = query.DistinctBy(x => x.LophpId).AsEnumerable<LopHocPhan>(); ;
		//	return a;

		//}

	}
}
