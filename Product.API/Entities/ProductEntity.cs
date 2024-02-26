namespace Product.API.Entities
{
    public class ProductEntity : BaseEntity
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
