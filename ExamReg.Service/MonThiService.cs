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
    public interface IMonThiService
    {
        void Add(MonThi monThi);
        void Update(MonThi monThi);
        MonThi Delete(int id);
        MonThi GetById(int id);
        IEnumerable<MonThi> GetAll();
        void SaveChanges();
    }
    public class MonThiService : IMonThiService
    {
        IMonThiRepository _monthiRepository;
        IUnitOfWork _unitOfWork;

        public MonThiService(IMonThiRepository monThi, IUnitOfWork unitOfWork)
        {
            this._monthiRepository = monThi;
            this._unitOfWork = unitOfWork;
        }

        public void Add(MonThi monThi)
        {
            _monthiRepository.Add(monThi);
        }

        public void Update(MonThi monThi)
        {
            _monthiRepository.Update(monThi);
        }

        public MonThi Delete(int id)
        {
            return _monthiRepository.Delete(id);
        }

        public IEnumerable<MonThi> GetAll()
        {
            var result = _monthiRepository.GetAll();
   

            return result;
        }

        public MonThi GetById(int id)
        {
            var result = _monthiRepository.GetSingleById(id);
           
            return result;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }


    }


}
