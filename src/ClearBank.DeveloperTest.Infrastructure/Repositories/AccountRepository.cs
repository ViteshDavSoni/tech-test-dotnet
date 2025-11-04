using ClearBank.DeveloperTest.Domain.Entities;

namespace ClearBank.DeveloperTest.Infrastructure.Repositories
{
    public class AccountRepository
    {
        public Account GetAccount(string accountNumber)
        {
            // Access database to retrieve account, code removed for brevity 
            return new Account();
        }

        public void UpdateAccount(Account account)
        {
            // Update account in database, code removed for brevity
        }
    }
}
