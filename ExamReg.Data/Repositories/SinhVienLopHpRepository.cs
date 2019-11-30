using ExamReg.Data.Infrastructure;
using ExamReg.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Data.Repositories
{
     public interface ISinhVienLophpRepository : IRepository<SinhVienLophp>
    {

    }

    public class SinhVienLophpRepository : RepositoryBase<SinhVienLophp>, ISinhVienLophpRepository
    {
        public SinhVienLophpRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
