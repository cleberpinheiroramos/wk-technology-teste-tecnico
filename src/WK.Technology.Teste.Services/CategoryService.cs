using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WK.Technology.Teste.Domain.Entities;
using WK.Technology.Teste.Domain.Filters;
using WK.Technology.Teste.Domain.Interfaces.Services;
using WK.Technology.Teste.Domain.ViewModel.Category;
using WK.Technology.Teste.Infra.Exceptions;

namespace WK.Technology.Teste.Services
{
    public class CategoryService : BaseService, ICategoryService
    {

        public CategoryService(IMapper mapper,
                                IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<IPagedList<CategoryViewModel>> GetPagedAsync(CategoryFilter filter, CancellationToken cancellationToken = default)
        {
            try
            {
                var repoCategory = _unitOfWork.GetRepository<Category>();
                var listCategory = await repoCategory.GetPagedListAsync(pageIndex: filter.PageIndex,
                                                                          pageSize: filter.PageSize,
                                                                          selector: u => new CategoryViewModel()
                                                                          {
                                                                              Id = u.Id,
                                                                              Name = u.Name
                                                                          },
                                                                          include: i => i.Include(l => l.Products),
                                                                          orderBy: o => o.OrderBy(or => or.Name),
                                                                          predicate: filter.GetFilter(),
                                                                          cancellationToken: cancellationToken,
                                                                          disableTracking: false).ConfigureAwait(false);

                return listCategory;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public async Task<CategoryViewModel> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            try
            {
                var model = await _unitOfWork.GetRepository<Category>()
                                             .GetFirstOrDefaultAsync(include: i => i.Include(l => l.Products),
                                                                     predicate: p => p.Id == id,
                                                                     selector: s => _mapper.Map<CategoryViewModel>(s),
                                                                     disableTracking: false).ConfigureAwait(false);

                return model;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public async Task<CategoryViewModel> InsertAsync(CreateCategoryViewModel categoryViewModel, CancellationToken cancellationToken = default)
        {
            try
            {
                if (categoryViewModel is null)
                    throw new ArgumentNullException();

                var category = _mapper.Map<Category>(categoryViewModel);
                var categoryRepo = _unitOfWork.GetRepository<Category>();
                await categoryRepo.InsertAsync(category, cancellationToken).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

                return _mapper.Map<CategoryViewModel>(category);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public async Task<CategoryViewModel> UpdateAsync(UpdateCategoryViewModel categoryViewModel, CancellationToken cancellationToken = default)
        {
            try
            {
                if (categoryViewModel is null)
                    throw new ArgumentNullException();

                var category = await _unitOfWork.GetRepository<Category>().FindAsync(categoryViewModel.Id);
                if (category != null)
                {
                    category.Name = categoryViewModel.Name;

                    _unitOfWork.GetRepository<Category>().Update(category);
                    await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
                }

                return _mapper.Map<CategoryViewModel>(category);
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
                var category = await _unitOfWork.GetRepository<Category>().FindAsync(id);
                if (category != null)
                {
                    _unitOfWork.GetRepository<Category>().Delete(category);
                    await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
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
