using System.Runtime.InteropServices.JavaScript;
using CodeflixCatalogDomain.Domain.Exceptions;
using FluentAssertions;
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

        category.Should().NotBeNull();
        category.Name.Should().Be(validData.Name);
        category.Description.Should().Be(validData.Description);
        category.Id.Should().NotBeEmpty();
        category.Created_At.Should().NotBeSameDateAs(default(DateTime));
        category.Created_At.Should().BeAfter(dateTimeBefore);
        category.Created_At.Should().BeBefore(dateTimeAfter);
        
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

        category.Should().NotBeNull();
        category.Name.Should().Be(validData.Name);
        category.Description.Should().Be(validData.Description);
        category.Id.Should().NotBeEmpty();
        category.Created_At.Should().NotBeSameDateAs(default(DateTime));
        category.Created_At.Should().BeAfter(dateTimeBefore);
        category.Created_At.Should().BeBefore(dateTimeAfter);
        category.Is_Active.Should().Be(isActive);        
        
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Category - Agregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void InstantiateErrorWhenNameIsEmpty(string? name)
    {
        Action action = () => new DomainEntity.Category(name!, "Category Description");

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");

    }    
    
    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
    [Trait("Domain", "Category - Agregates")]

  
    public void InstantiateErrorWhenDescriptionIsNull()
    {
        Action action = () => new DomainEntity.Category("Teste", null!);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Description should not be empty or null");
    }
    
    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Chars))]
    [Trait("Domain", "Category - Agregates")]
    [InlineData("a")]
    [InlineData("ab")]
    
    public void InstantiateErrorWhenNameIsLessThan3Chars(string name)
    {
        Action action = () => new DomainEntity.Category(name, "Category Description");
        
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be less than 3 characters");
    }
    
    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsMoreThan255Chars))]
    [Trait("Domain", "Category - Agregates")]

            
    public void InstantiateErrorWhenNameIsMoreThan255Chars()
    {
        var invalidName = String.Join(null, Enumerable.Range(0, 256).Select(_ => "a").ToArray());
        Action action = () => new DomainEntity.Category(invalidName, "Category Description");
        
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be greater than 255 characters");
        
    }
    
    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10kChars))]
    [Trait("Domain", "Category - Agregates")]
    
    public void InstantiateErrorWhenDescriptionIsGreaterThan10kChars()
    {
        var invalidDescription = String.Join(null, Enumerable.Range(0, 10001).Select(_ => "a").ToArray());
        Action action = () => new DomainEntity.Category("Category Name", invalidDescription);
        
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Description should not be greater than 10.000 characters");
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
        
        category.Is_Active.Should().BeTrue();
        
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
        
        category.Is_Active.Should().BeFalse();
    }
    
    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Category - Agregates")]
    public void Update()
        {

                var category = new DomainEntity.Category("Category Name", "Category Description");

                var newValues = new {Name = "New Category Name", Description = "New Category Description"};

                category.Update(newValues.Name, newValues.Description);
                
                category.Name.Should().Be(newValues.Name);
                category.Description.Should().Be(newValues.Description);
                
        }    
    [Fact(DisplayName = nameof(UpdateOnlyName))]
    [Trait("Domain", "Category - Agregates")]
    public void UpdateOnlyName()
        {

            var category = new DomainEntity.Category("Category Name", "Category Description");
            var newValues = new {Name = "New Category Name"};
            var currentDescription = category.Description;    

            category.Update(newValues.Name);
            
            category.Name.Should().Be(newValues.Name);
            category.Description.Should().Be(currentDescription);
            
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

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");
    }
    
    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsLessThan3Chars))]
    [Trait("Domain", "Category - Agregates")]
    [InlineData("a")]
    [InlineData("ab")]
    
    public void UpdateErrorWhenNameIsLessThan3Chars(string name)
    {
        var category = new DomainEntity.Category("Category Name", "Category Description");
        Action action = () => category.Update(name);
        
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be less than 3 characters");
    }
    
    [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreaterThan255Chars))]
    [Trait("Domain", "Category - Agregates")]
    
    public void UpdateErrorWhenNameIsGreaterThan255Chars()
    {
        var category = new DomainEntity.Category("Category Name", "Category Description");
        var invalidName = String.Join(null, Enumerable.Range(0, 256).Select(_ => "a").ToArray());
        Action action = () => category.Update(invalidName);
        
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be greater than 255 characters");
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreaterThan10kChars))]
    [Trait("Domain", "Category - Agregates")]

    public void UpdateErrorWhenDescriptionIsGreaterThan10kChars()
    {
        var category = new DomainEntity.Category("Category Name", "Category Description");
        var invalidDescription = String.Join(null, Enumerable.Range(0, 10001).Select(_ => "a").ToArray());
        Action action = () => category.Update("Category Name", invalidDescription);
        
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Description should not be greater than 10.000 characters");

    }
}