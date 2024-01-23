using MediatR;

namespace CodeflixCatalog.Application.UseCases.Category.DeleteCategory;

public interface IDeleteCategory : IRequestHandler<DeleteCategoryInput>
{
    
}