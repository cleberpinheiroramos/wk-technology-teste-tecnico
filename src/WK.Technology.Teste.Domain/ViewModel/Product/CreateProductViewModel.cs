using WK.Technology.Teste.Domain.ViewModel.Category;

namespace WK.Technology.Teste.Domain.ViewModel.Product
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
    }
}
