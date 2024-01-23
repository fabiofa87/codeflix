using CodeflixCatalog.Application.UseCases.Category.Common;
using MediatR;

namespace CodeflixCatalog.Application.UseCases.Category.GetCategory;

public interface IGetCategory : IRequestHandler<GetCategoryInput, CategoryModelOutput>
{
    
}