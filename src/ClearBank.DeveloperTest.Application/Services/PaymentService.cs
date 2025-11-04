using ClearBank.DeveloperTest.Application.Dtos;
using ClearBank.DeveloperTest.Domain.Enums;
using ClearBank.DeveloperTest.Domain.Repositories;

namespace ClearBank.DeveloperTest.Application.Services;

public class PaymentService : IPaymentService
{
    private IAccountRepository _accountRepository;
    
    public PaymentService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public MakePaymentResult MakePayment(MakePaymentRequest request)
    { 
        var account = _accountRepository.GetAccount(request.DebtorAccountNumber);
        var result = new MakePaymentResult();

        result.Success = true;
        
        switch (request.PaymentScheme)
        {
            case PaymentScheme.Bacs:
                if (account == null)
                {
                    result.Success = false;
                }
                else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
                {
                    result.Success = false;
                }
                break;

            case PaymentScheme.FasterPayments:
                if (account == null)
                {
                    result.Success = false;
                }
                else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
                {
                    result.Success = false;
                }
                else if (account.Balance < request.Amount)
                {
                    result.Success = false;
                }
                break;

            case PaymentScheme.Chaps:
                if (account == null)
                {
                    result.Success = false;
                }
                else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
                {
                    result.Success = false;
                }
                else if (account.Status != AccountStatus.Live)
                {
                    result.Success = false;
                }
                break;
        }

        if (result.Success)
        {
            account.Balance -= request.Amount;
            _accountRepository.UpdateAccount(account);
        }

        return result;
    }
}

