using Bogus.Extensions;
using WK.Technology.Teste.Domain.Entities;

namespace WK.Technology.Teste.Infra.Data.FakeData.Product
{
    public class ProductFaker : BaseFaker<Domain.Entities.Product>
    {
        public ProductFaker()
        {
            Locale = "pt_BR";
            var id = 1;
            var idFkCategory = 1;
            RuleFor(p => p.Id, f => id++);
            RuleFor(p => p.CategoryId, f => idFkCategory++);
            RuleFor(p => p.Name, f => f.Commerce.ProductName().ClampLength(3, 255));
            RuleFor(p => p.Description, f => f.Commerce.ProductAdjective().ClampLength(3, 255));
            RuleFor(p => p.Price, f => Convert.ToDecimal(f.Commerce.Price()));
            RuleFor(p => p.CreatedBy, f => f.Person.Email.ClampLength(0, 30));
            RuleFor(p => p.CreatedOn, f => f.Date.Past());
            RuleFor(p => p.CreatedOn, f => f.Date.Past());
            RuleFor(p => p.UpdatedBy, f => f.Person.Email.ClampLength(0, 30));
            RuleFor(p => p.UpdatedOn, f => f.Date.Past());
        }
    }
}
