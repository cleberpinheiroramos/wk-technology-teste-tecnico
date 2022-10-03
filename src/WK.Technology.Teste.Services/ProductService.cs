using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WK.Technology.Teste.Domain.Entities;
using WK.Technology.Teste.Domain.Filters;
using WK.Technology.Teste.Domain.Interfaces.Services;
using WK.Technology.Teste.Domain.ViewModel.Product;
using WK.Technology.Teste.Infra.Exceptions;

namespace WK.Technology.Teste.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IMapper mapper,
                              IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<IPagedList<ProductViewModel>> GetPagedAsync(ProductFilter filter, CancellationToken cancellationToken = default)
        {
            try
            {
                var repoProduct = _unitOfWork.GetRepository<Product>();
                var listProduct = await repoProduct.GetPagedListAsync(pageIndex: filter.PageIndex,
                                                                          pageSize: filter.PageSize,
                                                                          selector: u => new ProductViewModel()
                                                                          {
                                                                              Id = u.Id,
                                                                              Name = u.Name,
                                                                              Description = u.Description,
                                                                              Price = u.Price,
                                                                              CategoryId = u.CategoryId
                                                                          },
                                                                          include: i => i.Include(l => l.Category),
                                                                          orderBy: o => o.OrderBy(or => or.Id),
                                                                          predicate: filter.GetFilter(),
                                                                          cancellationToken: cancellationToken,
                                                                          disableTracking: false).ConfigureAwait(false);

                return listProduct;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public async Task<ProductViewModel> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            try
            {
                var model = await _unitOfWork.GetRepository<Product>()
                                             .GetFirstOrDefaultAsync(include: i => i.Include(l => l.Category),
                                                                     predicate: p => p.Id == id,
                                                                     selector: s => _mapper.Map<ProductViewModel>(s),
                                                                     disableTracking: false).ConfigureAwait(false);
                return model;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public async Task<ProductViewModel> InsertAsync(CreateProductViewModel productViewModel, CancellationToken cancellationToken = default)
        {
            try
            {
                if (productViewModel is null)
                    throw new ArgumentNullException();

                var product = _mapper.Map<Product>(productViewModel);
                var productRepo = _unitOfWork.GetRepository<Product>();
                await productRepo.InsertAsync(product, cancellationToken).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

                return _mapper.Map<ProductViewModel>(product);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public async Task<ProductViewModel> UpdateAsync(UpdateProductViewModel productViewModel, CancellationToken cancellationToken = default)
        {
            try
            {
                if (productViewModel is null)
                    throw new ArgumentNullException();

                var product = await _unitOfWork.GetRepository<Product>().FindAsync(productViewModel.Id);
                if (product != null)
                {
                    product.Name = productViewModel.Name;
                    product.Description = productViewModel.Description;
                    product.Price = productViewModel.Price; 
                    product.CategoryId = productViewModel.CategoryId;

                    _unitOfWork.GetRepository<Product>().Update(product);
                    await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
                }

                return _mapper.Map<ProductViewModel>(product);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            try
            {
                var product = await _unitOfWork.GetRepository<Product>().FindAsync(id);
                if (product != null)
                {
                    _unitOfWork.GetRepository<Product>().Delete(product);
                    await _unitOfWork.SaveChangesAsync().ConfigureAwait(false); ;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }
    }
}
