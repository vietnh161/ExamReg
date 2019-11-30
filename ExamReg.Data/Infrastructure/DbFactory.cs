using ExamReg.Data;

namespace ExamReg.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ExamRegDbContext dbContext;

        public ExamRegDbContext Init()
        {
            return dbContext ?? (dbContext = new ExamRegDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}