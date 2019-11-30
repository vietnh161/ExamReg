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
	public interface ISinhVienLichThiService
	{
		void Add(SinhVienLichThi sinhVien);
		void Update(SinhVienLichThi sinhVien);
		SinhVienLichThi Delete(int id);
		SinhVienLichThi GetById(int id);
		SinhVienLichThi GetByCondition(Expression<Func<SinhVienLichThi, bool>> expression, string[] includes = null);
		IEnumerable<SinhVienLichThi> GetMulti(Expression<Func<SinhVienLichThi, bool>> expression, string[] includes = null);
		IEnumerable<SinhVienLichThi> GetAll();
		void SaveChanges();
	}
	public class SinhVienLichThiService : ISinhVienLichThiService
	{
		ISinhVienLichThiRepository _sinhVienLichThiRepository;
		ISinhVienRepository _sinhVienRepository;
		ILichThiRepository _lichThiRepository;

		IUnitOfWork _unitOfWork;

		public SinhVienLichThiService(ISinhVienLichThiRepository sinhVienLichThiRepository, ISinhVienRepository sinhVien, ILichThiRepository lichThi, IUnitOfWork unitOfWork)
		{
			this._sinhVienLichThiRepository = sinhVienLichThiRepository;
			this._sinhVienRepository = sinhVien;
			this._lichThiRepository = lichThi;
			this._unitOfWork = unitOfWork;
		}

		public void Add(SinhVienLichThi sinhVienLichThi)
		{
			_sinhVienLichThiRepository.Add(sinhVienLichThi);
		}

		public void Update(SinhVienLichThi sinhVienLichThi)
		{
			_sinhVienLichThiRepository.Update(sinhVienLichThi);
		}

		public SinhVienLichThi Delete(int id)
		{
			return _sinhVienLichThiRepository.Delete(id);
		}

		public IEnumerable<SinhVienLichThi> GetAll()
		{
			var result = _sinhVienLichThiRepository.GetAll();
			foreach (var item in result)
			{
				item.SinhVien = _sinhVienRepository.GetSingleById(item.SinhVienId);
				item.LichThi = _lichThiRepository.GetSingleById(item.LichThiId);
			}

			return result;
		}

		public SinhVienLichThi GetById(int id)
		{
			var result = _sinhVienLichThiRepository.GetSingleById(id);
			result.SinhVien = _sinhVienRepository.GetSingleById(result.SinhVienId);
			result.LichThi = _lichThiRepository.GetSingleById(result.LichThiId);
			return result;
		}

		public void SaveChanges()
		{
			_unitOfWork.Commit();
		}

		public SinhVienLichThi GetByCondition(Expression<Func<SinhVienLichThi, bool>> expression, string[] includes = null)
		{
			return _sinhVienLichThiRepository.GetSingleByCondition(expression,includes);
		}

		public IEnumerable<SinhVienLichThi> GetMulti(Expression<Func<SinhVienLichThi, bool>> expression, string[] includes = null)
		{
			IEnumerable<SinhVienLichThi> list = _sinhVienLichThiRepository.GetMulti(expression, includes);
			foreach (var item in list)
			{
				item.SinhVien = _sinhVienRepository.GetSingleById(item.SinhVienId);
				item.LichThi = _lichThiRepository.GetSingleById(item.LichThiId);
			}
			return list;
		}
	}
}
