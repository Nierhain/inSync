using inSync.Application.Models.Dtos;
using inSync.Domain.ItemLists;
using inSync.Domain.Models;
using Moq;

namespace inSync.Tests.Mocks;

public static class DbRepositoryMock
{
    public static void ItemListExistsReturnsTrue(this Mock<IDbRepository> mock)
    {
        mock.Setup(x => x.Exists<ItemList>(It.IsAny<Guid>())).ReturnsAsync(true);
    }

    public static void ItemListExistsReturnsFalse(this Mock<IDbRepository> mock)
    {
        mock.Setup(x => x.Exists<ItemList>(It.IsAny<Guid>())).ReturnsAsync(false);
    }
    public static void GetItemListReturnsNull(this Mock<IDbRepository> mock)
    {
        ItemListDto list = null;
        mock.Setup(x => x.GetItemList(It.IsAny<Guid>())).ReturnsAsync(list);
    }

    public static void GetItemListReturnsListWithId(this Mock<IDbRepository> mock, Guid id)
    {
        mock.Setup(x => x.GetItemList(It.IsAny<Guid>())).ReturnsAsync(new ItemListDto {Id = id});
    }

    public static void GetItemListReturnsListWithUser(this Mock<IDbRepository> mock, Guid id)
    {
        mock.Setup(x => x.GetItemList(It.IsAny<Guid>())).ReturnsAsync(new ItemListDto{Id = id, Username = string.Empty});
    }
}