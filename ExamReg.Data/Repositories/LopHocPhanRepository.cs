using ExamReg.Data.Infrastructure;
using ExamReg.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Data.Repositories
{
    public interface ILopHocPhanRepository : IRepository<LopHocPhan>
    {

    }

    public class LopHocPhanRepository : RepositoryBase<LopHocPhan>, ILopHocPhanRepository
    {
        public LopHocPhanRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
