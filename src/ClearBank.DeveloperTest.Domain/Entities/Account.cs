namespace ClearBank.DeveloperTest.Domain.Entities
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public AllowedPaymentSchemes AllowedPaymentSchemes { get; set; }

        public Account(string accountNumber, decimal balance, AccountStatus status,
            AllowedPaymentSchemes allowedPaymentSchemes)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            Status = status;
            AllowedPaymentSchemes = allowedPaymentSchemes;
        }

        public static Account CreateNewAccount(string accountNumber) => 
            new Account(accountNumber, 0, AccountStatus.Live, AllowedPaymentSchemes.Bacs);
    }
}
