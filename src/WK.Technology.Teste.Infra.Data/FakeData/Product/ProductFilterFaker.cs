using Bogus.Extensions;
using WK.Technology.Teste.Domain.Filters;

namespace WK.Technology.Teste.Infra.Data.FakeData.Product
{
    public class ProductFilterFaker : BaseFaker<ProductFilter>
    {
        public ProductFilterFaker()
        {
            Locale = "pt_BR";
            var id = 1;
            RuleFor(p => p.Id, f => id++);
            RuleFor(p => p.Name, f => f.Company.CompanyName().ClampLength(3, 150));
            RuleFor(p => p.PageIndex, f => 0);
            RuleFor(p => p.PageSize, f => 10);
        }
    }
}
