using CodeflixCatalog.Application.Interfaces;
using Moq;

namespace CodeflixCatalog.UnitTests.Application.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest
{
    private readonly CreateCategoryTestFixture _fixture;

    public CreateCategoryTest(CreateCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategory()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        
        var useCase = new UseCases.CreateCategory(
            repositoryMock.Object, 
            unitOfWorkMock.Object
            );

        
        var input = _fixture.GetInput();

        var output = await useCase.Handle(input, CancellationToken.None);
        
        repositoryMock.Verify(repository => repository.Insert(
                It.IsAny<DomainEntity.Category>(), 
                It.IsAny<CancellationToken>()),
            Times.Once
            );
        unitOfWorkMock.Verify(unitOfWork => unitOfWork.Commit(
            It.IsAny<CancellationToken>()), 
            Times.Once
            );
        
        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.Id.Should().NotBeEmpty();
        output.IsActive.Should().Be(input.IsActive);
        (output.CreatedAt != default).Should().BeTrue();
        
    }
}