using Models;

namespace Data.IRepositories
{
    public interface IUserAnswerRepository : IBaseRepository<UserAnswer>
    {
        UserAnswer GetNextUserAnswer(int userTestId);
    }
}
