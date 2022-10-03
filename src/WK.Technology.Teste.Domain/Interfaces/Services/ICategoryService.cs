using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using WK.Technology.Teste.Domain.Filters;
using WK.Technology.Teste.Domain.ViewModel.Category;

namespace WK.Technology.Teste.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IPagedList<CategoryViewModel>> GetPagedAsync(CategoryFilter filter, CancellationToken cancellationToken = default);
        Task<CategoryViewModel> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<CategoryViewModel> InsertAsync(CreateCategoryViewModel CategoryModel, CancellationToken cancellationToken = default);
        Task<CategoryViewModel> UpdateAsync(UpdateCategoryViewModel CategoryModel, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
