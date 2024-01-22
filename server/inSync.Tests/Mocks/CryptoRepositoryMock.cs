using inSync.Domain.Abstractions;
using inSync.Domain.Models;
using Moq;

namespace inSync.Tests.Mocks;

public static class CryptoRepositoryMock
{
    public static void VerifyHashReturnsTrue(this Mock<ICryptoRepository> mock)
    {
        mock.Setup(x => x.VerifyHash(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(true);
    }

    public static void VerifyHashReturnsFalse(this Mock<ICryptoRepository> mock)
    {
        mock.Setup(x => x.VerifyHash(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(false);
    }
}