using DomainEntity = CodeflixCatalogDomain.Entity;
namespace CodeflixCatalog.Application.UseCases.Category.GetCategory;

public class GetCategoryOutput
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public GetCategoryOutput(
        Guid id, 
        string name, 
        string description, 
        bool isActive, 
        DateTime createdAt
    )
    {
        Id = id;
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = createdAt;
    }
    
    public static GetCategoryOutput FromCategory(DomainEntity.Category category)  => new(
        category.Id, 
        category.Name, 
        category.Description, 
        category.Is_Active, 
        category.Created_At);
}