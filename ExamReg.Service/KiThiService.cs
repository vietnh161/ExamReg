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
	public interface IKiThiService
	{
		void Add(KiThi KiThi);
		void Update(KiThi KiThi);
		KiThi Delete(int id);
		KiThi GetById(int id);
		IEnumerable<KiThi> GetAll();
		void SaveChanges();
	}
	public class KiThiService : IKiThiService
	{
		IKiThiRepository _kiThiRepository;
		ILichThiRepository _lichThiRepository;
		ILopHocPhanRepository _lopHocPhanRepository;
		IUnitOfWork _unitOfWork;

		public KiThiService(IKiThiRepository KiThiRepository, IUnitOfWork unitOfWork, ILichThiRepository lichThiRepository,
		ILopHocPhanRepository lopHocPhanRepository)
		{
			this._kiThiRepository = KiThiRepository;
			this._lichThiRepository = lichThiRepository;
			this._lopHocPhanRepository = lopHocPhanRepository;
			this._unitOfWork = unitOfWork;
		}
		public void Add(KiThi kiThi)
		{
			kiThi.CreatedDate = DateTime.Now;
			 _kiThiRepository.Add(kiThi);
		}

		public KiThi Delete(int id)
		{

			_lopHocPhanRepository.DeleteMulti(x => x.KiThiId == id);
			_lichThiRepository.DeleteMulti(x => x.KiThiId == id);
			return _kiThiRepository.Delete(id);
		}

		public IEnumerable<KiThi> GetAll()
		{
			var result = _kiThiRepository.GetAll().OrderByDescending(x=> x.CreatedDate);
			return result;
		}

		public KiThi GetById(int id)
		{
			return _kiThiRepository.GetSingleById(id);
		}

		public void SaveChanges()
		{
			_unitOfWork.Commit();
		}


		public void Update(KiThi KiThi)
		{
			_kiThiRepository.Update(KiThi);
		}
	}
}
