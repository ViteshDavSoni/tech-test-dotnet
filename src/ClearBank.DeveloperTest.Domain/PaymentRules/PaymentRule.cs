using ClearBank.DeveloperTest.Domain.Entities;
using ClearBank.DeveloperTest.Domain.Enums;

namespace ClearBank.DeveloperTest.Domain.PaymentRules;

public abstract class PaymentRule : IPaymentRule
{
    public abstract PaymentScheme Scheme { get; }
    public bool MakePayment(Account account, AccountStatus accountStatus, decimal requestAmount)
    {
        if (!ValidatePayment(account, accountStatus, requestAmount))
        {
            return false;
        }

        account.Balance -= requestAmount;
        return true;
    }

    public abstract bool ValidatePayment(Account account, AccountStatus accountStatus, decimal requestAmount);
}