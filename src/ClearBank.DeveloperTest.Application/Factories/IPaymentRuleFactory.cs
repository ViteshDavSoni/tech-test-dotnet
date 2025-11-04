using ClearBank.DeveloperTest.Application.PaymentRules;
using ClearBank.DeveloperTest.Domain.Enums;

namespace ClearBank.DeveloperTest.Application.Factories;

public interface IPaymentRuleFactory
{
    IPaymentRule GetRule(PaymentScheme scheme);
}