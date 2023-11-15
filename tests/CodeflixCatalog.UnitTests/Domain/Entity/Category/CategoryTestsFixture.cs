
namespace CodeflixCatalog.UnitTests.Domain.Entity.Category;

public class CategoryTestsFixture
{
    public DomainEntity.Category GetValidCategory() => new("Category name", "Some description");
}

[CollectionDefinition(nameof(CategoryTestsFixture))]
public class CategoryTestsFixtureCollection : ICollectionFixture<CategoryTestsFixture> {}
