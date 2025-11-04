using ClearBank.DeveloperTest.Domain.Entities;

namespace ClearBank.DeveloperTest.Domain.Repositories;

public interface IAccountRepository
{
    Account GetAccount(string accountNumber);
    void UpdateAccount(Account account);
}