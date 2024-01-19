namespace CodeflixCatalog.Application.UseCases.CreateCategory.Category;

public interface ICreateCategory
{
    public Task<CreateCategoryOutput> Handle(CreateCategoryInput input, CancellationToken cancellationToken);
}