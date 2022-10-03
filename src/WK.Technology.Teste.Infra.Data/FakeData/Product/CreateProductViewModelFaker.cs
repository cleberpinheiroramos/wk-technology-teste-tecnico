using Bogus.Extensions;
using WK.Technology.Teste.Domain.ViewModel.Product;

namespace WK.Technology.Teste.Infra.Data.FakeData.Product
{
    public class CreateProductViewModelFaker : BaseFaker<CreateProductViewModel>
    {
        public CreateProductViewModelFaker()
        {
            Locale = "pt_BR";
            RuleFor(p => p.Name, f => f.Commerce.ProductName().ClampLength(3, 255));
            RuleFor(p => p.Description, f => f.Commerce.ProductAdjective().ClampLength(3, 255));
            RuleFor(p => p.Price, f => Convert.ToDecimal(f.Commerce.Price()));
        }
    }
}
