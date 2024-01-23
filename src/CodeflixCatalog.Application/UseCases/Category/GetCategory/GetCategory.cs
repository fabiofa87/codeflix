using CodeflixCatalog.Application.UseCases.Category.Common;
using CodeflixCatalogDomain.Repository;



namespace CodeflixCatalog.Application.UseCases.Category.GetCategory;

public class GetCategory : IGetCategory
{   
    private readonly ICategoryRepository _categoryRepository;
    
    public GetCategory(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<CategoryModelOutput> Handle(GetCategoryInput request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetById(request.Id, cancellationToken);
        return CategoryModelOutput.FromCategory(category);
    }
    
    
}