using AutoMapper;
using WK.Technology.Teste.Domain.Entities;
using WK.Technology.Teste.Domain.ViewModel.Product;

namespace WK.Technology.Teste.Domain.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>()
                .ForMember(x => x.CreatedOn, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedOn, opt => opt.Ignore());

            CreateMap<Product, CreateProductViewModel>();
            CreateMap<CreateProductViewModel, Product>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedOn, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedOn, opt => opt.Ignore());

            CreateMap<Product, UpdateProductViewModel>();
            CreateMap<UpdateProductViewModel, Product>()
                .ForMember(x => x.CreatedOn, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedOn, opt => opt.Ignore());
        }
    }
}
