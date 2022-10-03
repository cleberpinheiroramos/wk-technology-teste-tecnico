using AutoMapper;
using WK.Technology.Teste.Domain.Entities;
using WK.Technology.Teste.Domain.ViewModel.Category;

namespace WK.Technology.Teste.Domain.Mapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>()
                .ForMember(x => x.CreatedOn, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedOn, opt => opt.Ignore());

            CreateMap<Category, CreateCategoryViewModel>();
            CreateMap<CreateCategoryViewModel, Category>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedOn, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedOn, opt => opt.Ignore());

            CreateMap<Category, UpdateCategoryViewModel>();
            CreateMap<UpdateCategoryViewModel, Category>()
                .ForMember(x => x.CreatedOn, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedOn, opt => opt.Ignore());
        }
    }
}
