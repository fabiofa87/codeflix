using CodeflixCatalog.Application.Interfaces;
using CodeflixCatalog.UnitTests.Common;
using Moq;

namespace CodeflixCatalog.UnitTests.Application.CreateCategory;

public class CreateCategoryTestFixture : FixtureBase
{
    public string GetValidDescription()
    {
        var description = Faker.Commerce.ProductDescription();
        
        if(description.Length > 10000) 
            description = description[..10000];
        return description;
    }
    
    public string GetValidCategoryName()
    {
        var categoryName = "";
        while(categoryName.Length < 3) 
            categoryName = Faker.Commerce.Categories(1)[0];
        if (categoryName.Length > 255)
            categoryName = categoryName[..255];
        return categoryName;
    }

    public bool GetRandomBoolean() => (new Random().NextDouble()) < 0.5;

    public UseCases.CreateCategoryInput GetInput() =>
        new(GetValidCategoryName(), GetValidDescription(), GetRandomBoolean());
    
    public Mock<IRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();
}

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureColletion : ICollectionFixture<CreateCategoryTestFixture>
{
    
}