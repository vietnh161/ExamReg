using ExamReg.Data.Infrastructure;
using ExamReg.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Data.Repositories
{

    public interface ISinhVienRepository : IRepository<SinhVien>
    {

    }

    public class SinhVienRepository : RepositoryBase<SinhVien>, ISinhVienRepository
    {
        public SinhVienRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
