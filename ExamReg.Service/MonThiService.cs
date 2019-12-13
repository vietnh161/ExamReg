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
    public interface IMonThiService
    {
        void Add(MonThi monThi);
        void Update(MonThi monThi);
        MonThi Delete(int id);
        MonThi GetById(int id);
		IEnumerable<MonThi> GetMultiPaging(int page, int pageSize, string keyword, out int totalRow);
		MonThi GetByConDition(Expression<Func<MonThi, bool>> expression);
		bool checkDuplicate(string maMon);
		bool checkDuplicateUpdate(MonThi monThi);
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

		public bool checkDuplicate(string ma)
		{
			if (_monthiRepository.Count(x => x.Title == ma) > 0) return true;
			return false;
		}
		public bool checkDuplicateUpdate(MonThi monThi)
		{
			int count = _monthiRepository.Count(x => x.Title == monThi.Title && x.MonThiId == monThi.MonThiId);
			if (count > 0) return true;
			return false;
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

		public MonThi GetByConDition(Expression<Func<MonThi, bool>> expression)
		{
			return _monthiRepository.GetSingleByCondition(expression);
		}

		public IEnumerable<MonThi> GetMultiPaging(int page, int pageSize, string keyword, out int totalRow)
		{
			if(keyword != "null")
			{
				IEnumerable<MonThi> query = _monthiRepository.GetMulti(x => x.Name.ToUpper().Contains(keyword.ToUpper()) || x.Title.ToUpper().Contains(keyword.ToUpper()));

				totalRow = query.Count();

				return query.Skip((page - 1) * pageSize).Take(pageSize);
			}
			else
			{
				IEnumerable<MonThi> query = _monthiRepository.GetAll();

				totalRow = query.Count();

				return query.Skip((page - 1) * pageSize).Take(pageSize);
			}
			
		}
	}


}
