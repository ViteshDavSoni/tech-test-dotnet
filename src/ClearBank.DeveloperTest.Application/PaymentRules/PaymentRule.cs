using ClearBank.DeveloperTest.Application.Dtos;
using ClearBank.DeveloperTest.Domain.Entities;
using ClearBank.DeveloperTest.Domain.Enums;

namespace ClearBank.DeveloperTest.Application.PaymentRules;

public abstract class PaymentRule : IPaymentRule
{
    public abstract PaymentScheme Scheme { get; }
    public MakePaymentResult MakePayment(Account account, MakePaymentRequest request)
    {
        var result = new MakePaymentResult();

        if (!ValidatePayment(account, request))
        {
            result.Success = false;
            return result;
        }

        account.Balance -= request.Amount;
        result.Success = true;
        return result;
    }

    public abstract bool ValidatePayment(Account account, MakePaymentRequest request);
}