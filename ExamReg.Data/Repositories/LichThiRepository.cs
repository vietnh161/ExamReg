using ExamReg.Data.Infrastructure;
using ExamReg.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Data.Repositories
{
    public interface ILichThiRepository : IRepository<LichThi>
    {
        bool checkDuplicate(LichThi lThi);
    }

    public class LichThiRepository : RepositoryBase<LichThi>, ILichThiRepository
    {
        public LichThiRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public bool checkDuplicate(LichThi lThi)
        {
			bool result = true;
			IEnumerable<LichThi> list = DbContext.lichThi.Where(s => s.PhongThiId == lThi.PhongThiId && s.NgayThi == lThi.NgayThi).ToList();
			if (list.Count() > 0)
				result = list.Any(s => (s.GioBatDau >= lThi.GioBatDau && s.GioBatDau <= lThi.GioKetThuc) ||
				(s.GioKetThuc >= lThi.GioBatDau && s.GioKetThuc <= lThi.GioKetThuc));
			else result = false;

            return result;
        }
    }
}
