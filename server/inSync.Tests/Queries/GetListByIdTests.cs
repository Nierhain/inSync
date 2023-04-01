using FluentAssertions;
using inSync.Application.Errors;
using inSync.Application.Models;
using inSync.Application.Queries.User;
using inSync.Tests.Mocks;
using Moq;

namespace inSync.Tests.Queries;

public class GetListByIdTests
{
    private readonly Mock<IDbRepository> _repository;
    private readonly Mock<ICryptoRepository> _crypto;

    private readonly Guid _defaultGuid = Guid.NewGuid();
    private readonly Credentials _defaultCredentials = new(){Username = "default", Password = "default"};

    public GetListByIdTests()
    {
        _repository = new Mock<IDbRepository>();
        _crypto = new Mock<ICryptoRepository>();
    }
    
    [Fact]
    public async Task Validate_Should_ReturnError_WhenIdDoesNotExist()
    {
        //Arrange
        _repository.ItemListExistsReturnsFalse();
        _crypto.VerifyHashReturnsTrue();
        
        var sut = new GetListByIdValidator(_repository.Object, _crypto.Object);
        var request = new GetListById(Guid.NewGuid(), _defaultCredentials);
        
        //Act
        var result = await sut.Validate(request);
        
        //Assert
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().Be(Errors.ItemLists.ListDoesNotExist);
    }
    
    [Fact]
    public async Task Validate_Should_ReturnError_WhenPasswordIsIncorrect()
    {

        //Arrange
        _repository.ItemListExistsReturnsTrue();
        _repository.GetItemListReturnsListWithId(_defaultGuid);
        _crypto.VerifyHashReturnsFalse();
        
        var sut = new GetListByIdValidator(_repository.Object, _crypto.Object);
        var request = new GetListById(_defaultGuid, _defaultCredentials);
        
        //Act
        var result = await sut.Validate(request);
        
        //Assert
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().Be(Errors.Users.WrongCredentials);
    }
    
    [Fact]
    public async Task Validate_Should_ReturnError_WhenUserIsWrong()
    {
        //Arrange
        _repository.ItemListExistsReturnsTrue();
        _repository.GetItemListReturnsListWithUser(_defaultGuid);
        _crypto.VerifyHashReturnsTrue();

        var sut = new GetListByIdValidator(_repository.Object, _crypto.Object);
        var request = new GetListById(_defaultGuid, _defaultCredentials);
        
        //Act
        var result = await sut.Validate(request);
        
        //Assert
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().Be(Errors.Users.WrongCredentials);
    }
}