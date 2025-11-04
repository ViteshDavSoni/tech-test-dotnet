using ClearBank.DeveloperTest.Application.Dtos;
using ClearBank.DeveloperTest.Domain.Entities;
using ClearBank.DeveloperTest.Domain.Enums;

namespace ClearBank.DeveloperTest.Application.PaymentRules;

public class FasterPaymentsRule : PaymentRule
{
    public override PaymentScheme Scheme => PaymentScheme.FasterPayments;

    public override bool ValidatePayment(Account account, MakePaymentRequest request)
        => account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) && 
           account.Balance > request.Amount;
}