namespace ClearBank.DeveloperTest.Application.Dtos
{
    public class MakePaymentResult
    {
        public bool Success { get; set; }

        public MakePaymentResult(bool success = false)
        {
            Success = success;
        }
    }
}
