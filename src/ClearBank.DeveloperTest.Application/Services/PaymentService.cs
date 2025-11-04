using ClearBank.DeveloperTest.Application.Dtos;
using ClearBank.DeveloperTest.Application.Factories;
using ClearBank.DeveloperTest.Domain.Repositories;

namespace ClearBank.DeveloperTest.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPaymentRuleFactory _paymentRuleFactory;

    public PaymentService(IAccountRepository accountRepository, IPaymentRuleFactory paymentRuleFactory)
    {
        _accountRepository = accountRepository;
        _paymentRuleFactory = paymentRuleFactory;
    }

    public MakePaymentResult MakePayment(MakePaymentRequest request)
    {
        var account = _accountRepository.GetAccount(request.DebtorAccountNumber);
        var rule = _paymentRuleFactory.GetRule(request.PaymentScheme);

        if (account == null)
        {
            return new MakePaymentResult(false);
        }
        
        var result = rule.MakePayment(account, request);
        if (result.Success)
        {
            _accountRepository.UpdateAccount(account);
        }
        
        return result;
    }
}


