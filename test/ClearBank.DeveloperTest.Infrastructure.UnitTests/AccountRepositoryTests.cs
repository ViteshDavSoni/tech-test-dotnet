using ClearBank.DeveloperTest.Infrastructure.Repositories;
using FluentAssertions;

namespace ClearBank.DeveloperTest.Infrastructure.UnitTests;

[TestClass]
public class AccountRepositoryTests
{
    [TestMethod]
    public void GetAccount_WithValidId_ReturnsAccountWithSameId()
    {
        var accountRepository = new AccountRepository();
        var accountNumber = "accountNumber";
        var result = accountRepository.GetAccount(accountNumber);

        result.AccountNumber.Should().Be(accountNumber);
    }
}