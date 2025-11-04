using ClearBank.DeveloperTest.Application.Dtos;
using ClearBank.DeveloperTest.Domain.Entities;
using ClearBank.DeveloperTest.Domain.Enums;

namespace ClearBank.DeveloperTest.Application.PaymentRules;

public class BacsPaymentRule : PaymentRule
{
    public override PaymentScheme Scheme => PaymentScheme.Bacs;
    
    public override bool ValidatePayment(Account account, MakePaymentRequest request)
        => account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);
}