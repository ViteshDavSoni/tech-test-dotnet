using ClearBank.DeveloperTest.Domain.Entities;
using ClearBank.DeveloperTest.Domain.Enums;

namespace ClearBank.DeveloperTest.Domain.PaymentRules;

public class FasterPaymentsRule : PaymentRule
{
    public override PaymentScheme Scheme => PaymentScheme.FasterPayments;

    public override bool ValidatePayment(Account account, AccountStatus accountStatus, decimal requestAmount)
        => account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) && 
           account.Balance > requestAmount;
}