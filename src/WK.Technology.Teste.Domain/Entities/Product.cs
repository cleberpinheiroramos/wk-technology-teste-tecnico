using WK.Technology.Teste.Domain.Base;

namespace WK.Technology.Teste.Domain.Entities
{
    public class Product : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public long? CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
