using System.Linq.Expressions;
using WK.Technology.Teste.Domain.Base;
using WK.Technology.Teste.Domain.Entities;
using WK.Technology.Teste.Infra.Extensions;

namespace WK.Technology.Teste.Domain.Filters
{
    public class CategoryFilter : BaseFilter
    {
        public long? Id { get; set; }
        public string? Name { get; set; }

        private Expression<Func<Category, bool>> Filter = PredicateBuilder.True<Category>();

        public Expression<Func<Category, bool>> GetFilter()
        {
            if (this.Id.HasValue && this.Id > 0)
                Filter = Filter.And(x => x.Id == this.Id);

            if (!string.IsNullOrWhiteSpace(this.Name))
                Filter = Filter.And(c => c.Name == this.Name);

            return Filter;
        }
    }
}
