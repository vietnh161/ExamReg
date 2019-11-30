namespace ExamReg.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}