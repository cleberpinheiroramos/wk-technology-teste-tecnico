using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using WK.Technology.Teste.Domain.Filters;
using WK.Technology.Teste.Domain.ViewModel.Product;

namespace WK.Technology.Teste.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<IPagedList<ProductViewModel>> GetPagedAsync(ProductFilter? filter, CancellationToken cancellationToken = default);
        Task<ProductViewModel> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<ProductViewModel> InsertAsync(CreateProductViewModel ProductModel, CancellationToken cancellationToken = default);
        Task<ProductViewModel> UpdateAsync(UpdateProductViewModel ProductModel, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
