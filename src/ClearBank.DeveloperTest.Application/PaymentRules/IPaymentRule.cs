using ClearBank.DeveloperTest.Application.Dtos;
using ClearBank.DeveloperTest.Domain.Entities;
using ClearBank.DeveloperTest.Domain.Enums;

namespace ClearBank.DeveloperTest.Application.PaymentRules;

public interface IPaymentRule
{
    PaymentScheme Scheme { get; }
    MakePaymentResult MakePayment(Account account, MakePaymentRequest request);
}