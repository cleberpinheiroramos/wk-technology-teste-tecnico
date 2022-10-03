using Bogus.Extensions;
using WK.Technology.Teste.Domain.ViewModel.Product;

namespace WK.Technology.Teste.Infra.Data.FakeData.Product
{
    public class UpdateProductViewModelFaker : BaseFaker<UpdateProductViewModel>
    {
        public UpdateProductViewModelFaker()
        {
            Locale = "pt_BR";
            var id = 1;
            var idFkCategory = 1;
            RuleFor(p => p.Id, f => id++);
            RuleFor(p => p.CategoryId, f => idFkCategory++);
            RuleFor(p => p.Name, f => f.Commerce.ProductName().ClampLength(3, 255));
            RuleFor(p => p.Description, f => f.Commerce.ProductAdjective().ClampLength(3, 255));
            RuleFor(p => p.Price, f => Convert.ToDecimal(f.Commerce.Price()));
        }
    }
}
