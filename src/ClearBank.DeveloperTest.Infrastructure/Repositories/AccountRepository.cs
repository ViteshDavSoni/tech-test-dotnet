using ClearBank.DeveloperTest.Domain.Entities;
using ClearBank.DeveloperTest.Domain.Repositories;

namespace ClearBank.DeveloperTest.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Account? GetAccount(string accountNumber)
        {
            // Access database to retrieve account, code removed for brevity 
            return Account.CreateNewAccount(accountNumber);
        }

        public void UpdateAccount(Account account)
        {
            // Update account in database, code removed for brevity
        }
    }
}
