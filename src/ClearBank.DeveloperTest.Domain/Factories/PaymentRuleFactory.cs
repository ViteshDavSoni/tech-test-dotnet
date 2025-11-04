using ClearBank.DeveloperTest.Domain.Enums;
using ClearBank.DeveloperTest.Domain.PaymentRules;

namespace ClearBank.DeveloperTest.Domain.Factories;

public class PaymentRuleFactory : IPaymentRuleFactory
{
    private readonly Dictionary<PaymentScheme, IPaymentRule> _rules;

    public PaymentRuleFactory(IEnumerable<IPaymentRule> rules)
    {
        _rules = rules.ToDictionary(r => r.Scheme, r => r);
    }

    public IPaymentRule GetRule(PaymentScheme scheme)
    {
        if (!_rules.TryGetValue(scheme, out var rule))
            throw new InvalidOperationException($"No rule for scheme: {scheme}");
        return rule;
    }
}