namespace Product.API.DTOs.Payloads
{
    public record CreateProductPayload
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
