using System.Runtime.InteropServices.JavaScript;
using CodeflixCatalogDomain.Domain.Exceptions;
using DomainEntity = CodeflixCatalogDomain.Domain.Entity;
namespace CodeflixCatalog.UnitTests.Domain.Entity.Category;

public class CategoryTests
{
    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Category - Agregates")]
    public void Instantiate()
    {
        var validData = new
        {
            Name = "Ccategory name",
            Description = "Some description"
        };
        var dateTimeBefore = DateTime.Now;
        var category = new DomainEntity.Category(validData.Name, validData.Description);
        var dateTimeAfter = DateTime.Now;
        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        Assert.NotEqual(default(Guid), category.Id);
        Assert.NotEqual(default(DateTime), category.Created_At);
        Assert.True(category.Created_At > dateTimeBefore);
        Assert.True(category.Created_At < dateTimeAfter);
        Assert.True(category.Is_Active);
    }
    
    [Theory(DisplayName = nameof(InstantiateWithActiveStatus))]
    [Trait("Domain", "Category - Agregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithActiveStatus(bool isActive)
    {
        var validData = new
        {
            Name = "Ccategory name",
            Description = "Some description"
        };
        var dateTimeBefore = DateTime.Now;
        var category = new DomainEntity.Category(validData.Name, validData.Description, isActive);
        var dateTimeAfter = DateTime.Now;
        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        Assert.NotEqual(default(Guid), category.Id);
        Assert.NotEqual(default(DateTime), category.Created_At);
        Assert.True(category.Created_At > dateTimeBefore);
        Assert.True(category.Created_At < dateTimeAfter);
        Assert.Equal(isActive, category.Is_Active);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Category - Agregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void InstantiateErrorWhenNameIsEmpty(string? name)
    {
        Action action = () => new DomainEntity.Category(name!, "Category Description");
        
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should not be empty or null", exception.Message);
    }    
    
    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
    [Trait("Domain", "Category - Agregates")]

  
    public void InstantiateErrorWhenDescriptionIsNull()
    {
        Action action = () => new DomainEntity.Category("Teste", null!);
        
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description should not be empty or null", exception.Message);
    }
    
    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Chars))]
    [Trait("Domain", "Category - Agregates")]
    [InlineData("a")]
    [InlineData("ab")]
    
    public void InstantiateErrorWhenNameIsLessThan3Chars(string name)
    {
        Action action = () => new DomainEntity.Category(name, "Category Description");
        
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should not be less than 3 characters", exception.Message);
    }
    
    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsMoreThan255Chars))]
    [Trait("Domain", "Category - Agregates")]

            
    public void InstantiateErrorWhenNameIsMoreThan255Chars()
    {
        var invalidName = String.Join(null, Enumerable.Range(0, 256).Select(_ => "a").ToArray());
        Action action = () => new DomainEntity.Category(invalidName, "Category Description");
        
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should not be greater than 255 characters", exception.Message);
    }
    
    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10kChars))]
    [Trait("Domain", "Category - Agregates")]
    
    public void InstantiateErrorWhenDescriptionIsGreaterThan10kChars()
    {
        var invalidDescription = String.Join(null, Enumerable.Range(0, 10001).Select(_ => "a").ToArray());
        Action action = () => new DomainEntity.Category("Category Name", invalidDescription);
        
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description should not be greater than 10.000 characters", exception.Message);
    }
    
    [Fact(DisplayName = nameof(Activate))]
    [Trait("Domain", "Category - Agregates")]

    public void Activate()
    {
        var validData = new
        {
            Name = "Ccategory name",
            Description = "Some description"
        };
        
        var category = new DomainEntity.Category(validData.Name, validData.Description, false);
        category.Activate();
        
        Assert.True(category.Is_Active);
    }
    
    [Fact(DisplayName = nameof(Deactivate))]
    [Trait("Domain", "Category - Agregates")]

    public void Deactivate()
    {
        var validData = new
        {
            Name = "Ccategory name",
            Description = "Some description"
        };
        
        var category = new DomainEntity.Category(validData.Name, validData.Description, true);
        category.Deactivate();
        
        Assert.False(category.Is_Active);
    }
    
    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Category - Agregates")]
    public void Update()
        {

                var category = new DomainEntity.Category("Category Name", "Category Description");

                var newValues = new {Name = "New Category Name", Description = "New Category Description"};

                category.Update(newValues.Name, newValues.Description);
                
                Assert.Equal(newValues.Name, category.Name);
                Assert.Equal(newValues.Description, category.Description);
        }    
    [Fact(DisplayName = nameof(UpdateOnlyName))]
    [Trait("Domain", "Category - Agregates")]
    public void UpdateOnlyName()
        {

            var category = new DomainEntity.Category("Category Name", "Category Description");
            var newValues = new {Name = "New Category Name"};
            var currentDescription = category.Description;    

            category.Update(newValues.Name);
                
            Assert.Equal(newValues.Name, category.Name);
            Assert.Equal(category.Description, currentDescription);
        }
    
    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Category - Agregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    
    public void UpdateErrorWhenNameIsEmpty(string? name)
    {
        var category = new DomainEntity.Category("Category Name", "Category Description");
        Action action = () => category.Update(name!);
        
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should not be empty or null", exception.Message);
    }
    
    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsLessThan3Chars))]
    [Trait("Domain", "Category - Agregates")]
    [InlineData("a")]
    [InlineData("ab")]
    
    public void UpdateErrorWhenNameIsLessThan3Chars(string name)
    {
        var category = new DomainEntity.Category("Category Name", "Category Description");
        Action action = () => category.Update(name);
        
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should not be less than 3 characters", exception.Message);
    }
    
    [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreaterThan255Chars))]
    [Trait("Domain", "Category - Agregates")]
    
    public void UpdateErrorWhenNameIsGreaterThan255Chars()
    {
        var category = new DomainEntity.Category("Category Name", "Category Description");
        var invalidName = String.Join(null, Enumerable.Range(0, 256).Select(_ => "a").ToArray());
        Action action = () => category.Update(invalidName);
        
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should not be greater than 255 characters", exception.Message);
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreaterThan10kChars))]
    [Trait("Domain", "Category - Agregates")]

    public void UpdateErrorWhenDescriptionIsGreaterThan10kChars()
    {
        var category = new DomainEntity.Category("Category Name", "Category Description");
        var invalidDescription = String.Join(null, Enumerable.Range(0, 10001).Select(_ => "a").ToArray());
        Action action = () => category.Update("Category Name", invalidDescription);
        
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description should not be greater than 10.000 characters", exception.Message);
    }
}