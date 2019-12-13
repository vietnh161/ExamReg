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
	public interface ILichThiService
	{
		void Add(LichThi lichThi);
		void Update(LichThi lichThi);
		LichThi Delete(int id);
		LichThi GetById(int id);
		IEnumerable<LichThi> GetAll();
		IEnumerable<LichThi> GetAllBySv(int sinhVienId);
		IEnumerable<LichThi> GetDaDky(int svId);
		LichThiProp GetAllProp(int kiThiId);
		IEnumerable<LichThi> GetMultiPaging(int page, int pageSize, string sort, string sortBy, string keyword, int kiThiId, out int totalRow);
		bool checkDuplicate(LichThi lichThi);
		//void changeCount(int id);
		void SaveChanges();
	}
	public class LichThiService : ILichThiService
	{
		ILichThiRepository _lichThiRepository;
		IPhongThiRepository _phongThiRepository;
		ILopHocPhanRepository _lopHocPhanRepository;
		ICaThiRepository _caThiRepository;
		ISinhVienLichThiRepository _sinhVienLichThiRepository;
		IUnitOfWork _unitOfWork;

		public LichThiService(ILichThiRepository lichThi, IPhongThiRepository phongThi, ILopHocPhanRepository lopHocPhan, ICaThiRepository caThi, ISinhVienLichThiRepository _sinhVienLichThiRepository, IUnitOfWork unitOfWork)
		{
			this._lichThiRepository = lichThi;
			this._phongThiRepository = phongThi;
			this._lopHocPhanRepository = lopHocPhan;
			this._caThiRepository = caThi;
			this._sinhVienLichThiRepository = _sinhVienLichThiRepository;
			this._unitOfWork = unitOfWork;
		}

		public void Add(LichThi lichThi)
		{
			_lichThiRepository.Add(lichThi);
		}

		public void Update(LichThi lichThi)
		{
			_lichThiRepository.Update(lichThi);
		}

		public LichThi Delete(int id)
		{
			_sinhVienLichThiRepository.DeleteMulti(x => x.LichThiId == id);
			return _lichThiRepository.Delete(id);
		}

		public IEnumerable<LichThi> GetAll()
		{
			var result = _lichThiRepository.GetAll();
			foreach (var item in result)
			{
				item.CaThi = _caThiRepository.GetSingleById(item.CaThiId);
				item.PhongThi = _phongThiRepository.GetSingleById(item.PhongThiId);
				item.LopHocPhan = _lopHocPhanRepository.GetSingleById(item.LophpId);
			}

			return result;
		}

		public LichThi GetById(int id)
		{
			var result = _lichThiRepository.GetSingleById(id);
			result.CaThi = _caThiRepository.GetSingleById(result.CaThiId);
			result.PhongThi = _phongThiRepository.GetSingleById(result.PhongThiId);
			result.LopHocPhan = _lopHocPhanRepository.GetSingleById(result.LophpId);
			return result;
		}

		public bool checkDuplicate(LichThi lichThi)
		{
			if(lichThi.LichThiId == (-1))
			{
				return _lichThiRepository.checkDuplicateAdd(lichThi);
			}
			else
			{
				return _lichThiRepository.checkDuplicateUp(lichThi);
			}
		}

		public void SaveChanges()
		{
			_unitOfWork.Commit();
		}


		public IEnumerable<LichThi> GetMultiPaging(int page, int pageSize, string sort, string sortBy, string keyword, int kiThiId, out int totalRow)
		{

			IEnumerable<LichThi> query = null;
			if (keyword.Equals("null"))
			{
				query = _lichThiRepository.GetMulti(x => x.KiThiId == kiThiId);
			}
			else
			{
				query = _lichThiRepository.getMulti(keyword, kiThiId);
			}

			if (sortBy.Equals("DESC"))
			{
				switch (sort)
				{
					case ("count"):
						query = query.OrderByDescending(x => x.Count);
						break;
					case ("phongthi"):
						query = query.OrderByDescending(x => x.PhongThiId);
						break;
					default:
						query = query.OrderByDescending(x => x.NgayThi);
						break;
				}

			}
			else
			{
				switch (sort)
				{
					case ("count"):
						query = query.OrderBy(x => x.Count);
						break;
					case ("phongthi"):
						query = query.OrderBy(x => x.PhongThiId);
						break;
					default:
						query = query.OrderBy(x => x.NgayThi);
						break;
				}
			}
			foreach(var item in query)
			{
				item.PhongThi = _phongThiRepository.GetSingleById(item.PhongThiId);
				item.CaThi = _caThiRepository.GetSingleById(item.CaThiId);
				item.LopHocPhan = _lopHocPhanRepository.GetSingleById(item.LophpId);
			}


			totalRow = query.Count();

			return query.Skip((page - 1) * pageSize).Take(pageSize);
		}

		public LichThiProp GetAllProp(int kiThiId)
		{
			LichThiProp lichThiProp = new LichThiProp();
			lichThiProp.caThi = _caThiRepository.GetAll();
			lichThiProp.phongThi = _phongThiRepository.GetAll();
			lichThiProp.lopHp = _lopHocPhanRepository.GetMulti(x=> x.KiThiId == kiThiId);

			return lichThiProp;
		}

		public IEnumerable<LichThi> GetAllBySv(int svId)
		{
			IEnumerable<LichThi> list = _lichThiRepository.getMultibySv(svId);
			foreach(var item in list)
			{
				item.PhongThi = _phongThiRepository.GetSingleById(item.PhongThiId);
				item.CaThi = _caThiRepository.GetSingleById(item.CaThiId);
				item.LopHocPhan = _lopHocPhanRepository.GetSingleById(item.LophpId);
			}
			return list;
		}


		public IEnumerable<LichThi> GetDaDky(int svId)
		{
			IEnumerable<LichThi> list = _lichThiRepository.getDaDky(svId);
			foreach (var item in list)
			{
				item.PhongThi = _phongThiRepository.GetSingleById(item.PhongThiId);
				item.CaThi = _caThiRepository.GetSingleById(item.CaThiId);
				item.LopHocPhan = _lopHocPhanRepository.GetSingleById(item.LophpId);
			}
			return list;
		}
	}

	public class LichThiProp
	{
		public IEnumerable<CaThi> caThi { get; set; }
		public IEnumerable<PhongThi> phongThi { get; set; }
		public IEnumerable<LopHocPhan> lopHp { get; set; }

	}

}


