using ClearBank.DeveloperTest.Domain.Entities;

namespace ClearBank.DeveloperTest.Application.Services
{
    public interface IPaymentService
    {
        MakePaymentResult MakePayment(MakePaymentRequest request);
    }
}
