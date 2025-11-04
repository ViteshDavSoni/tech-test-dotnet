using ClearBank.DeveloperTest.Application.Dtos;
using ClearBank.DeveloperTest.Domain.Entities;
using ClearBank.DeveloperTest.Domain.Enums;

namespace ClearBank.DeveloperTest.Application.PaymentRules;

public class ChapsPaymentRule : IPaymentRule
{
    public PaymentScheme Scheme => PaymentScheme.Chaps;

    public MakePaymentResult MakePayment(Account account, MakePaymentRequest request)
    {
        var result = new MakePaymentResult();

        if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) ||
            account.Status != AccountStatus.Live)
        {
            result.Success = false;
            return result;
        }

        account.Balance -= request.Amount;
        result.Success = true;
        return result;
    }
}