using CodeflixCatalogDomain.Domain.Exceptions;

namespace CodeflixCatalogDomain.Domain.Entity;

public class Category
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Guid Id { get; private set; }
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
        if (String.IsNullOrWhiteSpace(Name))
        {
            throw new EntityValidationException($"{nameof(Name)} should not be empty or null");
        }
        if (Name.Length < 3)
        {
            throw new EntityValidationException($"{nameof(Name)} should not be less than 3 characters");
        }

        if (Name.Length > 255)
        {
            throw new EntityValidationException($"{nameof(Name)} should not be greater than 255 characters");
        }
        if (Description == null)
        {
            throw new EntityValidationException($"{nameof(Description)} should not be empty or null");
        }

        if (Description.Length > 10000)
        {
            throw new EntityValidationException($"{nameof(Description)} should not be greater than 10.000 characters");
        }
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
