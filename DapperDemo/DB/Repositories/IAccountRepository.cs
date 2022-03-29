using DapperDemo.Models.DTOs;

namespace DapperDemo.DB.Repositories
{
    public interface IAccountRepository
    {
        int SignUp(AccountDTO account);

        string Login(AccountDTO account);
    }
}
