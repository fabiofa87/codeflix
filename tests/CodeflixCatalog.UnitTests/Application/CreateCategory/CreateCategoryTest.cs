
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

    [Theory(DisplayName = nameof(TrhowWhenCantInstantiateAggreagete))]
    [Trait("Application", "CreateCategory - Use Cases")]
    [MemberData(nameof(GetInvalidInputs))]
    public async void TrhowWhenCantInstantiateAggreagete(UseCases.CreateCategoryInput input, 
        string exceptionMessage)
    {
        var useCase = new UseCases.CreateCategory(
            _fixture.GetRepositoryMock().Object, 
            _fixture.GetUnitOfWorkMock().Object
        );
        

        Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<EntityValidationException>().WithMessage(exceptionMessage);
    }

    public static IEnumerable<object[]> GetInvalidInputs()
    {
        var fixture = new CreateCategoryTestFixture();
        var invalidInputList = new List<object[]>();

        // name cannot be less than 3 chars
        var invalidInputShortName = fixture.GetInput();
        invalidInputShortName.Name =
            invalidInputShortName.Name.Substring(0, 2);

        invalidInputList.Add(new object[]
        {
            invalidInputShortName,
            "Name should not be less than 3 characters"
        });

        // name cannot be higher than 255 chars
        var invalidInputLongName = fixture.GetInput();
        var categoryNameTooLong = fixture.Faker.Commerce.ProductName();
        
        while (categoryNameTooLong.Length < 256)
            categoryNameTooLong = $"{categoryNameTooLong} {fixture.Faker.Commerce.ProductName()}";
        
        invalidInputLongName.Name =
            invalidInputLongName.Name +
            new string('a', 255 - invalidInputLongName.Name.Length + 1);

        invalidInputList.Add(new object[]
        {
            invalidInputLongName,
            "Name should not be greater than 255 characters"
        });
        
        // description cannot be null
            
        var invalidInputNullDescription = fixture.GetInput();
        
        
        
    return invalidInputList;
    }
}