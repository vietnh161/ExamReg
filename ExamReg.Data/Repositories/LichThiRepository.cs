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
        bool checkDuplicateAdd(LichThi lThi);
		bool checkDuplicateUp(LichThi lThi);
		IEnumerable<LichThi> getDaDky(int id);
		IEnumerable<LichThi> getMultibySv(int id);
		IEnumerable<LichThi> getMulti(string keyword,int kithiId);
	}

    public class LichThiRepository : RepositoryBase<LichThi>, ILichThiRepository
    {
        public LichThiRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public bool checkDuplicateAdd(LichThi lThi)
        {
			bool result = true;
			IEnumerable<LichThi> list = DbContext.LichThi.Where(s => s.PhongThiId == lThi.PhongThiId && s.NgayThi == lThi.NgayThi).ToList();
			if (list.Count() > 0)
				result = list.Any(s => (s.GioBatDau >= lThi.GioBatDau && s.GioBatDau <= lThi.GioKetThuc) ||
				(s.GioKetThuc >= lThi.GioBatDau && s.GioKetThuc <= lThi.GioKetThuc));
			else result = false;

            return result;
        }

		public bool checkDuplicateUp(LichThi lThi)
		{
			bool result = true;
			IEnumerable<LichThi> list = DbContext.LichThi.Where(s =>s.LichThiId != lThi.LichThiId && s.PhongThiId == lThi.PhongThiId && s.NgayThi == lThi.NgayThi).ToList();
			if (list.Count() > 0)
				result = list.Any(s => (s.GioBatDau >= lThi.GioBatDau && s.GioBatDau <= lThi.GioKetThuc) ||
				(s.GioKetThuc >= lThi.GioBatDau && s.GioKetThuc <= lThi.GioKetThuc));
			else result = false;

			return result;
		}
		public IEnumerable<LichThi> getMulti(string keyword,int kiThiId)
		{


			var query = from p in DbContext.LichThi
						join pt in DbContext.LopHocPhan
						on p.LophpId equals pt.LophpId
						where p.KiThiId == kiThiId
						select new
						{
							p,
							pt.Title,
							pt.Name
						};


			query = query.Where(x => x.Title.ToUpper().Contains(keyword.ToUpper()) || x.Name.Contains(keyword));
			IEnumerable<LichThi> a = query.Select(x => x.p);
			//var a = query.DistinctBy(x => x.LophpId).AsEnumerable<LopHocPhan>(); ;
			return a;

		}

		//Lay danh sach lich thi chua dang ky cua mot sinh vien
		public IEnumerable<LichThi> getMultibySv(int id)
		{
			var query = from lt in DbContext.LichThi
						join svlhp in DbContext.SinhVienLophp
						on lt.LophpId equals svlhp.LophpId
						where svlhp.DuDieuKien == true && svlhp.SinhVienId == id
						select lt;
			return query;
		}

		public IEnumerable<LichThi> getDaDky(int id)
		{
			var query = from lt in DbContext.LichThi
						join svlt in DbContext.SinhVienLichThi
						on lt.LichThiId equals svlt.LichThiId
						where svlt.SinhVienId == id
						select lt;
			return query;
		}
	}
}
