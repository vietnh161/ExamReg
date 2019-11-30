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
        IUnitOfWork _unitOfWork;

        public LichThiService(ILichThiRepository lichThi, IPhongThiRepository phongThi, ILopHocPhanRepository lopHocPhan, ICaThiRepository caThi, IUnitOfWork unitOfWork)
        {
            this._lichThiRepository = lichThi;
            this._phongThiRepository = phongThi;
            this._lopHocPhanRepository = lopHocPhan;
            this._caThiRepository = caThi;
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
            return _lichThiRepository.checkDuplicate(lichThi);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

	}


}
