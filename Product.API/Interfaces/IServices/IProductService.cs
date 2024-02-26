namespace Product.API.Interfaces.IServices
{
    public interface IProductService
    {
        Task<Result<ProductModel>> CreateProduct(CreateProductPayload createProduct);
        Task<Result<ProductModel>> GetProduct(string productId);
    }
}
