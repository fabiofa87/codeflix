using System.Collections;
using Bogus;

namespace CodeflixCatalog.UnitTests.Domain.Validation;

public class DomainValidationTest
{
    private Faker Faker { get; set; } = new Faker();
    
    [Fact(DisplayName = nameof(NotNullOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullOk()
    {
        var value = Faker.Commerce.ProductName();
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidate.DomainValidation.NotNull(value, fieldName);
        action.Should().NotThrow();
    }
    
    [Fact(DisplayName = nameof(ThrowWhenNull))]
    [Trait("Domain", "DomainValidation - Validation")]
    
    public void ThrowWhenNull()
    {
        string? value = null;
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidate.DomainValidation.NotNull(value, fieldName);
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be null");
    }
    
    [Theory(DisplayName = nameof(ThrowWhenNullOrEmpty))]
    [Trait("Domain", "DomainValidation - Validation")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void ThrowWhenNullOrEmpty(string? target)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidate.DomainValidation.NotNullOrEmpty(target, fieldName);
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be null or empty");
    }    
    
    [Fact(DisplayName = nameof(ThrowWhenNullOrEmptyOk))]
    [Trait("Domain", "DomainValidation - Validation")]

    public void ThrowWhenNullOrEmptyOk()
    {
        var target = Faker.Commerce.ProductName();
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidate.DomainValidation.NotNullOrEmpty(target, fieldName);
        action.Should()
            .NotThrow();
    }
    
    [Theory(DisplayName = nameof(MinLengthError))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesSmallerThanMin), parameters: 10)]
    
    public void MinLengthError(string value, int minLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidate.DomainValidation.MinLength(value,  minLength, fieldName);
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be less than {minLength} characters");
    }

    public static IEnumerable<object[]> GetValuesSmallerThanMin(int numberOfTests = 5)
    {
        yield return new object[] { "123456", 10 };
        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var exemple = faker.Commerce.ProductName();
            var minLength = exemple.Length + (new Random().Next(1, 20));
            yield return new object[] { exemple, minLength };
        }
    }
   
    [Theory(DisplayName = nameof(MinLengthOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesGreaterThanMin), parameters: 10)]
    
    public void MinLengthOk(string value, int minLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidate.DomainValidation.MinLength(value,  minLength, fieldName);
        action.Should()
            .NotThrow<EntityValidationException>();
    }
    
    public static IEnumerable<object[]> GetValuesGreaterThanMin(int numberOfTests = 5)
    {
        yield return new object[] { "123456", 6 };
        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var exemple = faker.Commerce.ProductName();
            var minLength = exemple.Length - (new Random().Next(1, 5));
            yield return new object[] { exemple, minLength };
        }
    }
    
    [Theory(DisplayName = nameof(MaxLengthError))]
    [Trait("Domain ", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesGreaterThanMax), parameters: 10)]
    public void MaxLengthError(string value, int maxLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidate.DomainValidation.MaxLength(value, maxLength, fieldName);
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be greater than {maxLength} characters");
        
    }
    
    public static IEnumerable<object[]> GetValuesGreaterThanMax(int numberOfTests = 5)
    {
        yield return new object[] { "123456", 5 };
        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var exemple = faker.Commerce.ProductName();
            var maxLength = exemple.Length - (new Random().Next(1, 5));
            yield return new object[] { exemple, maxLength };
        }
    }

    [Theory(DisplayName = nameof(MaxLengthOk))]
    [Trait("Domain ", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesSmallerThanMax), parameters: 10)]
    
    public void MaxLengthOk(string value, int maxLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidate.DomainValidation.MaxLength(value, maxLength, fieldName);
        action.Should()
            .NotThrow();
    }

    public static IEnumerable<object[]> GetValuesSmallerThanMax(int numberOfTests = 5)
    {
        yield return new object[] { "123456", 6 };
        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var exemple = faker.Commerce.ProductName();
            var maxLength = exemple.Length + (new Random().Next(1, 5));
            yield return new object[] { exemple, maxLength };
        }
    }
}