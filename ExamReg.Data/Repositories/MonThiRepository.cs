using ExamReg.Data.Infrastructure;
using ExamReg.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamReg.Data.Repositories
{
    public interface IMonThiRepository : IRepository<MonThi>
    {

    }

    public class MonThiRepository : RepositoryBase<MonThi>, IMonThiRepository
    {
        public MonThiRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
