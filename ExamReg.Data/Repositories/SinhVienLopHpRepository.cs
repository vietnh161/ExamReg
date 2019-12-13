using ExamReg.Data.Infrastructure;
using ExamReg.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Data.Repositories
{
     public interface ISinhVienLophpRepository : IRepository<SinhVienLophp>
    {
		IEnumerable<SinhVienLophp> GetMultiByAdm(string keyword, int lopHpId);

	}

    public class SinhVienLophpRepository : RepositoryBase<SinhVienLophp>, ISinhVienLophpRepository
    {
        public SinhVienLophpRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

		public IEnumerable<SinhVienLophp> GetMultiByAdm(string keyword,int lopHpId)
		{




			if (keyword.Equals("null"))
			{
				var query = from p in DbContext.SinhVienLophp
							where p.LophpId == lopHpId
							select p;
				return query;
			}
			else
			{

				var query = from p in DbContext.SinhVienLophp
							join pt in DbContext.SinhVien
							on p.SinhVienId equals pt.SinhVienId
							where p.LophpId == lopHpId
							select new
							{
								p,
								pt.FullName,
								pt.MSSV
							};
				query = query.Where(x => x.FullName.ToUpper().Contains(keyword.ToUpper()) || x.MSSV.Contains(keyword));
				//query = query.Where(x => x.MSSV.Contains(keyword));
				IEnumerable<SinhVienLophp> a = query.Select(x => x.p);

				// a = query.DistinctBy(x => x.s).AsEnumerable<SinhVienLophp>(); ;
				return a;
			}

		}
	}
}
