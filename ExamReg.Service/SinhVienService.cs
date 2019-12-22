using ExamReg.Data.Infrastructure;
using ExamReg.Data.Repositories;
using ExamReg.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Service
{
	public interface ISinhVienService
	{
		void Add(SinhVien sinhVien);
		void Update(SinhVien sinhVien);
		SinhVien Delete(int id);
		SinhVien GetById(int id);
		SinhVien GetByUserId(string userId);
		List<LopHocPhan> GetLopHocPhan(int sinhVienId);
		List<LichThi> GetLichThi(int sinhVienId);
		SinhVien GetByConDition(Expression<Func<SinhVien, bool>> expression);
		IEnumerable<SinhVien> GetMultiPaging(int page, int pageSize, string sort,string sortBy,string keyword, out int totalRow);
		IEnumerable<SinhVien> GetMultiPaging(int page, int pageSize,int lichThiId, string sort, string sortBy, string keyword, out int totalRow);
		IEnumerable<SinhVien> GetMultiByLichThi( int lichThiId, out int totalRow);
		IEnumerable<SinhVien> GetAll();
		void SaveChanges();
		bool checkMssv(string mssv);
		bool checkDuplicateUpdate(SinhVien sinhVien);
	}
	public class SinhVienService : ISinhVienService
	{
		ISinhVienRepository _sinhVienRepository;
		ILopHocPhanRepository _lopHocPhanRepository;
		ISinhVienLophpRepository _sinhVienLophpRepository;
		ILichThiRepository _lichThiRepository;
		ISinhVienLichThiRepository _sinhVienLichThiRepository;

		IUnitOfWork _unitOfWork;

		public SinhVienService(ISinhVienRepository sinhVienRepository, ILichThiRepository lichThiRepository, 
			ILopHocPhanRepository lopHocPhanRepository, ISinhVienLichThiRepository sinhVienLichThiRepository,
			ISinhVienLophpRepository sinhVienLophpRepository, IUnitOfWork unitOfWork)
		{
			this._sinhVienRepository = sinhVienRepository;
			this._lopHocPhanRepository = lopHocPhanRepository;
			this._sinhVienLophpRepository = sinhVienLophpRepository;
			this._lichThiRepository = lichThiRepository;
			this._sinhVienLichThiRepository = sinhVienLichThiRepository;

			this._unitOfWork = unitOfWork;
		}

		public void Add(SinhVien sinhVien)
		{
			_sinhVienRepository.Add(sinhVien);
		}

		public SinhVien Delete(int id)
		{
			_sinhVienLophpRepository.DeleteMulti(x => x.SinhVienId == id);
			_sinhVienLichThiRepository.DeleteMulti(x => x.SinhVienId == id);


			return _sinhVienRepository.Delete(id);
		}

		public IEnumerable<SinhVien> GetAll()
		{
			return _sinhVienRepository.GetAll();
		}

		public SinhVien GetByUserId(string userId)
		{
			return _sinhVienRepository.GetSingleByCondition(x => x.UserId == userId);
		}

		public SinhVien GetById(int id)
		{

			return _sinhVienRepository.GetSingleById(id);
		}

		public void SaveChanges()
		{
			_unitOfWork.Commit();
		}

		public void Update(SinhVien sinhVien)
		{
			_sinhVienRepository.Update(sinhVien);
		}

		public List<LopHocPhan> GetLopHocPhan(int sinhVienId)
		{
			List<LopHocPhan> result = new List<LopHocPhan>();
			IEnumerable<SinhVienLophp> list = _sinhVienLophpRepository.GetMulti(x => x.SinhVienId == sinhVienId);
			if (list.Count() > 0)
			{
				foreach (var item in list)
				{
					result.Add(_lopHocPhanRepository.GetSingleById(item.LophpId));
				}
				return result;
			}
			return result;
		}
		public List<LichThi> GetLichThi(int sinhVienId)
		{
			List<LichThi> result = new List<LichThi>();
			IEnumerable<SinhVienLichThi> list = _sinhVienLichThiRepository.GetMulti(x => x.SinhVienId == sinhVienId);
			if (list.Count() > 0)
			{
				foreach (var item in list)
				{
					result.Add(_lichThiRepository.GetSingleById(item.LichThiId));
				}
				return result;
			}
			return result;
		}
		public bool checkMssv(string mssv)
		{
			int count = _sinhVienRepository.Count(x => x.MSSV == mssv);
			if(count > 0)
			return true;
			return false;
		}
		public bool checkDuplicateUpdate(SinhVien sinhVien)
		{
			int count = _sinhVienRepository.Count(x => x.MSSV == sinhVien.MSSV && x.SinhVienId == sinhVien.SinhVienId);
			if (count > 0) return true;
			return false;
		}

		public SinhVien GetByConDition(Expression<Func<SinhVien, bool>> expression)
		{
			return _sinhVienRepository.GetSingleByCondition(expression);
		}

		public IEnumerable<SinhVien> GetMultiPaging(int page, int pageSize, string sort,string sortBy,string keyword, out int totalRow)
		{
			IEnumerable<SinhVien> query = null;
			if (keyword.Equals("null"))
			{
				 query = _sinhVienRepository.GetAll();
			}
			else
			{
				 query = _sinhVienRepository.GetMulti(x => x.FullName.ToUpper().Contains(keyword.ToUpper()) || x.MSSV.Contains(keyword));
			}
			
			if (sortBy.Equals("DESC"))
			{
				switch (sort)
				{
					case "fullName":
						query = query.OrderByDescending(x => Convert.ToString(x.FullName)).ToList();
						break;
					default:
						query = query.OrderByDescending(x => x.MSSV);
						break;
				}
			}
			else
			{
				switch (sort)
				{
					case "fullName":
						query = query.OrderBy(x => Convert.ToString(x.FullName)).ToList();
						break;
					default:
						query = query.OrderBy(x => x.MSSV, StringComparer.CurrentCultureIgnoreCase);
						break;
				}
			}
			totalRow = query.Count();

			return query.Skip((page - 1) * pageSize).Take(pageSize);
		}

		public IEnumerable<SinhVien> GetMultiPaging(int page, int pageSize,int lichThiId, string sort, string sortBy, string keyword, out int totalRow)
		{

			IEnumerable<SinhVien> query = _sinhVienLichThiRepository.getMultiSvbyId(lichThiId);
			if (keyword.Equals("null"))
			{
				
			}
			else
			{
				query = query.Where(x => x.FullName.ToUpper().Contains(keyword.ToUpper()) || x.MSSV.Contains(keyword));
			}

			if (sortBy.Equals("DESC"))
			{
				switch (sort)
				{
					case "fullName":
						query = query.OrderByDescending(x => Convert.ToString(x.FullName)).ToList();
						break;
					default:
						query = query.OrderByDescending(x => x.MSSV);
						break;
				}
			}
			else
			{
				switch (sort)
				{
					case "fullName":
						query = query.OrderBy(x => Convert.ToString(x.FullName)).ToList();
						break;
					default:
						query = query.OrderBy(x => x.MSSV, StringComparer.CurrentCultureIgnoreCase);
						break;
				}
			}
			totalRow = query.Count();

			return query.Skip((page - 1) * pageSize).Take(pageSize);
		}

		public IEnumerable<SinhVien> GetMultiByLichThi(int lichThiId, out int totalRow)
		{
			IEnumerable<SinhVien> query = _sinhVienLichThiRepository.getMultiSvbyId(lichThiId);
			totalRow = query.Count();
			return query;
		}
	}
}
