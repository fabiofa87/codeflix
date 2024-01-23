using CodeflixCatalog.Application.UseCases.Category.Common;
using MediatR;

namespace CodeflixCatalog.Application.UseCases.CreateCategory.Category;

public interface ICreateCategory : IRequestHandler<CreateCategoryInput, CategoryModelOutput>
{
   
}