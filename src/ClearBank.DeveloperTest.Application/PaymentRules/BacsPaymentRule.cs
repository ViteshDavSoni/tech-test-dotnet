using ClearBank.DeveloperTest.Application.Dtos;
using ClearBank.DeveloperTest.Domain.Entities;
using ClearBank.DeveloperTest.Domain.Enums;

namespace ClearBank.DeveloperTest.Application.PaymentRules;

public class BacsPaymentRule : IPaymentRule
{
    public PaymentScheme Scheme => PaymentScheme.Bacs;

    public MakePaymentResult MakePayment(Account account, MakePaymentRequest request)
    {
        var result = new MakePaymentResult();

        if (account == null || !account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
        {
            result.Success = false;
            return result;
        }

        account.Balance -= request.Amount;
        result.Success = true;
        return result;
    }
}