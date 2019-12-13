using ExamReg.Data.Infrastructure;
using ExamReg.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Data.Repositories
{
    public interface ISinhVienLichThiRepository : IRepository<SinhVienLichThi>
    {
		IEnumerable<SinhVien> getMultiSvbyId(int id);
    }

    public class SinhVienLichThiRepository : RepositoryBase<SinhVienLichThi>, ISinhVienLichThiRepository
    {
        public SinhVienLichThiRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
		public IEnumerable<SinhVien> getMultiSvbyId(int id)
		{
			var query = from sv in DbContext.SinhVien
						join svlt in DbContext.SinhVienLichThi
						on sv.SinhVienId equals svlt.SinhVienId
						where svlt.LichThiId == id
						select sv;
			return query;
		}
	}
}
