namespace WK.Technology.Teste.Domain.ViewModel.Product
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long? CategoryId { get; set; }

        public object Clone()
        {
            var product = (ProductViewModel)MemberwiseClone();
            return product;
        }

        public ProductViewModel CloneTipado()
        {
            return (ProductViewModel)Clone();
        }
    }
}
