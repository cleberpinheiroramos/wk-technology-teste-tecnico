using System.Linq.Expressions;
using WK.Technology.Teste.Domain.Base;
using WK.Technology.Teste.Domain.Entities;
using WK.Technology.Teste.Infra.Extensions;

namespace WK.Technology.Teste.Domain.Filters
{
    public class ProductFilter : BaseFilter
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        private Expression<Func<Product, bool>> Filter = PredicateBuilder.True<Product>();

        public Expression<Func<Product, bool>> GetFilter()
        {
            if (this.Id.HasValue && this.Id > 0)
                Filter = Filter.And(x => x.Id == this.Id);

            if (!string.IsNullOrWhiteSpace(this.Name))
                Filter = Filter.And(c => c.Name == this.Name);

            if (!string.IsNullOrWhiteSpace(this.Description))
                Filter = Filter.And(c => c.Description == this.Description);

            return Filter;
        }
    }
}
