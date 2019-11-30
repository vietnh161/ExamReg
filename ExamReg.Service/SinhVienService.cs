using ExamReg.Data.Infrastructure;
using ExamReg.Data.Repositories;
using ExamReg.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
		IEnumerable<SinhVien> GetAll();
		void SaveChanges();
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
	}
}
