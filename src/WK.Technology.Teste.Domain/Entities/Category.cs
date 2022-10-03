using WK.Technology.Teste.Domain.Base;

namespace WK.Technology.Teste.Domain.Entities
{
    public class Category : BaseEntity<long>
    {
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
