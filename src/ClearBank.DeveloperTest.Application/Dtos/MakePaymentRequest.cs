using ClearBank.DeveloperTest.Domain.Enums;

namespace ClearBank.DeveloperTest.Application.Dtos
{
    public class MakePaymentRequest
    {
        public string CreditorAccountNumber { get; set; }

        public string DebtorAccountNumber { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public PaymentScheme PaymentScheme { get; set; }

        public MakePaymentRequest(string creditorAccountNumber, string debtorAccountNumber, decimal amount,
            DateTime paymentDate, PaymentScheme paymentScheme)
        {
            CreditorAccountNumber = creditorAccountNumber;
            DebtorAccountNumber = debtorAccountNumber;
            Amount = amount;
            PaymentDate = paymentDate;
            PaymentScheme = paymentScheme;
        }
    }
}
