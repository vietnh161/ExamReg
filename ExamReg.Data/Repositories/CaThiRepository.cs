using ExamReg.Data.Infrastructure;
using ExamReg.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Data.Repositories
{
    public interface ICaThiRepository : IRepository<CaThi>
    {

    }

    public class CaThiRepository : RepositoryBase<CaThi>, ICaThiRepository
    {
        public CaThiRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
