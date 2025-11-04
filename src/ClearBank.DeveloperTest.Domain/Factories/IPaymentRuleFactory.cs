using ClearBank.DeveloperTest.Domain.Enums;
using ClearBank.DeveloperTest.Domain.PaymentRules;

namespace ClearBank.DeveloperTest.Domain.Factories;

public interface IPaymentRuleFactory
{
    IPaymentRule GetRule(PaymentScheme scheme);
}