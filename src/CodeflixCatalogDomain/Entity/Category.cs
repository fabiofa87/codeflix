using CodeflixCatalogDomain.SeedWork;
using CodeflixCatalogDomain.Validation;

namespace CodeflixCatalogDomain.Entity;

public class Category : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime Created_At { get; private set; }
    public bool Is_Active { get; private set; } = true;
    public Category(string name, string description, bool isActive = true)
    {
        Name = name;
        Description = description;
        Id = Guid.NewGuid();
        Created_At = DateTime.Now;
        Is_Active = isActive;
        Validate();
    }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        DomainValidation.MinLength(Name, 3, nameof(Name));
        DomainValidation.MaxLength(Name, 255, nameof(Name));
        DomainValidation.NotNull(Name, nameof(Name));
        DomainValidation.NotNull(Description, nameof(Description));
        DomainValidation.MaxLength(Description, 10000, nameof(Description));
        
    }
    
    public void Activate()
    {
        Is_Active = true;
        Validate();
    }

    public void Deactivate()
    {
        Is_Active = false;
        Validate();
    }

    public void Update(string name, string? description = null)
    {
        Name = name;
        Description = description ?? Description;
        Validate();   
    }
}
