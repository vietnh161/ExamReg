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
    public interface IMonThiRepository : IRepository<MonThi>
    {
		IEnumerable<MonThi> GetMultiByAdm(string keyword);

	}

    public class MonThiRepository : RepositoryBase<MonThi>, IMonThiRepository
    {
        public MonThiRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

		public IEnumerable<MonThi> GetMultiByAdm(string keyword)
		{


			var query = from p in DbContext.MonThi
						join pt in DbContext.LopHocPhan
						on p.MonThiId equals pt.MonThiId
						select p;

			if (keyword.Equals("null"))
			{
				

				var a = query.DistinctBy(x => x.MonThiId).AsEnumerable<MonThi>(); ;
				return a;
				
			}
			else
			{

				query = query.Where(x => x.Name.ToUpper().Contains(keyword.ToUpper()) || x.Title.ToUpper().Contains(keyword.ToUpper()));
				////query = query.Where(x => x.MSSV.Contains(keyword));
				//IEnumerable<SinhVienLophp> a = query.Select(x => x.p);

				var a = query.DistinctBy(x => x.MonThiId).AsEnumerable<MonThi>(); ;
				return a;
			}
		}
	}
}
