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
	public interface IHocPhanService
	{
		void Add(LopHocPhan LopHocPhan);
		void Update(LopHocPhan LopHocPhan);
		LopHocPhan Delete(int id);
		LopHocPhan GetById(int id);
		IEnumerable<LopHocPhan> GetAll();
		IEnumerable<LopHocPhan> GetMulti(Expression<Func<LopHocPhan, bool>> expression);
		bool checkDuplicate(string maHocPhan);
		IEnumerable<LopHocPhan> GetMultiPaging(int page, int pageSize, int kithiId, string keyword, out int totalRow);
		IEnumerable<LopHocPhan> GetMultiPaging(int page, int pageSize, int monThiId, int kithi, string keyword, out int totalRow);
		LopHocPhan GetByConDition(Expression<Func<LopHocPhan, bool>> expression);
		//void changeCount(int id);
		void SaveChanges();
	}

	public class HocPhanService : IHocPhanService
	{

		ILopHocPhanRepository _lopHocPhanRepository;
		ISinhVienLophpRepository _sinhVienLophpRepository;
		IMonThiRepository _monThiRepository;
		IUnitOfWork _unitOfWork;

		public HocPhanService(ILopHocPhanRepository _lopHocPhanRepository, IMonThiRepository _monThiRepository, ISinhVienLophpRepository sinhVienLophpRepository,
		IUnitOfWork _unitOfWork)
		{
			this._sinhVienLophpRepository = sinhVienLophpRepository;
			this._lopHocPhanRepository = _lopHocPhanRepository;
			this._monThiRepository = _monThiRepository;
			this._unitOfWork = _unitOfWork;
		}

		public void Add(LopHocPhan LopHocPhan)
		{
			_lopHocPhanRepository.Add(LopHocPhan);
		}

		public bool checkDuplicate(string maHocPhan)
		{
			if (_lopHocPhanRepository.Count( x => x.Title == maHocPhan) > 0) return true;
			return false;
		}
		public IEnumerable<LopHocPhan> GetMulti(Expression<Func<LopHocPhan, bool>> expression)
		{
			return _lopHocPhanRepository.GetMulti(expression);
		}

		public LopHocPhan Delete(int id)
		{
			_sinhVienLophpRepository.DeleteMulti(x => x.LophpId == id);
			return _lopHocPhanRepository.Delete(id);
		}

		public IEnumerable<LopHocPhan> GetAll()
		{
			var list = _lopHocPhanRepository.GetAll();
			foreach (var item in list)
			{
				item.MonThi = _monThiRepository.GetSingleById(item.MonThiId);
			}
			return list;
		}

		public LopHocPhan GetByConDition(Expression<Func<LopHocPhan, bool>> expression)
		{
			return _lopHocPhanRepository.GetSingleByCondition(expression);
		}

		public LopHocPhan GetById(int id)
		{
			LopHocPhan lopHocPhan = _lopHocPhanRepository.GetSingleById(id);
			MonThi monThi = _monThiRepository.GetSingleById(lopHocPhan.MonThiId);
			lopHocPhan.MonThi = monThi;
			return lopHocPhan;
		}

		public void SaveChanges()
		{
			_unitOfWork.Commit();
		}

		public void Update(LopHocPhan LopHocPhan)
		{
			_lopHocPhanRepository.Update(LopHocPhan);
		}
		public IEnumerable<LopHocPhan> GetMultiPaging(int page, int pageSize,int monThiId, int kithiId, string keyword, out int totalRow)
		{

			IEnumerable<LopHocPhan> result = _lopHocPhanRepository.GetMulti(x=> x.KiThiId == kithiId && x.MonThiId == monThiId);
			if (keyword.Equals("null"))
			{
				
				totalRow = result.Count();
				return result.Skip((page - 1) * pageSize).Take(pageSize);
			}
			else
			{
				result = result.Where(x => x.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) != -1 || x.Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) != -1);
				totalRow = result.Count();
				return result.Skip((page - 1) * pageSize).Take(pageSize);
			}

		}
		public IEnumerable<LopHocPhan> GetMultiPaging(int page, int pageSize, int kithiId, string keyword, out int totalRow)
		{

			IEnumerable<LopHocPhan> result = _lopHocPhanRepository.GetMulti(x => x.KiThiId == kithiId);
			if (keyword.Equals("null"))
			{

				totalRow = result.Count();
				return result.Skip((page - 1) * pageSize).Take(pageSize);
			}
			else
			{
				result = result.Where(x => x.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) != -1 || x.Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) != -1);
				totalRow = result.Count();
				return result.Skip((page - 1) * pageSize).Take(pageSize);
			}

		}
	}
}
