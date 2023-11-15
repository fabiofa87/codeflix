
using CodeflixCatalog.UnitTests.Common;

namespace CodeflixCatalog.UnitTests.Domain.Entity.Category;

public class CategoryTestsFixture : FixtureBase
{
    public string GetValidCategoryName()
    {
        var categoryName = "";
        while(categoryName.Length < 3) 
            categoryName = Faker.Commerce.Categories(1)[0];
        if (categoryName.Length > 255)
            categoryName = categoryName[..255];
        return categoryName;
    }

    public string GetValidDescription()
    {
        var description = Faker.Commerce.ProductDescription();
        
        if(description.Length > 10000) 
            description = description[..10000];
        return description;
    }


    public DomainEntity.Category GetValidCategory() => new(
        GetValidCategoryName(),
        GetValidDescription());
}

[CollectionDefinition(nameof(CategoryTestsFixture))]
public class CategoryTestsFixtureCollection : ICollectionFixture<CategoryTestsFixture> {}
