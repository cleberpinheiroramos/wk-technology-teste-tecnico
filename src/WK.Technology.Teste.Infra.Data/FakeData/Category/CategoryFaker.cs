using Bogus.Extensions;

namespace WK.Technology.Teste.Infra.Data.FakeData.Category
{
    public class CategoryFaker : BaseFaker<Domain.Entities.Category>
    {
        public CategoryFaker()
        {
            Locale = "pt_BR";
            var id = 1;
            RuleFor(p => p.Id, f => id++);
            RuleFor(p => p.Name, f => f.Commerce.Department().ClampLength(3, 255));
            RuleFor(p => p.CreatedBy, f => f.Person.Email.ClampLength(0, 30));
            RuleFor(p => p.CreatedOn, f => f.Date.Past());
            RuleFor(p => p.CreatedOn, f => f.Date.Past());
            RuleFor(p => p.UpdatedBy, f => f.Person.Email.ClampLength(0, 30));
            RuleFor(p => p.UpdatedOn, f => f.Date.Past());
        }
    }
}
