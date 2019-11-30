using System;

namespace ExamReg.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ExamRegDbContext Init();
    }
}