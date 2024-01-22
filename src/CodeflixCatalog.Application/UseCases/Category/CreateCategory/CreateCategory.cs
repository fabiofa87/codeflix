using CodeflixCatalog.Application.Interfaces;
using CodeflixCatalogDomain.Repository;
using DomainEntity = CodeflixCatalogDomain.Entity;

namespace CodeflixCatalog.Application.UseCases.CreateCategory.Category;

public class CreateCategory : ICreateCategory
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateCategoryOutput> Handle(CreateCategoryInput input, CancellationToken cancellationToken)
    {
        var category = new DomainEntity.Category(
            input.Name, input.Description, input.IsActive
            );
        await _categoryRepository.Insert(category, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return CreateCategoryOutput.FromCategory(category);
    }
}