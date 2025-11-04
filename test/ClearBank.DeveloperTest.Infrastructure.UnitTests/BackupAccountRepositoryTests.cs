using ClearBank.DeveloperTest.Infrastructure.Repositories;
using FluentAssertions;

namespace ClearBank.DeveloperTest.Infrastructure.UnitTests;

[TestClass]
public class BackupAccountRepositoryTests
{
    [TestMethod]
    public void GetAccount_WithValidId_ReturnsBackupAccountWithSameId()
    {
        var backupAccountRepository = new BackupAccountRepository();
        var accountNumber = "accountNumber";
        var result = backupAccountRepository.GetAccount(accountNumber);

        result.AccountNumber.Should().Be(accountNumber);
    }
}