using ClearBank.DeveloperTest.Application.Dtos;

namespace ClearBank.DeveloperTest.Application.Services
{
    public interface IPaymentService
    {
        MakePaymentResult MakePayment(MakePaymentRequest request);
    }
}
